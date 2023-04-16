using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class nonnullresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LapScore_Result_ResultId",
                table: "LapScore");

            migrationBuilder.AlterColumn<long>(
                name: "ResultId",
                table: "LapScore",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LapScore_Result_ResultId",
                table: "LapScore",
                column: "ResultId",
                principalTable: "Result",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LapScore_Result_ResultId",
                table: "LapScore");

            migrationBuilder.AlterColumn<long>(
                name: "ResultId",
                table: "LapScore",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_LapScore_Result_ResultId",
                table: "LapScore",
                column: "ResultId",
                principalTable: "Result",
                principalColumn: "Id");
        }
    }
}
