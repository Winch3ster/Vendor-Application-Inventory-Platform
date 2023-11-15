using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendor_Application_Inventory_Platform.Migrations
{
    /// <inheritdoc />
    public partial class ChangedClassVarName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses");

            migrationBuilder.AlterColumn<long>(
                name: "Number",
                table: "ContactNumbers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "ContactNumbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_CityID",
                table: "ContactNumbers",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID");
        }
    }
}
