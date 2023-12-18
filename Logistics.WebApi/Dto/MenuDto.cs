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
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Icon { get; set; }
    public string? Redirect { get; set; }
    public string? Component { get; set; }
    public ICollection<MenuListDto>? Routes { get; set; }
}

public record MenuPathListDto
{
    public int Id { get; set; }
    public string? Value { get; set; }
    public string? Label { get; set; }
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

// public record MenuItemDto(int Id, string? Name, int? ParentId, string? Path, string? Component, string? Icon, string? Redirect, bool HideInMenu);
// public class MenuDto
// {
//     public static MenuItemDto CreateMenuItem(Menu menu)
//     {
//         return new MenuItemDto(menu.Id, menu.Name, menu.ParentId, menu.Path, menu.Component, menu.Icon, menu.Redirect, menu.HideInMenu);
//     }
//     public static async Task<ICollection<MenuTreeDto>> CreateMenuTreeAsync(ICollection<Menu> menus)
//     {
//         var menuTree = await Task.WhenAll(menus.Select(async menu => new MenuTreeDto(
//             Key: menu.Id,
//             Parent: menu.ParentId,
//             Title: menu.Name,
//             Value: menu.Id.ToString(),
//             Order: menu.Order,
//             HidenInMenu: menu.HideInMenu,
//             Children: (menu.Children != null && menu.Children.Any())
//                 ? await CreateMenuTreeAsync(menu.Children)
//                 : null
//         )));

//         return menuTree.ToList();
//     }
// }
