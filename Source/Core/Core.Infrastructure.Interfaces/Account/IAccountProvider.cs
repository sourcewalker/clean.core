using Core.Shared.Models;

namespace Core.Infrastructure.Interfaces.Account
{
    public interface IAccountProvider
    {
        Configurations Configuration { get; set; }

        LoginResult Login(LoginInfo login);

        LoginResult Logout();

        string GetLoginCookie();

        LoginResult Register(RegisterInfo user);
    }
}
