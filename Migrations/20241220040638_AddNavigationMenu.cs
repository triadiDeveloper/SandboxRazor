using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandboxRazor.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Navigation",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)"),
                    ModifiedUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navigation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavigationRole",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NavigationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CAST(sysdatetimeoffset() AT TIME ZONE 'SE Asia Standard Time' AS DATETIME)"),
                    ModifiedUser = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavigationRole_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "ApplicationRole",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NavigationRole_Navigation_NavigationId",
                        column: x => x.NavigationId,
                        principalSchema: "Identity",
                        principalTable: "Navigation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Navigation_Code",
                schema: "Identity",
                table: "Navigation",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Navigation_Name_Icon_Url_ParentCode",
                schema: "Identity",
                table: "Navigation",
                columns: new[] { "Name", "Icon", "Url", "ParentCode" });

            migrationBuilder.CreateIndex(
                name: "IX_NavigationRole_NavigationId",
                schema: "Identity",
                table: "NavigationRole",
                column: "NavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationRole_RoleId",
                schema: "Identity",
                table: "NavigationRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavigationRole",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Navigation",
                schema: "Identity");
        }
    }
}
