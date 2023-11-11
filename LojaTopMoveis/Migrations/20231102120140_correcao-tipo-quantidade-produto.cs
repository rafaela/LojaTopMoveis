using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class correcaotipoquantidadeproduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Clients_ClientId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ClientId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientId",
                table: "Employees",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Clients_ClientId",
                table: "Employees",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
