﻿using System;
using System.Net;
using Logistics.Domain.Entities.Identity;
using Logistics.Domain.Interfaces;
using Logistics.Domain.Interfaces.RepositoryInterface;
using Logistics.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.Requests.UserRequest;

namespace Logistics.WebApi.Controllers.Auth
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public AuthController(IUserRepository userRepository, UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest req)
        {
            if (await _userRepository.FindByNameAsync(req.userName) != null)
                return BadRequest("用户已存在");
            User user = new User(req.userName);

            var result = await _userRepository.CreateAsync(user, req.passwordHash).ConfigureAwait(false);
            if (!result.Succeeded)
                return BadRequest("请稍后再试");
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string?>> LoginByPhoneAndPwd(string userName, string password)
        {
            (var checkResult, string? token) = await _userService.LoginByUserNameAndPwdAsync(userName, password);
            if (checkResult.Succeeded)
                return token;
            if (checkResult.IsLockedOut)
                return StatusCode((int)HttpStatusCode.Locked, "此帐号已经锁定");

            return BadRequest("登录失败");
        }


    }
}
