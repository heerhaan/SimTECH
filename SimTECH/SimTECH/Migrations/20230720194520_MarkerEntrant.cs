using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class MarkerEntrant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alive",
                table: "Driver",
                newName: "Mark");

            migrationBuilder.AddColumn<bool>(
                name: "Mark",
                table: "Team",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mark",
                table: "Engine",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Engine");

            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "Driver",
                newName: "Alive");
        }
    }
}
