using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class SetQualifyingFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualifyingFormat",
                table: "Season",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 125, 75 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 999, 125 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 999, 175 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 999, 100 });

            migrationBuilder.InsertData(
                table: "Tyre",
                columns: new[] { "Id", "Colour", "DistanceMax", "DistanceMin", "ForWet", "Name", "Pace", "State", "WearMax", "WearMin" },
                values: new object[] { 5L, "#3399ff  ", 999, 50, true, "Wet", 50, 1, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DropColumn(
                name: "QualifyingFormat",
                table: "Season");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DistanceMax", "DistanceMin" },
                values: new object[] { 0, 0 });
        }
    }
}
