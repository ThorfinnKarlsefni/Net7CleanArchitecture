namespace Logistics.Domain;

public class Permission
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? HttpMethod { get; set; }
    public string? HttpPath { get; set; }
    public int Order { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
