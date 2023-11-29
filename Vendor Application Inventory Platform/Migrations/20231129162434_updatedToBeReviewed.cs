using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class updatedToBeReviewed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDemoDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LastReviewDate",
                table: "Companies");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDemoDate",
                table: "Softwares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReviewDate",
                table: "Softwares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "softwareToBeRevieweds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_softwareToBeRevieweds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_softwareToBeRevieweds_Softwares_SoftwareID",
                        column: x => x.SoftwareID,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_softwareToBeRevieweds_SoftwareID",
                table: "softwareToBeRevieweds",
                column: "SoftwareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "softwareToBeRevieweds");

            migrationBuilder.DropColumn(
                name: "LastDemoDate",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "LastReviewDate",
                table: "Softwares");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDemoDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReviewDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
