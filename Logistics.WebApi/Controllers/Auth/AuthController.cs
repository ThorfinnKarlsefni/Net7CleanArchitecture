using System;
using System.Data;
using System.Security.Claims;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.Requests.IdentityRequest;

namespace Logistics.WebApi.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest req)
        {
            if (await _userRepository.GetByNameAsync(req.userName).ConfigureAwait(false) != null)
                return BadRequest("用户已存在");
            User user = new User(req.userName);
            var result = await _userRepository.CreateAsync(user, req.password).ConfigureAwait(false);
            if (!result.Succeeded)
                return BadRequest("请稍后再试");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginByUserName([FromBody] LoginUserNameRequest req)
        {
            var user = await _userRepository.FindByNameAsync(req.userName).ConfigureAwait(false);
            var result = await _userRepository.CheckPasswordAsync(user, req.password).ConfigureAwait(false);
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
            //if (!Guid.TryParse(this.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            var resetPwdReseult = await _userService.ResetPasswordAsync(req.userId, req.oldPassword, req.newPassword);
            if (!resetPwdReseult.Succeeded)
                return BadRequest(resetPwdReseult.Errors.FirstOrDefault()?.Description);

            return NoContent();
        }
    }
}

