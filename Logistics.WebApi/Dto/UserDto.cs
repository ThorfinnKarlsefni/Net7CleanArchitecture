using System;
using Logistics.Domain.Entities.Identity;

namespace Logistics.WebApi.Dto
{
    public class UserDto
    {
        public record UserInfoDto(Guid Id, string? UserName, string? RoleName);

        public static UserInfoDto CreateUserInfo(User user, string? roleName)
        {
            return new UserInfoDto(user.UserId, user.UserName, roleName);
        }
    }
}

