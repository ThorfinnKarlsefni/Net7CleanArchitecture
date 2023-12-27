namespace Logistics.WebApi;

public class PermissionRequest
{
    public record PermissionAddAndUpdateRequest
    {
        public string? Name { get; init; }
        public int? ParentId { get; init; }
        public List<string> HttpMethod { get; set; } = new List<string> { };
        public List<string> HttpPath { get; set; } = new List<string> { };
    }
}
