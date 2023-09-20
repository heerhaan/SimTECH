using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class AddBackTracksDataInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "Id", "AeroMod", "ChassisMod", "Country", "DefenseMod", "Length", "Name", "PowerMod", "QualifyingMod", "State" },
                values: new object[,]
                {
                    { 1L, 0.55000000000000004, 1.1000000000000001, 22, 0.90000000000000002, 7.0099999999999998, "Spa-Francorchamps", 1.3500000000000001, 0.69999999999999996, 1 },
                    { 2L, 1.5, 1.25, 131, 2.0, 3.0499999999999998, "Circuit de Monaco", 0.5, 2.0, 1 },
                    { 3L, 0.84999999999999998, 1.05, 32, 0.80000000000000004, 4.3099999999999996, "Autodromo de Interlagos", 1.1000000000000001, 0.90000000000000002, 1 },
                    { 4L, 0.94999999999999996, 1.1000000000000001, 157, 1.3, 4.5499999999999998, "TT Assen", 0.94999999999999996, 1.1000000000000001, 1 },
                    { 5L, 1.05, 0.90000000000000002, 112, 0.66000000000000003, 5.9900000000000002, "Fuji Speedway", 1.05, 0.90000000000000002, 1 },
                    { 6L, 1.05, 0.94999999999999996, 15, 1.0, 4.3300000000000001, "Österreichring", 1.1000000000000001, 0.80000000000000004, 1 },
                    { 7L, 0.80000000000000004, 0.75, 110, 1.2, 5.79, "Autodromo Nazionale di Monza", 1.25, 1.2, 1 },
                    { 8L, 1.1000000000000001, 0.90000000000000002, 145, 0.90000000000000002, 5.54, "Sepang", 1.1000000000000001, 1.0, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 8L);
        }
    }
}
