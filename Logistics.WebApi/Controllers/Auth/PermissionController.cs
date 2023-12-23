using AutoMapper;
using Logistics.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IActionResult> AddAsync([FromBody] PermissionAddRequest req)
    {
        var permission = new Permission
        {
            ParentId = req.ParentId ?? null,
            HttpMethod = string.Join(",", req.HttpMethod),
            HttpPath = string.Join(",", req.HttpPath),
            Name = req.Name
        };

        await _permissionRepository.AddPermissionAsync(permission);
        return Ok();
    }
}
