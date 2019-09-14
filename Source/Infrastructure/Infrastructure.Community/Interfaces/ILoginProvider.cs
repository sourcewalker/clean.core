using Infrastructure.Community.Models;

namespace Infrastructure.Community.Interfaces
{
    public interface ILoginProvider
    {
        KuhmunityResponse Login();

        string GetLoginCookie();

        KuhmunityResponse Logout();
    }
}
