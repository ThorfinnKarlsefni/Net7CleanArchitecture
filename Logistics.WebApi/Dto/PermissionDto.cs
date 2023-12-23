using System.Net.NetworkInformation;

namespace Logistics.WebApi;

public record PermissionListDto
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string? Name { get; set; }
    public string? HttpMethod { get; set; }
    public string? HttpPath { get; set; }
    public string? Order { get; set; }
    public List<PermissionController>? Children { get; set; }
}
