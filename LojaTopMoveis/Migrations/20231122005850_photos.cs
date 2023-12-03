using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class photos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Photos");
        }
    }
}
