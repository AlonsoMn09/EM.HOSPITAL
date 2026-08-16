using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Authentication.Model
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationMinutes { get; set; } = default!;
    }
}
