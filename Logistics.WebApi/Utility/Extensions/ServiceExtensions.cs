using FluentValidation;
using Logistics.Domain;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Infrastructure;
using Logistics.Infrastructure.Authentication;
using Logistics.Infrastructure.Repositories;
using Logistics.WebApi.Requests;
using static Logistics.WebApi.Requests.IdentityRequest;
using static Logistics.WebApi.Requests.WaybillRequest;

namespace Logistics.WebApi.Utility.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
            services.AddScoped<IConsigneeRepository, ConsigneeRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IWaybillRepository, WaybillRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IConsigneeService, ConsigneeService>();
            services.AddScoped<IWaybillService, WaybillService>();
        }

        public static void AddCustomValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginUserNameRequest>, LoginUserNameValidator>();
            services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordValidator>();
            services.AddScoped<IValidator<AddAndUpdateRoleRequest>, AddAndUpdateRoleValidator>();
            services.AddScoped<IValidator<InvoiceRequest>, InvoiceRequestValidator>();
        }
    }
}

