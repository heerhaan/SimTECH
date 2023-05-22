using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class HolyShitOvertakingIsHere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DefenseMod",
                table: "Track",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "SeasonDriver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "SeasonDriver",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefenseMod",
                table: "Track");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "SeasonDriver");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "SeasonDriver");
        }
    }
}
