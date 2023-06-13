using System;
using FluentValidation;
using Logistics.WebApi.Helpers;

namespace Logistics.WebApi.Requests
{
    public class UserRequest
    {
        public record RegisterUserRequest(string userName, string? phoneNumber, string passwordHash, string confirmPassword);

        public class RegisterUserValidator : ValidationHelper<RegisterUserRequest>
        {
            public RegisterUserValidator()
            {
                RuleFor(e => e.userName).NotNull().NotEmpty().WithMessage("用户名不能为空");
                RuleFor(e => e.phoneNumber)
                    .NotEmpty()
                    .When(e => !string.IsNullOrEmpty(e.phoneNumber))
                    .Must(Helper.IsValidPhoneNumer)
                    .WithMessage("非法手机号");

                RuleFor(e => e.passwordHash).NotNull().NotEmpty().WithMessage("用户密码不能为空");
                RuleFor(e => e)
                    .Must(e => e.passwordHash == e.confirmPassword)
                    .WithMessage("两次密码不一致");
            }
        }
    }
}

