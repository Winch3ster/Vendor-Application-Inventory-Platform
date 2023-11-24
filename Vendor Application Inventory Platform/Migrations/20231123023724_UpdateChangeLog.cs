using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChangeLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangesDescription",
                table: "changeLogs",
                newName: "ActionPerformed");

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "changeLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "changeLogs");

            migrationBuilder.RenameColumn(
                name: "ActionPerformed",
                table: "changeLogs",
                newName: "ChangesDescription");
        }
    }
}
