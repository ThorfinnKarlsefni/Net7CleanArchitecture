using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Logistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankName = table.Column<string>(type: "varchar(20)", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consignees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(20)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(256)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Path = table.Column<string>(type: "varchar(50)", nullable: true),
                    Icon = table.Column<string>(type: "varchar(50)", nullable: true),
                    Redirect = table.Column<string>(type: "varchar(50)", nullable: true),
                    Component = table.Column<string>(type: "varchar(50)", nullable: true),
                    HideInMenu = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    Slug = table.Column<string>(type: "text", nullable: true),
                    HttpMethod = table.Column<string>(type: "varchar(191)", nullable: true),
                    HttpPath = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(256)", nullable: false),
                    IdCard = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(256)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    TokenVersion = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuRoles_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waybills",
                columns: table => new
                {
                    WaybillId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShipperId = table.Column<int>(type: "integer", nullable: false),
                    ConsigneeId = table.Column<int>(type: "integer", nullable: false),
                    ToStation = table.Column<int>(type: "integer", nullable: false),
                    TransitStation = table.Column<int>(type: "integer", nullable: true),
                    Address = table.Column<string>(type: "varchar(256)", nullable: false),
                    CargoName = table.Column<string>(type: "varchar(20)", nullable: false),
                    CargoId = table.Column<string>(type: "varchar(256)", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Volume = table.Column<float>(type: "real", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Packing = table.Column<int>(type: "integer", nullable: true),
                    CargoValue = table.Column<int>(type: "integer", nullable: true),
                    InsuranceFee = table.Column<decimal>(type: "numeric", nullable: true),
                    FreightFee = table.Column<decimal>(type: "numeric", nullable: false),
                    CodcFee = table.Column<decimal>(type: "numeric", nullable: true),
                    BackFreightFee = table.Column<decimal>(type: "numeric", nullable: true),
                    DeliveryFee = table.Column<decimal>(type: "numeric", nullable: true),
                    NotificationFee = table.Column<decimal>(type: "numeric", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(256)", nullable: true),
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    BargainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PayAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybills", x => x.WaybillId);
                    table.ForeignKey(
                        name: "FK_Waybills_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Waybills_Consignees_ConsigneeId",
                        column: x => x.ConsigneeId,
                        principalTable: "Consignees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waybills_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waybills_Users_BargainerId",
                        column: x => x.BargainerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Waybills_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Component", "CreatedAt", "HideInMenu", "Icon", "Name", "Order", "ParentId", "Path", "Redirect", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9550), false, "crown", "系统", 0, 0, "/admin", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9550) },
                    { 2, "./Admin/Users", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, null, "员工列表", 0, 1, "/admin/users", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 3, "./Admin/Menu", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, null, "菜单管理", 0, 1, "/admin/menu", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 4, "./Admin/Permission", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, null, "权限管理", 0, 1, "/admin/permission", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 5, "./Admin/Role", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, null, "角色管理", 0, 1, "/admin/role", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 6, "./Admin/Station", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, null, "站点管理", 0, 1, "/admin/station", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 7, null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560), false, "car", "运输管理", 0, 0, "/transport", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9560) },
                    { 8, "./Transport/Invoices", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9570), false, null, "发票管理", 0, 7, "/transport/invoices", null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9570) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { new Guid("4f0a35f2-72c0-4928-8ebb-23647c3f807b"), null, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9530), "Admin", "ADMIN", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9530) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "CreatedAt", "DeletedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("4ee30eb7-4985-4e2c-a4fd-511d9c56717b"), 0, "http://avatar.xhwt56.com/5eaf95c210fa76978d58fec9b9d9e8ba.avif", "32713740-f8dd-43aa-85f5-7891210af0ef", new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9420), null, null, false, true, null, null, "CHEUNG", "AQAAAAIAAYagAAAAEHP2wX5FQce1oLhn4nv9Re16m5Km5INhOdGN3tTvEiW8ZnJY7N7bR9k4wJJ/wz6YxQ==", null, false, "G4UUUI4DO6ORH4NZUEM7FT3NJVBUUEQG", false, new DateTime(2023, 12, 14, 21, 39, 58, 560, DateTimeKind.Local).AddTicks(9420), "cheung" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("4f0a35f2-72c0-4928-8ebb-23647c3f807b"), new Guid("4ee30eb7-4985-4e2c-a4fd-511d9c56717b") });

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_MenuId",
                table: "MenuRoles",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoles_RoleId",
                table: "MenuRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_BankId",
                table: "Waybills",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_BargainerId",
                table: "Waybills",
                column: "BargainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_ConsigneeId",
                table: "Waybills",
                column: "ConsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_CreatorId",
                table: "Waybills",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_ShipperId",
                table: "Waybills",
                column: "ShipperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuRoles");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Waybills");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Consignees");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
