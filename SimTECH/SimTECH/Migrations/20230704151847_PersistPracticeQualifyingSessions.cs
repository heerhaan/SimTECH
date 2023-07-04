using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class PersistPracticeQualifyingSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FastestLap",
                table: "Result",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PracticeScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Scores = table.Column<string>(type: "varchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<long>(type: "bigint", nullable: false),
                    ResultId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualifyingScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Scores = table.Column<string>(type: "varchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<long>(type: "bigint", nullable: false),
                    ResultId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifyingScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualifyingScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PracticeScore_ResultId",
                table: "PracticeScore",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_QualifyingScore_ResultId",
                table: "QualifyingScore",
                column: "ResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracticeScore");

            migrationBuilder.DropTable(
                name: "QualifyingScore");

            migrationBuilder.DropColumn(
                name: "FastestLap",
                table: "Result");
        }
    }
}
