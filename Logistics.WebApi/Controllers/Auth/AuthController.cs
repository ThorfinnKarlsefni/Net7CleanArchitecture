using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.WebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.WebApi.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AuthController(IUserService userService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest req)
        {
            if (await _userRepository.GetByNameAsync(req.UserName) != null)
                return BadRequest("用户已存在");
            User user = new User(req.UserName);
            var result = await _userRepository.CreateAsync(user, req.Password);
            if (!result.Succeeded)
                return BadRequest("创建失败!请联系管理员");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginByUserName([FromBody] LoginUserNameRequest req)
        {
            var user = await _userRepository.FindByNameAsync(req.UserName);
            var result = await _userRepository.CheckPasswordAsync(user, req.Password);
            if (!result)
                return BadRequest("用户名或密码错误");
            return Ok(await _userService.GenerateTokensAsync(user));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return Ok("Successfully");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest req)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var resetPwdResult = await _userService.ResetPasswordAsync(userId, req.OldPassword, req.NewPassword);
            if (!resetPwdResult.Succeeded)
                return BadRequest(resetPwdResult.Errors.FirstOrDefault()?.Description);

            return NoContent();
        }
    }
}

