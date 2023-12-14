using System;
using FluentValidation;
using Logistics.WebApi.Helpers;

namespace Logistics.WebApi.Requests
{
    public record RegisterUserRequest(string UserName, string? PhoneNumber, string Password, string ConfirmPassword);

    public record LoginUserNameRequest(string UserName, string Password);

    public record ResetPasswordRequest(string OldPassword, string NewPassword, string ConfirmPassword);

    public record AddAndUpdateRoleRequest(string RoleName);

    public record UpdateUserRequest(string UserName);

    public class IdentityRequest
    {

        public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
        {
            public RegisterUserValidator()
            {
                RuleFor(e => e.UserName).NotNull().NotEmpty().WithMessage("用户名不能为空");
                RuleFor(e => e.PhoneNumber)
                        .NotEmpty()
                        .When(e => !string.IsNullOrEmpty(e.PhoneNumber))
                        .Must(Helper.IsValidPhoneNumber)
                        .WithMessage("非法手机号");

                RuleFor(e => e.Password).NotNull().NotEmpty().WithMessage("用户密码不能为空");
                RuleFor(e => e)
                        .Must(e => e.Password == e.ConfirmPassword)
                        .WithMessage("两次密码不一致");
            }
        }

        public class LoginUserNameValidator : AbstractValidator<LoginUserNameRequest>
        {
            public LoginUserNameValidator()
            {
                RuleFor(e => e.UserName).NotNull().NotEmpty().WithMessage("用户名不能为空");
                RuleFor(e => e.Password).NotNull().NotEmpty().WithMessage("用户密码不能为空");
            }
        }

        public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
        {
            public ResetPasswordValidator()
            {
                RuleFor(e => e.OldPassword).NotNull().NotEmpty().WithMessage("旧密码不能为空");
                RuleFor(e => e)
                    .Must(e => e.NewPassword == e.ConfirmPassword)
                    .WithMessage("两次密码不一致");
            }
        }

        public class AddAndUpdateRoleValidator : AbstractValidator<AddAndUpdateRoleRequest>
        {
            public AddAndUpdateRoleValidator()
            {
                RuleFor(e => e.RoleName)
                    .NotNull().NotEmpty().WithErrorCode("角色名称不能为空")
                    .Length(1, 16).WithMessage("角色名长度必须在1到16个字符之间");
            }
        }

        public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRequest>
        {
            public UpdateUserRoleValidator()
            {
                RuleFor(e => e.UserName)
                    .NotNull().NotEmpty().WithErrorCode("角色名称不能为空")
                    .Length(1, 6).WithMessage("用户名长度必须在1到6个之间");
            }
        }
    }
}

