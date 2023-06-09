using System;
using Logistics.Domain.Interfaces.RepositoryInterface;
using Logistics.Infrastructure.Repositories;

namespace Logistics.WebApi.Utility.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

