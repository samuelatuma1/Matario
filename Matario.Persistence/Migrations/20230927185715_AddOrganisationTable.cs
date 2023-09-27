using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Matario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganisationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganisationId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1370), "CreateManager", new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "RecordStatus", "UpdatedAt" },
                values: new object[] { 2L, new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1470), null, "Allows users with permission to create Managers", "DeleteManager", 1, new DateTime(2023, 9, 27, 18, 57, 14, 714, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(540), new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(540) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "RecordStatus", "UpdatedAt" },
                values: new object[,]
                {
                    { 2L, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(650), 0L, "Admin Priviledges", "Admin", 1, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(650) },
                    { 3L, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660), 0L, "CorporateAdmin Priviledges", "CorporateAdmin", 1, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660) },
                    { 4L, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660), 0L, "Manager Priviledges", "Manager", 1, new DateTime(2023, 9, 27, 18, 57, 14, 721, DateTimeKind.Utc).AddTicks(660) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganisationId",
                table: "Users",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_Name",
                table: "Organisations",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organisations_OrganisationId",
                table: "Users",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organisations_OrganisationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganisationId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4040), "Create Manager", new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8430), new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8440) });
        }
    }
}
