using System;
using System.Security.Claims;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.Dto.UserDto;
using static Logistics.WebApi.Requests.IdentityRequest;

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

        [HttpGet]
        public async Task<UserInfoDto> GetUserInfo()
        {
            if (!Guid.TryParse(this.User.FindFirstValue((ClaimTypes.NameIdentifier)), out Guid userId))
                throw new AggregateException("请重新登录");
            var (user, roles) = await _userService.GetUserInfo(userId).ConfigureAwait(false);
            string role = string.Join(",", roles);
            return UserDto.CreateUserInfo(user, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest req)
        {
            var res = await _userService.UpdateUserAsync(id, req.userName);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return NoContent();
        }
    }
}

