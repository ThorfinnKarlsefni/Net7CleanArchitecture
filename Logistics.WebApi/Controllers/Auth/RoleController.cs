using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.WebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var res = await _roleService.CreateAsync(req.RoleName);
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
            var res = await _roleService.UpdateRoleAsync(id, req.RoleName);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return NoContent();
        }

        [HttpPost("{id}/User/{userId}")]
        public async Task<IActionResult> AddUserRole(string roleId, string userId)
        {
            var res = await _userService.AddToRoleAsync(userId, roleId).ConfigureAwait(false);
            if (!res.Succeeded)
                return BadRequest(res.Errors.First().Description);
            return Ok();
        }
    }
}

