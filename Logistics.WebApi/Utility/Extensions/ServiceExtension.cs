using System;
using Logistics.Domain.Interfaces.ServiceInterface;
using Logistics.Domain.Services;
using Logistics.Infrastructure.Security.Authentication.Services;

namespace Logistics.WebApi.Utility.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<UserService>();
        }
    }
}

