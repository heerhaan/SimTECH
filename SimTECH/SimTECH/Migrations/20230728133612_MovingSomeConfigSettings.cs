using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class MovingSomeConfigSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MistakeMaximum",
                table: "Season",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MistakeMinimum",
                table: "Season",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MistakeRolls",
                table: "Season",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PitCostSubtractCaution",
                table: "Season",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MistakeMaximum",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "MistakeMinimum",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "MistakeRolls",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "PitCostSubtractCaution",
                table: "Season");
        }
    }
}
