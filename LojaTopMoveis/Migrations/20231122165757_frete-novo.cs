using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaTopMoveis.Migrations
{
    /// <inheritdoc />
    public partial class fretenovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Freights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueKm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValuePriceFreeShipping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityFreeShipping = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeDeliveryDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeShipping = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freights", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Freights");
        }
    }
}
