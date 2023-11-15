using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class changedFinancialDbSetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Addresses");
        }
    }
}
