using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class colorsale3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsSales_Colors_ColorId",
                table: "ProductsSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductsSales_ColorId",
                table: "ProductsSales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_ColorId",
                table: "ProductsSales",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsSales_Colors_ColorId",
                table: "ProductsSales",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");
        }
    }
}
