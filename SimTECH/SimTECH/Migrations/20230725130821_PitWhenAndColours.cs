using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class PitWhenAndColours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PitWhenBelow",
                table: "Tyre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Incident",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Climate",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Colour",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PitWhenBelow",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PitWhenBelow",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PitWhenBelow",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PitWhenBelow",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PitWhenBelow",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PitWhenBelow",
                table: "Tyre");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Climate");
        }
    }
}
