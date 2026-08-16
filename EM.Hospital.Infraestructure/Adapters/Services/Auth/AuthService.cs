using EM.Hospital.Application.Common.Contracts.Services.Auth;
using EM.Hospital.Application.Common.Models.Auth;
using EM.Hospital.Infraestructure.Configuration.Authentication.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EM.Hospital.Infraestructure.Adapters.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;

        public AuthService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(User user)
        {
            var expirationDate = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

            //Header
            var credentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            //Payload
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.Role),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.UserData, user.PatientFullName),
                new(ClaimTypes.Expiration, expirationDate.ToString("yyyy-mm-dd hh:mm")),
                new(ClaimTypes.NameIdentifier, user.PatientId.ToString()),
            };

            //Signature
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expirationDate,
                signingCredentials: new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256)
                );

            //Token

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
