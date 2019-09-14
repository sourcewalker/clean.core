using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Community.Models
{
    public class KuhmunityResponse
    {
        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public string URL { get; set; }

        public object Body { get; set; }

        public object ErrorMessage { get; set; }
    }
}
