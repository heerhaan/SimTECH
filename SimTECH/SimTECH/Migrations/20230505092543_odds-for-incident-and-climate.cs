using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class oddsforincidentandclimate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_League_LeagueId",
                table: "Contract");

            migrationBuilder.AddColumn<int>(
                name: "Odds",
                table: "Incident",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "LeagueId",
                table: "Contract",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Odds",
                table: "Climate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_League_LeagueId",
                table: "Contract",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_League_LeagueId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Odds",
                table: "Incident");

            migrationBuilder.DropColumn(
                name: "Odds",
                table: "Climate");

            migrationBuilder.AlterColumn<long>(
                name: "LeagueId",
                table: "Contract",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_League_LeagueId",
                table: "Contract",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
