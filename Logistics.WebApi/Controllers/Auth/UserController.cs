using System;
using System.Security.Claims;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.WebApi.Dto;
using Logistics.WebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.WebApi.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;


        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<UserInfoDto> CurrentUser()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var (user, roles) = await _userService.GetUserInfo(userId).ConfigureAwait(false);
            string role = string.Join(",", roles);
            return UserDto.CreateUserInfo(user, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest req)
        {
            var res = await _userService.UpdateUserAsync(id, req.UserName);
            if (!res.Succeeded)
                throw new OperationCanceledException(res.Errors.ToString());
            return NoContent();
        }
    }
}

