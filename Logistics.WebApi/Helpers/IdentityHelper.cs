using System;
using Logistics.Domain.Entities.Identity;
using Logistics.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Logistics.WebApi.Helpers
{
    public class IdentityHelper
    {
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<XhwtDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
        }
    }
}

