using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class TrackBattlesAndCautions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defended",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Overtaken",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TyreColour",
                table: "LapScore",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RaceOccurrence",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Occurrences = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceOccurrence_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceOccurrence_RaceId",
                table: "RaceOccurrence",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceOccurrence");

            migrationBuilder.DropColumn(
                name: "Defended",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "Overtaken",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "TyreColour",
                table: "LapScore");
        }
    }
}
