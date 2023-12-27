using AutoMapper;
using Logistics.Domain;

using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.PermissionRequest;

namespace Logistics.WebApi;

[Route("[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public PermissionController(IPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<PermissionListDto>> Get()
    {
        return _mapper.Map<List<PermissionListDto>>(await _permissionRepository.GetPermissionsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PermissionAddAndUpdateRequest req)
    {
        var permission = _mapper.Map<Permission>(req);
        await _permissionRepository.AddPermissionAsync(permission);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] PermissionAddAndUpdateRequest req)
    {
        var permission = _mapper.Map<Permission>(req);
        permission.Id = id;
        await _permissionRepository.UpdatePermissionAsync(permission);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _permissionRepository.DeletePermissionAsync(id);
        return NoContent();
    }
}
