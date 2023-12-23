using Logistics.Domain.Entities.Identity;

namespace Logistics.Domain;

public class MenuRole
{
    public int Id { get; set; }
    public int MenuId { get; set; }
    public Menu? Menu { get; set; }

    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public MenuRole()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
    }
}
