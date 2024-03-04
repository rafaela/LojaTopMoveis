using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class vendaaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdrressId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_AddressId",
                table: "Sales",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Addresses_AddressId",
                table: "Sales",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Addresses_AddressId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_AddressId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "AdrressId",
                table: "Sales");
        }
    }
}
