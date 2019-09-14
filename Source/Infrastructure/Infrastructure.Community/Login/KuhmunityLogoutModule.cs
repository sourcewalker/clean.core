using Infrastructure.Community.Helper;
using Infrastructure.Community.Models;

namespace Infrastructure.Community.Login
{
    public partial class KuhmunityLoginModule
    {
        public KuhmunityResponse Logout()
        {
            return KuhmunityLoginHelper.Logout(GetKuhmunityApiEndpoint(_configuration));
        }
    }
}
