using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardName",
                table: "Wards",
                column: "WardName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_ProvinceName",
                table: "Provinces",
                column: "ProvinceName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wards_WardName",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Provinces_ProvinceName",
                table: "Provinces");
        }
    }
}
