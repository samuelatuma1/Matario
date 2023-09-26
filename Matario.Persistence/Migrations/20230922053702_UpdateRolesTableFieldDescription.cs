using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matario.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesTableFieldDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desceiption",
                table: "Roles",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Roles",
                newName: "Desceiption");
        }
    }
}
