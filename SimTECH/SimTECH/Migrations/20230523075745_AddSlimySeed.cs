using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class AddSlimySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Climate",
                columns: new[] { "Id", "EngineMultiplier", "Icon", "IsWet", "Odds", "ReliablityModifier", "RngModifier", "State", "Terminology" },
                values: new object[,]
                {
                    { 1L, 1.1000000000000001, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>", false, 3, 0, 0, 1, "Sunny" },
                    { 2L, 0.90000000000000002, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>", false, 3, 0, 0, 1, "Overcast" },
                    { 3L, 0.75, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>", false, 1, 0, 0, 1, "Rain" }
                });

            migrationBuilder.InsertData(
                table: "Incident",
                columns: new[] { "Id", "Category", "Limit", "Name", "Odds", "Punishment", "State" },
                values: new object[,]
                {
                    { 1L, 0, 0, "Damage", 2, 0, 1 },
                    { 2L, 0, 2, "Collision", 1, 3, 1 },
                    { 3L, 0, 5, "Accident", 2, 3, 1 },
                    { 4L, 0, 0, "Puncture", 1, 0, 1 },
                    { 5L, 2, 5, "Engine", 3, 10, 1 },
                    { 6L, 1, 3, "Electrics", 1, 5, 1 },
                    { 7L, 1, 0, "Exhaust", 1, 0, 1 },
                    { 8L, 1, 4, "Gearbox", 2, 5, 1 },
                    { 9L, 1, 0, "Hydraulics", 1, 0, 1 },
                    { 10L, 1, 0, "Wheel", 1, 0, 1 },
                    { 11L, 1, 0, "Brakes", 1, 0, 1 },
                    { 12L, 3, 0, "Illegal", 1, 10, 1 },
                    { 13L, 3, 0, "Fuel", 1, 10, 1 },
                    { 14L, 3, 0, "Dangerous", 1, 10, 1 },
                    { 15L, 4, 0, "Hospital", 5, 0, 1 },
                    { 16L, 4, 0, "Death", 1, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "Accent", "Colour", "Name", "Pace", "State", "WearMax", "WearMin" },
                values: new object[] { 1L, "#e56103", "#0b0b0d", "Hankook", 1, 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "Id", "AeroMod", "ChassisMod", "Country", "DefenseMod", "Length", "Name", "PowerMod", "QualifyingMod", "State" },
                values: new object[,]
                {
                    { 1L, 0.55000000000000004, 1.1000000000000001, 22, 0.90000000000000002, 7.0099999999999998, "Spa-Francorchamps", 1.3500000000000001, 0.69999999999999996, 1 },
                    { 2L, 1.5, 1.25, 131, 2.0, 3.0499999999999998, "Circuit de Monaco", 0.5, 2.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trait",
                columns: new[] { "Id", "Attack", "CarReliability", "Defense", "Description", "DriverReliability", "EngineReliability", "ForWetConditions", "Name", "QualifyingPace", "RacePace", "RngMax", "RngMin", "State", "Type", "WearMax", "WearMin" },
                values: new object[,]
                {
                    { 1L, 0, 0, 0, "Speed in moist", 3, 0, true, "Rainmeister", 3, 5, 0, 0, 0, 1, 0, 0 },
                    { 2L, 0, 0, 0, "Owns the engine", 0, 2, false, "Manufacturer", 1, 1, 0, 0, 0, 2, 0, 0 },
                    { 3L, 0, -1, 10, "Street is a circuit", -2, 0, false, "Street Circuit", 0, 0, 0, -5, 0, 3, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tyre",
                columns: new[] { "Id", "Colour", "Length", "Name", "Pace", "State", "WearMax", "WearMin" },
                values: new object[,]
                {
                    { 1L, "#fa0536  ", 100, "Soft", 200, 1, 25, 15 },
                    { 2L, "#f4ea26  ", 150, "Medium", 180, 1, 15, 9 },
                    { 3L, "#dfdde9  ", 200, "Hard", 160, 1, 10, 6 },
                    { 4L, "#bded80  ", 500, "Grooved", 100, 2, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Incident",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Manufacturer",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Track",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Trait",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Trait",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Trait",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
