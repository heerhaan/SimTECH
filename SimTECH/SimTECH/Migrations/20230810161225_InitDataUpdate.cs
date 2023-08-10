using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class InitDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Manufacturer",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Pace",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Trait",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Description",
                value: "Faster when it's wet");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#fa0536ff", 20 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#f4ea26ff", 15 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#dfdde9ff", 10 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Colour",
                value: "#bded80ff");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Colour",
                value: "#3399ffff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Manufacturer",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Pace",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Trait",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Description",
                value: "Speed in moist");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#fa0536  ", 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#f4ea26  ", 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Colour", "PitWhenBelow" },
                values: new object[] { "#dfdde9  ", 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Colour",
                value: "#bded80  ");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Colour",
                value: "#3399ff  ");
        }
    }
}
