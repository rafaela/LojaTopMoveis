using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class newitempayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Payments",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "Inactive",
                table: "Payments",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inactive",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Payments",
                newName: "Title");
        }
    }
}
