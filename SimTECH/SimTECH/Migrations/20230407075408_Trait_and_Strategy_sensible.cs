using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class Trait_and_Strategy_sensible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarPace",
                table: "Trait");

            migrationBuilder.DropColumn(
                name: "DriverPace",
                table: "Trait");

            migrationBuilder.RenameColumn(
                name: "EnginePace",
                table: "Trait",
                newName: "RacePace");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "StrategyTyre",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "StrategyTyre");

            migrationBuilder.RenameColumn(
                name: "RacePace",
                table: "Trait",
                newName: "EnginePace");

            migrationBuilder.AddColumn<int>(
                name: "CarPace",
                table: "Trait",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverPace",
                table: "Trait",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
