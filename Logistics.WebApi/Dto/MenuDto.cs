using Logistics.Domain;

namespace Logistics.WebApi;

public record MenuTreeDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public int Order { get; set; }
    public bool? HideInMenu { get; set; }
    public ICollection<MenuTreeDto>? Children { get; set; }
}

public record MenuListDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public string? Redirect { get; set; }
    public string? Component { get; set; }
    public ICollection<MenuListDto>? Routes { get; set; }
}

public class MenuItemDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public string? Path { get; set; }
    public string? Component { get; set; }
    public string? Icon { get; set; }
    public string? Redirect { get; set; }
    public bool HideInMenu { get; set; }
}

