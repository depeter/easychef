using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Contracts.Shared.RequestResponse
{
    public class VerifyLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class VerifyLoginResponse
    {
        public bool Success { get; set; }
    }
}
