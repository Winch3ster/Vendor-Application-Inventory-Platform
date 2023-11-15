using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class removedSoftwareCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessAreas",
                keyColumn: "BusinessAreaID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Software_Areas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Software_Areas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BusinessAreas",
                keyColumn: "BusinessAreaID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "SoftwareID",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_CompanyID",
                table: "Softwares",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Companies_CompanyID",
                table: "Softwares",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Companies_CompanyID",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_CompanyID",
                table: "Softwares");

            migrationBuilder.InsertData(
                table: "BusinessAreas",
                columns: new[] { "BusinessAreaID", "Description" },
                values: new object[,]
                {
                    { 1, "Business Area 1" },
                    { 2, "Business Area 2" }
                });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "SoftwareID", "Cloud", "CompanyID", "Description", "DocumentAttached", "SoftwareName" },
                values: new object[,]
                {
                    { 1, 0, 1, "Description 1", false, "Software 1" },
                    { 2, 0, 1, "Description 2", false, "Software 2" }
                });

            migrationBuilder.InsertData(
                table: "Software_Areas",
                columns: new[] { "Id", "areaID", "softwareID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });
        }
    }
}
