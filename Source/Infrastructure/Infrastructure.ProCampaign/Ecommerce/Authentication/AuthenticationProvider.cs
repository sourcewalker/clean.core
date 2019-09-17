using Core.Infrastructure.Interfaces.Caching;
using Core.Infrastructure.Interfaces.CRM;
using Core.Infrastructure.Interfaces.Logging;
using Infrastructure.ProCampaign.Ecommerce.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Infrastructure.ProCampaign.Ecommerce.Authentication
{
    /// <summary>
    /// This class is a manager class providing authentication 
    /// to Consultix Ecommerce API
    /// </summary>
    /// <seealso cref="AuthenticationEcommerceManager"/>
    /// <seealso cref="Consumer.UserProfileManager"/>
    /// <seealso cref="Manager.Ecommerce.OrderEcommerceManager"/>
    /// <seealso cref="Manager.Ecommerce.ProductEcommerceManager"/>
    public class AuthenticationEcommerceManager : ICrmAuthenticationProvider
    {

        private readonly ICacheProvider _cacheProvider;
        private readonly ILoggingProvider _logger;
        private readonly IConfiguration _configuration;

        private static AuthenticationHelper _helper;

        public AuthenticationEcommerceManager(
            ICacheProvider cacheProvider,
            ILoggingProvider logger,
            IConfiguration configuration)
        {
            _cacheProvider = cacheProvider;
            _logger = logger;
            _configuration = configuration;
            _helper = new AuthenticationHelper(configuration);
        }

        /// <summary>
        /// This method retrieves token string for a specific <paramref name="siteId" />.
        /// Token is retrieved from cache if it exists there and is valid, otherwise provided by the remote API
        /// </summary>
        /// <returns>
        /// Token string for Consultix authorization
        /// </returns>
        /// <param name="siteId">Site Id Guid associated to the token</param>
        /// <seealso cref="GetClientId(Guid)"/>
        /// <seealso cref="RefreshToken(string)"/>
        public string GetToken(Guid siteId)
        {

            var clientId = GetClientId(siteId);

            // Cache key is related to client Id. Client Id is related to site Id
            var cacheKey = "{{ClientId_CacheKey}}";

            // Get from remote API if not existing in cache
            if (!_cacheProvider.ContainsKey(cacheKey))
            {
                var bearer = RefreshToken(clientId);
                _cacheProvider.Add(cacheKey, bearer, bearer.ExpiryStamp);
                return bearer?.AccessToken;
            }
            else
            {
                var bearer = _cacheProvider.Get<BearerToken>(cacheKey);

                // Check token validity if from cache, otherwise get from remote
                if (_helper.IsValidToken(bearer))
                {
                    return bearer.AccessToken;
                }
                else
                {
                    var newBearer = RefreshToken(clientId);
                    _cacheProvider.Add(cacheKey, newBearer, newBearer.ExpiryStamp);
                    return bearer?.AccessToken;
                }
            }
        }

        /// <summary>
        /// This method retrieves token string for a specific <paramref name="siteId" />.
        /// Token is retrieved from cache if it exists there and is valid, otherwise provided by the remote API
        /// </summary>
        /// <returns>
        /// Token string for Consultix authorization
        /// </returns>
        /// <param name="siteId">Site Id Guid associated to the token</param>
        /// <seealso cref="GetToken(Guid)"/>
        /// <seealso cref="RefreshToken(string)"/>
        private string GetClientId(Guid siteId)
        {

            try
            {
                return _configuration["ConsultixAuth:ClientIdKey"];
            }
            catch (Exception ex)
            {
                Exception e = new Exception("One of the keys was not found : ", ex);
                _logger.LogError(e.Message, e);
                return string.Empty;
            }
        }

        /// <summary>
        /// This method make call to remote service API for retrieving token
        /// </summary>
        /// <returns>
        /// Bearer Token class from Consultix authorization API response
        /// </returns>
        /// <param name="clientId">Client Id string for token</param>
        /// <seealso cref="GetToken(Guid)"/>
        /// <seealso cref="GetClientId(Guid)"/>
        private BearerToken RefreshToken(string clientId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var postWebRequest = (HttpWebRequest)WebRequest.Create(_helper.AuthorizationUrl);
            postWebRequest.PreAuthenticate = true;

            var postData = $"grant_type={_helper.GrantType}&" +
                           $"username={_helper.Username}&" +
                           $"password={_helper.Password}&" +
                           $"client_id={clientId}";

            postWebRequest.Method = "POST";
            var data = Encoding.ASCII.GetBytes(postData);


            postWebRequest.Accept = "application/json";
            postWebRequest.ContentType = "application/x-www-form-urlencoded";
            postWebRequest.ContentLength = data.Length;

            using (var stream = postWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)postWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return BearerToken.FromJson(responseString);
                }
                else
                {
                    _logger.LogError("Authentication fail", new Exception("Error obtaining OAuth token. Response code: " + response.StatusCode.ToString()));
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Authentication fail", new Exception("Error calling the Authentication service.", new Exception(
                    "[Source]: " + ex.Source +
                    ", [Message]: " + ex.Message +
                    ", [Stack Trace]: " + ex.StackTrace
                    )));
                return null;
            }
        }

    }
}
