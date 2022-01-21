using System;
using System.Collections.Generic;
using System.Text;

namespace pruebatecnicapeaku.Shared.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
