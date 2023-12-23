using System.Security.Claims;
using AutoMapper;
using Logistics.Domain;
using Microsoft.AspNetCore.Mvc;
using static Logistics.WebApi.MenuRequest;

namespace Logistics.WebApi;

[Route("[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMenuRepository _menuRepository;
    private readonly IMapper _mapper;
    public MenuController(IMenuRepository menuRepository, IMapper mapper)
    {
        _menuRepository = menuRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<MenuListDto>>? GetMenus()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new AggregateException("用户ID无效");
        var allMenus = await _menuRepository.GetMenuListByUserIdAsync(userId);
        var rootMenus = _mapper.Map<List<MenuListDto>>(await BuildRootMenuAsync(allMenus));

        return rootMenus;
    }

    [HttpGet("{id}")]
    public async Task<MenuItemDto> GetMenu(int id)
    {
        var menu = await _menuRepository.FindMenuAsync(id);
        return _mapper.Map<MenuItemDto>(menu);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddAndUpdateMenuRequest req)
    {
        var menu = _mapper.Map<Menu>(req);
        await _menuRepository.AddMenuAsync(menu, req.MenuRoles);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _menuRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AddAndUpdateMenuRequest req)
    {
        var updateMenu = _mapper.Map<Menu>(req);
        await _menuRepository.UpdateMenuAsync(id, updateMenu, req.MenuRoles);
        return NoContent();
    }

    [HttpGet("Tree")]
    public async Task<List<MenuTreeDto?>> Tree()
    {
        var allMenus = await _menuRepository.GetMenuListAsync();
        return _mapper.Map<List<MenuTreeDto?>>(await BuildRootMenuAsync(allMenus));
    }

    [HttpGet("Permission/menus")]
    public async Task<List<MenuListDto?>> PathList()
    {
        return _mapper.Map<List<MenuListDto?>>(await _menuRepository.GetMenuPathListAsync());
    }

    [HttpPut("Tree/{id}")]
    public async Task<IActionResult> UpdateDragTree(int id, [FromBody] UpdateMenuTree req)
    {
        await _menuRepository.UpdateMenuTreeAsync(id, req.Parent, req.DropToGap, req.DropKey, req.DropPosition);
        return NoContent();
    }

    [HttpPut("Visibility/{id}")]
    public async Task<IActionResult> MenuVisibility(int id)
    {
        await _menuRepository.UpdateMenuVisibilityAsync(id);
        return NoContent();
    }

    /// <summary>
    /// This method should be implemented in the MenuServices layer
    /// </summary>
    /// <param name="allMenus"></param>
    /// <returns></returns>
    private async Task<List<Menu>> BuildRootMenuAsync(List<Menu> allMenus)
    {
        var menuDictionary = allMenus
            .Where(menu => menu.ParentId != 0)
            .GroupBy(menu => menu.ParentId)
            .ToDictionary(group => group.Key, group => group.ToList());

        var rootMenus = await Task.WhenAll(allMenus
            .Where(menu => menu.ParentId == 0)
            .Select(async rootMenu => await BuildMenuTreeAsync(rootMenu, menuDictionary)));

        return rootMenus.ToList();
    }

    /// <summary>
    ///  This method should be implemented in the MenuService layer
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="menuDictionary"></param>
    /// <returns></returns>
    private async Task<Menu> BuildMenuTreeAsync(Menu menu, Dictionary<int, List<Menu>> menuDictionary)
    {
        if (menuDictionary.ContainsKey(menu.Id))
        {
            menu.Children = (await Task.WhenAll(menuDictionary[menu.Id]
                .Select(async childMenu => await BuildMenuTreeAsync(childMenu, menuDictionary))))
                .ToList();
        }

        return menu;
    }
}
