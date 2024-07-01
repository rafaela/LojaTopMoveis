using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class colorclientproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlImage",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "urlImage",
                table: "Colors");
        }
    }
}
