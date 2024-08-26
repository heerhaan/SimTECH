using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class StrategicPreferenceAndSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetupGained",
                table: "PracticeScore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SetupRng",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Colour",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValue: "#ffffffff",
                oldClrType: typeof(string),
                oldType: "nchar(9)",
                oldFixedLength: true,
                oldMaxLength: 9,
                oldDefaultValueSql: "#ffffffff");

            migrationBuilder.AlterColumn<string>(
                name: "Accent",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValue: "#000000ff",
                oldClrType: typeof(string),
                oldType: "nchar(9)",
                oldFixedLength: true,
                oldMaxLength: 9,
                oldDefaultValueSql: "#000000ff");

            migrationBuilder.AddColumn<int>(
                name: "StrategyPreference",
                table: "Driver",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetupGained",
                table: "PracticeScore");

            migrationBuilder.DropColumn(
                name: "SetupRng",
                table: "League");

            migrationBuilder.DropColumn(
                name: "StrategyPreference",
                table: "Driver");

            migrationBuilder.AlterColumn<string>(
                name: "Colour",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValueSql: "#ffffffff",
                oldClrType: typeof(string),
                oldType: "nchar(9)",
                oldFixedLength: true,
                oldMaxLength: 9,
                oldDefaultValue: "#ffffffff");

            migrationBuilder.AlterColumn<string>(
                name: "Accent",
                table: "Engine",
                type: "nchar(9)",
                fixedLength: true,
                maxLength: 9,
                nullable: false,
                defaultValueSql: "#000000ff",
                oldClrType: typeof(string),
                oldType: "nchar(9)",
                oldFixedLength: true,
                oldMaxLength: 9,
                oldDefaultValue: "#000000ff");
        }
    }
}
