using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class LeagueSpecificTyres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueTyre",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "bigint", nullable: false),
                    TyreId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTyre", x => new { x.LeagueId, x.TyreId });
                    table.ForeignKey(
                        name: "FK_LeagueTyre_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueTyre_Tyre_TyreId",
                        column: x => x.TyreId,
                        principalTable: "Tyre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.InsertData(
            //    table: "Track",
            //    columns: new[] { "Id", "AeroMod", "ChassisMod", "Country", "DefenseMod", "Length", "Name", "PowerMod", "QualifyingMod", "State" },
            //    values: new object[,]
            //    {
            //        { 3L, 0.84999999999999998, 1.05, 32, 0.80000000000000004, 4.3099999999999996, "Autodromo de Interlagos", 1.1000000000000001, 0.90000000000000002, 1 },
            //        { 4L, 0.94999999999999996, 1.1000000000000001, 157, 1.3, 4.5499999999999998, "TT Assen", 0.94999999999999996, 1.1000000000000001, 1 },
            //        { 5L, 1.05, 0.90000000000000002, 112, 0.66000000000000003, 5.9900000000000002, "Fuji Speedway", 1.05, 0.90000000000000002, 1 },
            //        { 6L, 1.05, 0.94999999999999996, 15, 1.0, 4.3300000000000001, "Österreichring", 1.1000000000000001, 0.80000000000000004, 1 },
            //        { 7L, 0.80000000000000004, 0.75, 110, 1.2, 5.79, "Autodromo Nazionale di Monza", 1.25, 1.2, 1 },
            //        { 8L, 1.1000000000000001, 0.90000000000000002, 145, 0.90000000000000002, 5.54, "Sepang", 1.1000000000000001, 1.0, 1 }
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTyre_TyreId",
                table: "LeagueTyre",
                column: "TyreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueTyre");

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 3L);

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 4L);

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 5L);

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 6L);

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 7L);

            //migrationBuilder.DeleteData(
            //    table: "Track",
            //    keyColumn: "Id",
            //    keyValue: 8L);
        }
    }
}
