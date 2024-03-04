using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class vendaaddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdrressId",
                table: "Sales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdrressId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
