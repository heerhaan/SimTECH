using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class LapByLap_Scores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StintResult");

            migrationBuilder.DropTable(
                name: "Stint");

            migrationBuilder.DropColumn(
                name: "NumberStint",
                table: "StrategyTyre");

            migrationBuilder.DropColumn(
                name: "StintLength",
                table: "Strategy");

            migrationBuilder.CreateTable(
                name: "LapScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ResultId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LapScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LapScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LapScore_ResultId",
                table: "LapScore",
                column: "ResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LapScore");

            migrationBuilder.AddColumn<int>(
                name: "NumberStint",
                table: "StrategyTyre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StintLength",
                table: "Strategy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Stint",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    RngMax = table.Column<int>(type: "int", nullable: false),
                    RngMin = table.Column<int>(type: "int", nullable: false),
                    StintEvents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stint_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StintResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<long>(type: "bigint", nullable: false),
                    StintId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    RacerEvents = table.Column<int>(type: "int", nullable: false),
                    StintScore = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StintResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StintResult_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StintResult_Stint_StintId",
                        column: x => x.StintId,
                        principalTable: "Stint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stint_RaceId",
                table: "Stint",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_StintResult_ResultId",
                table: "StintResult",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_StintResult_StintId",
                table: "StintResult",
                column: "StintId");
        }
    }
}
