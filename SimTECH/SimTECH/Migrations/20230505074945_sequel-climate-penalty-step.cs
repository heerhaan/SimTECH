using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class sequelclimatepenaltystep : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race");

            migrationBuilder.AlterColumn<long>(
                name: "ClimateId",
                table: "Race",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race",
                column: "ClimateId",
                principalTable: "Climate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race");

            migrationBuilder.AlterColumn<long>(
                name: "ClimateId",
                table: "Race",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race",
                column: "ClimateId",
                principalTable: "Climate",
                principalColumn: "Id");
        }
    }
}
