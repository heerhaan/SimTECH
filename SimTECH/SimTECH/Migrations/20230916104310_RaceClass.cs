using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class RaceClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "SeasonTeam",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AbsoluteGrid",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbsolutePosition",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbsolutePosition",
                table: "QualifyingScore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AbsolutePosition",
                table: "PracticeScore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RaceClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Colour = table.Column<string>(type: "nchar(9)", fixedLength: true, maxLength: 9, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SeasonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceClass_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_ClassId",
                table: "SeasonTeam",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceClass_SeasonId",
                table: "RaceClass",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTeam_RaceClass_ClassId",
                table: "SeasonTeam",
                column: "ClassId",
                principalTable: "RaceClass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTeam_RaceClass_ClassId",
                table: "SeasonTeam");

            migrationBuilder.DropTable(
                name: "RaceClass");

            migrationBuilder.DropIndex(
                name: "IX_SeasonTeam_ClassId",
                table: "SeasonTeam");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "SeasonTeam");

            migrationBuilder.DropColumn(
                name: "AbsoluteGrid",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "AbsolutePosition",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "AbsolutePosition",
                table: "QualifyingScore");

            migrationBuilder.DropColumn(
                name: "AbsolutePosition",
                table: "PracticeScore");
        }
    }
}
