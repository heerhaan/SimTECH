using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class PenalizeAsBoolean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Penalized",
                table: "Incident",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Penalized",
                value: true);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Penalized",
                value: false);

            migrationBuilder.UpdateData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Penalized",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Penalized",
                table: "Incident");
        }
    }
}
