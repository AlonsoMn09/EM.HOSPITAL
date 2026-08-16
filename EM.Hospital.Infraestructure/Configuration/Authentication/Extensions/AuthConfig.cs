using EM.Hospital.Infraestructure.Configuration.Authentication.Context;
using EM.Hospital.Infraestructure.Configuration.Authentication.Entities;
using EM.Hospital.Infraestructure.Configuration.Authentication.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Authentication.Extensions
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuración de ASPNetCore Identity

            services.AddIdentity<UserIdentity, IdentityRole>(policy =>
            {
                policy.Password.RequiredLength = 6;
                policy.Password.RequireDigit = true;
                policy.Password.RequireUppercase = true;
                policy.Password.RequireLowercase = true;
                policy.Password.RequireNonAlphanumeric = false;
                policy.User.RequireUniqueEmail = true;
                policy.Lockout.MaxFailedAccessAttempts = 3;
                policy.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                policy.Lockout.AllowedForNewUsers = true;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            //Configuracíón de JWT

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            if (jwtOptions is null) throw new ArgumentNullException("No se han proporcionado las configuraciones de JWT.");

            services.AddSingleton(jwtOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
