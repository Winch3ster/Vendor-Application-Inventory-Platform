using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSetForSoftwareTypeandModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Software_Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    moduleID = table.Column<int>(type: "int", nullable: false),
                    softwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_Modules_SoftwareModules_moduleID",
                        column: x => x.moduleID,
                        principalTable: "SoftwareModules",
                        principalColumn: "SoftwareModuleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_Modules_Softwares_softwareID",
                        column: x => x.softwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Software_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeID = table.Column<int>(type: "int", nullable: false),
                    softwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_Types_SoftwareTypes_typeID",
                        column: x => x.typeID,
                        principalTable: "SoftwareTypes",
                        principalColumn: "SoftwareTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_Types_Softwares_softwareID",
                        column: x => x.softwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Software_Modules_moduleID",
                table: "Software_Modules",
                column: "moduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Modules_softwareID",
                table: "Software_Modules",
                column: "softwareID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Types_softwareID",
                table: "Software_Types",
                column: "softwareID");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Types_typeID",
                table: "Software_Types",
                column: "typeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Software_Modules");

            migrationBuilder.DropTable(
                name: "Software_Types");
        }
    }
}
