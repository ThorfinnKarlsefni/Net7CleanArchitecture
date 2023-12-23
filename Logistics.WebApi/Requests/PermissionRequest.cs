namespace Logistics.WebApi;

public class PermissionRequest
{
    public record PermissionAddRequest
    {
        public string? Name { get; init; }
        public int? ParentId { get; init; }
        public List<string> HttpMethod { get; init; } = new List<string> { };
        public List<string> HttpPath { get; init; } = new List<string> { };
    }
}
