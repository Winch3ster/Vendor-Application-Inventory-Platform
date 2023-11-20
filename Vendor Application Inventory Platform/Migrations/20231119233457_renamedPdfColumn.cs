using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccessFieldsInEmployeecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "companyId",
                table: "PdfDocuments",
                newName: "softwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "softwareId",
                table: "PdfDocuments",
                newName: "companyId");
        }
    }
}
