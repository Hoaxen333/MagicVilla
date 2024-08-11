using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_API.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationAlimentar_Vila : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "vilas",
                columns: new[] { "Id", "Amenindad", "DataDeActualization", "DateOfFoundation", "Description", "ImageUrl", "MetrosQuadrados", "Name", "Ocupantes", "Tarifa" },
                values: new object[] { 1, "", new DateTime(2024, 8, 10, 12, 57, 27, 602, DateTimeKind.Local).AddTicks(5558), new DateTime(2024, 8, 10, 12, 57, 27, 602, DateTimeKind.Local).AddTicks(5543), "Description", "", 10, "VilaReal", 10, 10.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "vilas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
