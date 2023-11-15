using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanyCountryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company_Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyID = table.Column<int>(type: "int", nullable: false),
                    countryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Country_Companies_companyID",
                        column: x => x.companyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_Country_Countries_countryID",
                        column: x => x.countryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Country_companyID",
                table: "Company_Country",
                column: "companyID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Country_countryID",
                table: "Company_Country",
                column: "countryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_Country");
        }
    }
}
