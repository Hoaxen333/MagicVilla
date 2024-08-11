using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVila_API.Migrations
{
    /// <inheritdoc />
    public partial class Actualization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amenindad",
                table: "vilas",
                newName: "Amenidad");

            migrationBuilder.UpdateData(
                table: "vilas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataDeActualization", "DateOfFoundation" },
                values: new object[] { new DateTime(2024, 8, 10, 14, 20, 12, 315, DateTimeKind.Local).AddTicks(6735), new DateTime(2024, 8, 10, 14, 20, 12, 315, DateTimeKind.Local).AddTicks(6723) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amenidad",
                table: "vilas",
                newName: "Amenindad");

            migrationBuilder.UpdateData(
                table: "vilas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataDeActualization", "DateOfFoundation" },
                values: new object[] { new DateTime(2024, 8, 10, 12, 57, 27, 602, DateTimeKind.Local).AddTicks(5558), new DateTime(2024, 8, 10, 12, 57, 27, 602, DateTimeKind.Local).AddTicks(5543) });
        }
    }
}
