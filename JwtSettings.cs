using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwtapi
{
    public class JwtSettings
    {
        public string Audience {get; set;}
        public string Issuer {get; set; }
        public string Secret {get; set;}
    }
}