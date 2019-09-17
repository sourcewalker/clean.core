using Core.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Web.Service.REST.Models;

namespace Web.Service.REST.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            var configuration = (IConfiguration)actionContext.ControllerContext.RequestContext.Configuration.DependencyResolver.GetService(typeof(IConfiguration));

            if (authHeader != null)
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthenticationToken = Encoding.UTF8.GetString(
                        Convert.FromBase64String(authenticationToken));
                var usernamePasswordArray = decodedAuthenticationToken.Split(':');

                var key = usernamePasswordArray.First();
                var password = usernamePasswordArray.Last();

                var currentUser = string.Empty;
                var hasAccess = false;

                if ((configuration["BasicAuth:Proximity:Key"] == key &&
                    configuration["BasicAuth:Proximity:Password"] == password) ||
                    (configuration["BasicAuth:System:Key"] == key &&
                    configuration["BasicAuth:System:Password"] == password) ||
                    (configuration["BasicAuth:External:Key"] == key &&
                    configuration["BasicAuth:External:Password"] == password))
                {
                    currentUser = key;
                    hasAccess = true;
                }

                if (hasAccess)
                {
                    var user = new GenericIdentity(currentUser);
                    var principal = new GenericPrincipal(user, null);
                    Thread.CurrentPrincipal = principal;

                    return;
                }
            }

            HandleUnauthorized(actionContext);
        }

        private static void HandleUnauthorized(HttpActionContext actionContext)
        {
            dynamic expando = new ExpandoObject();
            expando.Description = $"Authentication failed";

            var apiResponse = new ApiResponse
            {
                Success = false,
                Message = "Unauthorized action",
                Data = expando
            };

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, apiResponse);
            actionContext.Response.Headers.Add("WWW-Authenticate",
                $"Basic Scheme='Data' location='{actionContext.Request.RequestUri.Authority}'");
        }
    }
}
