using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedFinancialandRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialServicesClientType",
                columns: table => new
                {
                    FinancialServicesClientTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialServicesClientType", x => x.FinancialServicesClientTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Software_FinancialServicesClientTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    financialServicesClientTypeID = table.Column<int>(type: "int", nullable: false),
                    softwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_FinancialServicesClientTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_FinancialServicesClientTypes_FinancialServicesClientType_financialServicesClientTypeID",
                        column: x => x.financialServicesClientTypeID,
                        principalTable: "FinancialServicesClientType",
                        principalColumn: "FinancialServicesClientTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_FinancialServicesClientTypes_Softwares_softwareID",
                        column: x => x.softwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_FinancialServicesClientTypes_financialServicesClientTypeID",
                table: "Software_FinancialServicesClientTypes",
                column: "financialServicesClientTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_FinancialServicesClientTypes_softwareID",
                table: "Software_FinancialServicesClientTypes",
                column: "softwareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Software_FinancialServicesClientTypes");

            migrationBuilder.DropTable(
                name: "FinancialServicesClientType");
        }
    }
}
