using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Web.Jwt
{
    public class JwtSettings
    {
        public string Secret { get; set; } = "super-duper-secret-key";
    }
}
