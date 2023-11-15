using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class ReviewsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EmployeeID",
                table: "Reviews",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SoftwareID",
                table: "Reviews",
                column: "SoftwareID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_EmployeeID",
                table: "Reviews",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Softwares_SoftwareID",
                table: "Reviews",
                column: "SoftwareID",
                principalTable: "Softwares",
                principalColumn: "SoftwareID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_EmployeeID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Softwares_SoftwareID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EmployeeID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_SoftwareID",
                table: "Reviews");
        }
    }
}
