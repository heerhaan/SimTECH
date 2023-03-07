using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_SeasonTeam_Result_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonDriver_SeasonTeam_SeasonTeamId",
                table: "SeasonDriver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<long>(
                name: "SeasonTeamId",
                table: "SeasonDriver",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "SeasonTeamId",
                table: "Result",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Result_SeasonTeamId",
                table: "Result",
                column: "SeasonTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_SeasonTeam_SeasonTeamId",
                table: "Result",
                column: "SeasonTeamId",
                principalTable: "SeasonTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonDriver_SeasonTeam_SeasonTeamId",
                table: "SeasonDriver",
                column: "SeasonTeamId",
                principalTable: "SeasonTeam",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_SeasonTeam_SeasonTeamId",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonDriver_SeasonTeam_SeasonTeamId",
                table: "SeasonDriver");

            migrationBuilder.DropIndex(
                name: "IX_Result_SeasonTeamId",
                table: "Result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SeasonTeamId",
                table: "Result");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "SeasonTeamId",
                table: "SeasonDriver",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonDriver_SeasonTeam_SeasonTeamId",
                table: "SeasonDriver",
                column: "SeasonTeamId",
                principalTable: "SeasonTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
