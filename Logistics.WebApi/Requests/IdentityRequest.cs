using System;
using FluentValidation;
using Logistics.WebApi.Helpers;

namespace Logistics.WebApi.Requests
{
    public class IdentityRequest
    {
        public record RegisterUserRequest(string userName, string? phoneNumber, string password, string confirmPassword);

        public class RegisterUserValidator : Requests<RegisterUserRequest>
        {
            public RegisterUserValidator()
            {
                RuleFor(e => e.userName).NotNull().NotEmpty().WithMessage("用户名不能为空");
                RuleFor(e => e.phoneNumber)
                        .NotEmpty()
                        .When(e => !string.IsNullOrEmpty(e.phoneNumber))
                        .Must(Helper.IsValidPhoneNumer)
                        .WithMessage("非法手机号");

                RuleFor(e => e.password).NotNull().NotEmpty().WithMessage("用户密码不能为空");
                RuleFor(e => e)
                        .Must(e => e.password == e.confirmPassword)
                        .WithMessage("两次密码不一致");
            }
        }

        public record LoginUserNameRequest(string userName, string password);

        public class LoginUserNameValidator : Requests<LoginUserNameRequest>
        {
            public LoginUserNameValidator()
            {
                RuleFor(e => e.userName).NotNull().NotEmpty().WithMessage("用户名不能为空");
                RuleFor(e => e.password).NotNull().NotEmpty().WithMessage("用户密码不能为空");
            }
        }

        public record ResetPasswordRequest(Guid userId, string oldPassword, string newPassword, string confirmPassword);

        public class ResetPasswordValidator : Requests<ResetPasswordRequest>
        {
            public ResetPasswordValidator()
            {
                RuleFor(e => e.userId).NotNull().NotEmpty().WithMessage("用户不存在");
                RuleFor(e => e.oldPassword).NotNull().NotEmpty().WithMessage("旧密码不能为空");
                RuleFor(e => e)
                    .Must(e => e.newPassword == e.confirmPassword)
                    .WithMessage("两次密码不一致");
            }
        }

        public record AddAndUpdateRoleRequest(string roleName);

        public class AddAndUpdateRoleValidator : Requests<AddAndUpdateRoleRequest>
        {
            public AddAndUpdateRoleValidator()
            {
                RuleFor(e => e.roleName)
                    .NotNull().NotEmpty().WithErrorCode("角色名称不能为空")
                    .Length(1, 6).WithMessage("角色名长度必须在1到6个之间");
            }
        }

        public record UpdateUserRequest(string userName);

        public class UpdateUserRoleValidator : Requests<UpdateUserRequest>
        {
            public UpdateUserRoleValidator()
            {
                RuleFor(e => e.userName)
                    .NotNull().NotEmpty().WithErrorCode("角色名称不能为空")
                    .Length(1, 6).WithMessage("用户名长度必须在1到6个之间");
            }
        }
    }
}

