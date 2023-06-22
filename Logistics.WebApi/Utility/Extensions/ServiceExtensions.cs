using System;
using FluentValidation;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Infrastructure.Authentication;
using Logistics.Infrastructure.Repositories;
using Logistics.WebApi.Requests;
using static Logistics.WebApi.Requests.IdentityRequest;

namespace Logistics.WebApi.Utility.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

        }

        public static void AddCustomValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserNameRequest>, LoginUserNameValidator>();
            services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordValidator>();
            services.AddScoped<IValidator<AddAndUpdateRoleRequest>, AddAndUpdateRoleValidator>();
        }
    }
}

