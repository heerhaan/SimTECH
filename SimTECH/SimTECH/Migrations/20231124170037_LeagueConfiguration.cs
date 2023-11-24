using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class LeagueConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BattleRng",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "DisqualificationOdds",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 150);

            migrationBuilder.AddColumn<int>(
                name: "DriverStatusPaceModifier",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "FatalityOdds",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 300);

            migrationBuilder.AddColumn<int>(
                name: "SafetyCarGap",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 10);

            migrationBuilder.AddColumn<int>(
                name: "SafetyCarOdds",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "SafetyCarReturnOdds",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleRng",
                table: "League");

            migrationBuilder.DropColumn(
                name: "DisqualificationOdds",
                table: "League");

            migrationBuilder.DropColumn(
                name: "DriverStatusPaceModifier",
                table: "League");

            migrationBuilder.DropColumn(
                name: "FatalityOdds",
                table: "League");

            migrationBuilder.DropColumn(
                name: "SafetyCarGap",
                table: "League");

            migrationBuilder.DropColumn(
                name: "SafetyCarOdds",
                table: "League");

            migrationBuilder.DropColumn(
                name: "SafetyCarReturnOdds",
                table: "League");
        }
    }
}
