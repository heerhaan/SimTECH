using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class modificationincidents : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Incident",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "Weather",
                table: "Race");

            migrationBuilder.DropColumn(
                name: "ForEntrant",
                table: "Incident");

            migrationBuilder.RenameColumn(
                name: "ForStatus",
                table: "Incident",
                newName: "Category");

            migrationBuilder.AddColumn<long>(
                name: "ConsumedAtRaceId",
                table: "GivenPenalty",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumedAtRaceId",
                table: "GivenPenalty");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Incident",
                newName: "ForStatus");

            migrationBuilder.AddColumn<int>(
                name: "Incident",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weather",
                table: "Race",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ForEntrant",
                table: "Incident",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
