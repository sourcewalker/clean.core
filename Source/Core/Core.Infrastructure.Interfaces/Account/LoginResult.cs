using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Interfaces.Account
{
    public class LoginResult
    {
        public int Code { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
