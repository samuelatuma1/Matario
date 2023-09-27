using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PermissionsAddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesPermissions_Permission_PermissionId",
                table: "RolesPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4040), new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8430), new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.AddForeignKey(
                name: "FK_RolesPermissions_Permissions_PermissionId",
                table: "RolesPermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolesPermissions_Permissions_PermissionId",
                table: "RolesPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permission");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 26, 21, 31, 23, 93, DateTimeKind.Utc).AddTicks(8320), new DateTime(2023, 9, 26, 21, 31, 23, 93, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 26, 21, 31, 23, 104, DateTimeKind.Utc).AddTicks(6400), new DateTime(2023, 9, 26, 21, 31, 23, 104, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.AddForeignKey(
                name: "FK_RolesPermissions_Permission_PermissionId",
                table: "RolesPermissions",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
