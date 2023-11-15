using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSetForSoftwareArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_EmployeeID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Softwares_SoftwareID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Companies_CompanyID",
                table: "Softwares");

            migrationBuilder.DropTable(
                name: "BusinessAreaSoftware");

            migrationBuilder.DropTable(
                name: "CompanyCountry");

            migrationBuilder.DropTable(
                name: "SoftwareSoftwareModule");

            migrationBuilder.DropTable(
                name: "SoftwareSoftwareType");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_CompanyID",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EmployeeID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_SoftwareID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "FinancialServicesClientType",
                table: "Softwares");

            migrationBuilder.CreateTable(
                name: "Software_Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    areaID = table.Column<int>(type: "int", nullable: false),
                    softwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_Areas_BusinessAreas_areaID",
                        column: x => x.areaID,
                        principalTable: "BusinessAreas",
                        principalColumn: "BusinessAreaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_Areas_Softwares_softwareID",
                        column: x => x.softwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Areas_areaID",
                table: "Software_Areas",
                column: "areaID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Areas_softwareID",
                table: "Software_Areas",
                column: "softwareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Software_Areas");

            migrationBuilder.DropIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "FinancialServicesClientType",
                table: "Softwares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BusinessAreaSoftware",
                columns: table => new
                {
                    BusinessAreasBusinessAreaID = table.Column<int>(type: "int", nullable: false),
                    SoftwaresSoftwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAreaSoftware", x => new { x.BusinessAreasBusinessAreaID, x.SoftwaresSoftwareID });
                    table.ForeignKey(
                        name: "FK_BusinessAreaSoftware_BusinessAreas_BusinessAreasBusinessAreaID",
                        column: x => x.BusinessAreasBusinessAreaID,
                        principalTable: "BusinessAreas",
                        principalColumn: "BusinessAreaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessAreaSoftware_Softwares_SoftwaresSoftwareID",
                        column: x => x.SoftwaresSoftwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCountry",
                columns: table => new
                {
                    CompaniesCompanyID = table.Column<int>(type: "int", nullable: false),
                    CountriesCountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCountry", x => new { x.CompaniesCompanyID, x.CountriesCountryID });
                    table.ForeignKey(
                        name: "FK_CompanyCountry_Companies_CompaniesCompanyID",
                        column: x => x.CompaniesCompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCountry_Countries_CountriesCountryID",
                        column: x => x.CountriesCountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareSoftwareModule",
                columns: table => new
                {
                    SoftwareModulesSoftwareModuleID = table.Column<int>(type: "int", nullable: false),
                    SoftwaresSoftwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareSoftwareModule", x => new { x.SoftwareModulesSoftwareModuleID, x.SoftwaresSoftwareID });
                    table.ForeignKey(
                        name: "FK_SoftwareSoftwareModule_SoftwareModules_SoftwareModulesSoftwareModuleID",
                        column: x => x.SoftwareModulesSoftwareModuleID,
                        principalTable: "SoftwareModules",
                        principalColumn: "SoftwareModuleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareSoftwareModule_Softwares_SoftwaresSoftwareID",
                        column: x => x.SoftwaresSoftwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareSoftwareType",
                columns: table => new
                {
                    SoftwareTypesSoftwareTypeID = table.Column<int>(type: "int", nullable: false),
                    SoftwaresSoftwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareSoftwareType", x => new { x.SoftwareTypesSoftwareTypeID, x.SoftwaresSoftwareID });
                    table.ForeignKey(
                        name: "FK_SoftwareSoftwareType_SoftwareTypes_SoftwareTypesSoftwareTypeID",
                        column: x => x.SoftwareTypesSoftwareTypeID,
                        principalTable: "SoftwareTypes",
                        principalColumn: "SoftwareTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareSoftwareType_Softwares_SoftwaresSoftwareID",
                        column: x => x.SoftwaresSoftwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_CompanyID",
                table: "Softwares",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EmployeeID",
                table: "Reviews",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SoftwareID",
                table: "Reviews",
                column: "SoftwareID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers",
                column: "CityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAreaSoftware_SoftwaresSoftwareID",
                table: "BusinessAreaSoftware",
                column: "SoftwaresSoftwareID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCountry_CountriesCountryID",
                table: "CompanyCountry",
                column: "CountriesCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSoftwareModule_SoftwaresSoftwareID",
                table: "SoftwareSoftwareModule",
                column: "SoftwaresSoftwareID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSoftwareType_SoftwaresSoftwareID",
                table: "SoftwareSoftwareType",
                column: "SoftwaresSoftwareID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Companies_CompanyID",
                table: "Softwares",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
