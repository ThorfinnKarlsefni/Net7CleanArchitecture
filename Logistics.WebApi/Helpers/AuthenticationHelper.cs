using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Logistics.WebApi.Infrastructure.Helpers
{

    public class AuthenticationHelper
    {
        public static void ConfigureService(IServiceCollection service, string issuer, string audience, string secretKey)
        {
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(secretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                };

                //options.RequireHttpsMetadata = false;
                //options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
            });
        }
    }

}

