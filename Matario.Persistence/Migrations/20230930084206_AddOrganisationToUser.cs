using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganisationToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganisationId1",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 569, DateTimeKind.Utc).AddTicks(2490), new DateTime(2023, 9, 30, 8, 42, 6, 569, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 569, DateTimeKind.Utc).AddTicks(2600), new DateTime(2023, 9, 30, 8, 42, 6, 569, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(400), new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(420), new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(430), new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(440), new DateTime(2023, 9, 30, 8, 42, 6, 576, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganisationId1",
                table: "Users",
                column: "OrganisationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organisations_OrganisationId1",
                table: "Users",
                column: "OrganisationId1",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organisations_OrganisationId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganisationId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganisationId1",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1370), new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1470), new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(540), new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(540) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(650), new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660), new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660), new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660) });
        }
    }
}
