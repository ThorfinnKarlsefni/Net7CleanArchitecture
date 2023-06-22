using System;
using System.Xml.Linq;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.Requests.IdentityRequest;

namespace Logistics.WebApi.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;

        public RoleController(IRoleService roleService, IRoleRepository roleRepository, IUserService userService)
        {
            _roleService = roleService;
            _roleRepository = roleRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddAndUpdateRoleRequest req)
        {
            var res = await _roleService.CreateAsync(req.roleName);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var res = await _roleService.DeleteRoleAsync(id);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] AddAndUpdateRoleRequest req)
        {
            var res = await _roleService.UpdateRoleAsync(id, req.roleName);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return NoContent();
        }

        [HttpPost("{id}/User/{userId}")]
        public async Task<IActionResult> AddUserRole(string id, string userId)
        {
            if (!Guid.TryParse(userId, out Guid user))
                return BadRequest("用户标识不存在");

            var res = await _userService.AddToRoleAsync(user, id).ConfigureAwait(false);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);

            return Ok();
        }
    }
}

