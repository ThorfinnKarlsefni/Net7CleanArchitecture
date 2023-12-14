using Logistics.Domain;

namespace Logistics.WebApi;

public class MenuRequest
{
    public record AddAndUpdateMenuRequest
    {
        public int Id { get; init; }
        public int ParentId { get; init; }
        public int Order { get; init; }
        public string? Name { get; init; }
        public string? Path { get; init; }
        public string? Component { get; init; }
        public string? Icon { get; init; }
        public string? Redirect { get; init; }
        public bool HideInMenu { get; init; }
        public List<string>? MenuRoles { get; init; }
    }


    public record UpdateMenuTree
    {
        public int Parent { get; set; }
        public Boolean DropToGap { get; set; }
        public int DropKey { get; set; }
        public int DropPosition { get; set; }
    }
}
