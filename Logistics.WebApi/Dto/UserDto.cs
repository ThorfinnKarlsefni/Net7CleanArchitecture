using System;
using Logistics.Domain.Entities.Identity;

namespace Logistics.WebApi.Dto
{
    public record UserInfoDto(Guid Id, string? Username, string? PhoneNumber, string? Avatar, string? RoleName);

    public class UserDto
    {
        public static UserInfoDto CreateUserInfo(User user, string? roleName)
        {
            return new UserInfoDto(user.Id, user.UserName, user.PhoneNumber, user.Avatar, roleName);
        }
    }
}