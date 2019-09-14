using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Interfaces.Account
{
    public class LoginInfo
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
