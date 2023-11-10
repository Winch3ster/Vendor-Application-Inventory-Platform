using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessAreas",
                columns: table => new
                {
                    BusinessAreaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAreas", x => x.BusinessAreaID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfEmployee = table.Column<int>(type: "int", nullable: false),
                    InternalProfessionalServices = table.Column<bool>(type: "bit", nullable: false),
                    LastDemoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareModules",
                columns: table => new
                {
                    SoftwareModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareModules", x => x.SoftwareModuleID);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareTypes",
                columns: table => new
                {
                    SoftwareTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareTypes", x => x.SoftwareTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    SoftwareID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    SoftwareName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialServicesClientType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cloud = table.Column<int>(type: "int", nullable: false),
                    DocumentAttached = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.SoftwareID);
                    table.ForeignKey(
                        name: "FK_Softwares_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
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
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    SoftwareID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Softwares_SoftwareID",
                        column: x => x.SoftwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
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

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactNumbers",
                columns: table => new
                {
                    ContactNumberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactNumbers", x => x.ContactNumberID);
                    table.ForeignKey(
                        name: "FK_ContactNumbers_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Cities_CountryID",
                table: "Cities",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCountry_CountriesCountryID",
                table: "CompanyCountry",
                column: "CountriesCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers",
                column: "CityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EmployeeID",
                table: "Reviews",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SoftwareID",
                table: "Reviews",
                column: "SoftwareID");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_CompanyID",
                table: "Softwares",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSoftwareModule_SoftwaresSoftwareID",
                table: "SoftwareSoftwareModule",
                column: "SoftwaresSoftwareID");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSoftwareType_SoftwaresSoftwareID",
                table: "SoftwareSoftwareType",
                column: "SoftwaresSoftwareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BusinessAreaSoftware");

            migrationBuilder.DropTable(
                name: "CompanyCountry");

            migrationBuilder.DropTable(
                name: "ContactNumbers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SoftwareSoftwareModule");

            migrationBuilder.DropTable(
                name: "SoftwareSoftwareType");

            migrationBuilder.DropTable(
                name: "BusinessAreas");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "SoftwareModules");

            migrationBuilder.DropTable(
                name: "SoftwareTypes");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
