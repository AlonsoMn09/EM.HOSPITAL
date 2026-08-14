using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Auth.Login.DTO
{
    public class LoginResponse
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
