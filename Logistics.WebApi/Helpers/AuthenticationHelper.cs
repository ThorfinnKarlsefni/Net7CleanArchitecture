using System;
using System.Security.Claims;
using System.Text;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Logistics.WebApi.Helpers
{
    public class AuthenticationHelper
    {

        public static void ConfigureService(IServiceCollection services, string issuer, string audience, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = tokenValidationParameters;
                    option.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            if (context.Principal != null)
                            {
                                var userIdClaim = context.Principal.FindFirst(ClaimTypes.NameIdentifier);
                                var tokenVersionClaim = context.Principal.FindFirstValue("token_version");
                                var tokenService = services.BuildServiceProvider().GetRequiredService<ITokenService>();
                                if (userIdClaim != null && tokenVersionClaim != null)
                                {
                                    if (!await tokenService.ValidatorTokenAsync(userIdClaim.Value, tokenVersionClaim))
                                        context.Fail("Invalid token");
                                }
                            }

                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}

