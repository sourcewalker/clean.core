using Infrastructure.ProCampaign.Ecommerce.Entities;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.ProCampaign.Ecommerce.Authentication
{
    /// <summary>
    /// This class is a helper class providing properties and methods used by AuthenticationEcommerceManager
    /// for Consultix Ecommerce API connexion
    /// </summary>
    /// <seealso cref="AuthenticationEcommerceManager"/>
    public class AuthenticationHelper
    {
        private readonly IConfiguration _configuration;

        public AuthenticationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <value>Consultix admin username.</value>
        public string Username => _configuration["CSX_eCommerce:adminUsername"];

        /// <value>Consultix admin password.</value>
        public string Password => _configuration["CSX_eCommerce:adminPassword"];

        /// <value>Consultix Url from authorization (retrieving access token).</value>
        public string AuthorizationUrl => string.Concat(_configuration["ConsultixAuthBaseUrl"],
                                            _configuration["ConsultixAuth:TokenRequestPath"]);

        /// <value>Grant type authorization requested.</value>
        public string GrantType => _configuration["ConsultixAuth:GrantType"];

        /// <summary>
        /// This method validates if the parameter <paramref name="token" /> is valid and can be used.
        /// </summary>
        /// <returns>
        /// True if valid and false otherwise
        /// </returns>
        /// <param name="token"> A token class of type BearerToken to be validated</param>
        /// <seealso cref="BearerToken"/>
        public bool IsValidToken(BearerToken token)
        {
            // Adding 2 seconds guard to ensure the request is done before the expiry time has reached
            return token != null &&
                   !string.IsNullOrEmpty(token.AccessToken) &&
                   token.ExpirationDate > DateTime.UtcNow.AddSeconds(2);
        }
    }
}
