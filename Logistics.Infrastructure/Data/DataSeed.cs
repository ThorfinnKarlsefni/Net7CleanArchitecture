using Logistics.Domain;
using Logistics.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure;

public static class DataSeed
{

    public static void Seed(ModelBuilder builder)
    {
        var userId = Guid.NewGuid();
        var roleId = Guid.NewGuid();
        SeedUsers(builder, userId);
        SeedRoles(builder, roleId);
        SeedUserRoles(builder, userId, roleId);
        SeedMenus(builder);
    }

    private static void SeedUsers(ModelBuilder builder, Guid userId)
    {
        var userName = "cheung";
        builder.Entity<User>().HasData(
        new User(userName)
        {
            Id = userId,
            Avatar = "http://avatar.xhwt56.com/5eaf95c210fa76978d58fec9b9d9e8ba.avif",
            NormalizedUserName = "CHEUNG",
            EmailConfirmed = false,
            PasswordHash = "AQAAAAIAAYagAAAAEHP2wX5FQce1oLhn4nv9Re16m5Km5INhOdGN3tTvEiW8ZnJY7N7bR9k4wJJ/wz6YxQ==",
            SecurityStamp = "G4UUUI4DO6ORH4NZUEM7FT3NJVBUUEQG",
            ConcurrencyStamp = "32713740-f8dd-43aa-85f5-7891210af0ef",
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0,
        });
    }
    private static void SeedRoles(ModelBuilder builder, Guid roleId)
    {
        builder.Entity<Role>().HasData(
          new Role()
          {
              Id = roleId,
              Name = "Admin",
              NormalizedName = "ADMIN",
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
          });
    }

    private static void SeedUserRoles(ModelBuilder builder, Guid userId, Guid roleId)
    {
        builder.Entity<UserRole>().HasData(
           new UserRole()
           {
               UserId = userId,
               RoleId = roleId
           });
    }

    private static void SeedMenus(ModelBuilder builder)
    {
        builder.Entity<Menu>().HasData(
            new Menu
            {
                Id = 1,
                Path = "/admin",
                Name = "系统",
                // Icon = "crown",
            },
             new Menu { Id = 2, ParentId = 1, Path = "/admin/users", Name = "员工列表", Component = "./Admin/Users" },
            new Menu { Id = 3, ParentId = 1, Path = "/admin/menu", Name = "菜单管理", Component = "./Admin/Menu" },
            new Menu { Id = 4, ParentId = 1, Path = "/admin/permission", Name = "权限管理", Component = "./Admin/Permission" },
            new Menu { Id = 5, ParentId = 1, Path = "/admin/role", Name = "角色管理", Component = "./Admin/Role" },
            new Menu { Id = 6, ParentId = 1, Path = "/admin/station", Name = "站点管理", Component = "./Admin/Station" },
            new Menu
            {
                Id = 7,
                Path = "/transport",
                Name = "运输管理",
                // Icon = "car",
            },
            new Menu { Id = 8, ParentId = 7, Path = "/transport/invoices", Name = "收货开票", Component = "./Transport/Invoices" }
        );
    }
}
