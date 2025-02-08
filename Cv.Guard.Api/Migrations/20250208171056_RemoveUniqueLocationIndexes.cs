using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cv.Guard.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueLocationIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_City",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Region",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_City",
                table: "Locations",
                column: "City",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Region",
                table: "Locations",
                column: "Region",
                unique: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_City",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_Region",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_City",
                table: "Locations",
                column: "City",
                unique: true,
                filter: "[City] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Region",
                table: "Locations",
                column: "Region",
                unique: true,
                filter: "[Region] IS NOT NULL");
        }
    }
}
