using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class NonNullableTyre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result");

            migrationBuilder.AlterColumn<long>(
                name: "TyreId",
                table: "Result",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result",
                column: "TyreId",
                principalTable: "Tyre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result");

            migrationBuilder.AlterColumn<long>(
                name: "TyreId",
                table: "Result",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result",
                column: "TyreId",
                principalTable: "Tyre",
                principalColumn: "Id");
        }
    }
}
