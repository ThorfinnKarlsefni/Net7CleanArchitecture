using System;
using FluentValidation;
using Logistics.WebApi.Requests;
using static Logistics.WebApi.Requests.UserRequest;

namespace Logistics.WebApi.Utility.Extensions
{
    public static class ValidatorExtension
    {
        public static void AddCustomValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginByPhoneRequest>, LoginByPhoneValidator>();
            services.AddScoped<IValidator<LoginByUserNameRequest>, LoginByUserNameValidator>();

        }
    }
}

