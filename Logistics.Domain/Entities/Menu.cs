
namespace Logistics.Domain;

public class Menu
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int Order { get; set; }
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public string? Redirect { get; set; }
    public string? Component { get; set; }
    public bool HideInMenu { get; set; }
    public ICollection<MenuRole>? MenuRoles { get; set; }
    public ICollection<Menu>? Children { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Menu()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
