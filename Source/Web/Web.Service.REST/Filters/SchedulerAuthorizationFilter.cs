using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using System.Threading;

namespace Web.Service.REST.Filters
{
    public class SchedulerAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public SchedulerAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            var options = new BasicAuthAuthorizationFilterOptions
            {
                RequireSsl = true,
                LoginCaseSensitive = true,
                Users = new[]
                {
                    new BasicAuthAuthorizationUser
                    {
                        Login = _configuration["BasicAuth:Proximity:Key"],
                        PasswordClear = _configuration["BasicAuth:Proximity:Password"]
                    },
                    new BasicAuthAuthorizationUser
                    {
                        Login = _configuration["BasicAuth:System:Key"],
                        PasswordClear = _configuration["BasicAuth:System:Password"]
                    },
                    new BasicAuthAuthorizationUser
                    {
                        Login = _configuration["BasicAuth:External:Key"],
                        PasswordClear = _configuration["BasicAuth:External:Password"]
                    }
                }
            };
            var httpContext = context.GetHttpContext();
            var hasAccess = httpContext.User.Identity.IsAuthenticated;

            if (hasAccess)
            {
                var user = new GenericIdentity("Hangfire");
                var principal = new GenericPrincipal(user, null);
                Thread.CurrentPrincipal = principal;
            }

            return hasAccess;
        }
    }
}
