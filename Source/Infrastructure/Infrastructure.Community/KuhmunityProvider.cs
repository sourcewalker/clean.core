using Core.Infrastructure.Interfaces.Account;
using Core.Infrastructure.Interfaces.UserData;
using Core.Shared.Models;
using Infrastructure.Community.Login;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Community
{
    public class KuhmunityProvider : IAccountProvider, IUserDataProvider
    {
        private readonly IConfiguration _configuration;

        public KuhmunityProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Configurations Configuration
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string GetLoginCookie()
        {
            throw new NotImplementedException();
        }

        public User GetUserDetails<T>(T id)
        {
            throw new NotImplementedException();
        }

        public LoginResult Login(LoginInfo login)
        {
            var loginInfo = new KuhmunityLoginModule(_configuration)
            {
                Email = login.Username,
                Password = login.Password
            };

            var result = loginInfo.Login();
            return new LoginResult
            {
                Success = result.IsSuccessful,
                Message = result.Message
            };
        }

        public LoginResult Logout()
        {
            throw new NotImplementedException();
        }

        public LoginResult Register(RegisterInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
