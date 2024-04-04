using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class EngineBadgeColour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Accent",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValue: "#000000ff");

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValue: "#ffffffff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accent",
                table: "Engine");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Engine");
        }
    }
}
