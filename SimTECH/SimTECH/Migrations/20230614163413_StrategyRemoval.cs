using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class StrategyRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Strategy_StrategyId",
                table: "Result");

            migrationBuilder.DropTable(
                name: "StrategyTyre");

            migrationBuilder.DropTable(
                name: "Strategy");

            migrationBuilder.DropIndex(
                name: "IX_Result_StrategyId",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "StrategyId",
                table: "Result");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Tyre",
                newName: "DistanceMin");

            migrationBuilder.AddColumn<int>(
                name: "DistanceMax",
                table: "Tyre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ForWet",
                table: "Tyre",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TyreId",
                table: "Result",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Icon",
                value: "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M6.76 4.84l-1.8-1.79-1.41 1.41 1.79 1.79 1.42-1.41zM4 10.5H1v2h3v-2zm9-9.95h-2V3.5h2V.55zm7.45 3.91l-1.41-1.41-1.79 1.79 1.41 1.41 1.79-1.79zm-3.21 13.7l1.79 1.8 1.41-1.41-1.8-1.79-1.4 1.4zM20 10.5v2h3v-2h-3zm-8-5c-3.31 0-6 2.69-6 6s2.69 6 6 6 6-2.69 6-6-2.69-6-6-6zm-1 16.95h2V19.5h-2v2.95zm-7.45-3.91l1.41 1.41 1.79-1.8-1.41-1.41-1.79 1.8z\"/>");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DistanceMax", "DistanceMin", "ForWet" },
                values: new object[] { 0, 0, false });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "DistanceMax", "DistanceMin", "ForWet" },
                values: new object[] { 0, 0, false });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "DistanceMax", "DistanceMin", "ForWet" },
                values: new object[] { 0, 0, false });

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "DistanceMax", "DistanceMin", "ForWet" },
                values: new object[] { 0, 0, false });

            migrationBuilder.CreateIndex(
                name: "IX_Result_TyreId",
                table: "Result",
                column: "TyreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result",
                column: "TyreId",
                principalTable: "Tyre",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Tyre_TyreId",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Result_TyreId",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "DistanceMax",
                table: "Tyre");

            migrationBuilder.DropColumn(
                name: "ForWet",
                table: "Tyre");

            migrationBuilder.DropColumn(
                name: "TyreId",
                table: "Result");

            migrationBuilder.RenameColumn(
                name: "DistanceMin",
                table: "Tyre",
                newName: "Length");

            migrationBuilder.AddColumn<long>(
                name: "StrategyId",
                table: "Result",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Strategy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strategy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrategyTyre",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrategyId = table.Column<long>(type: "bigint", nullable: false),
                    TyreId = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategyTyre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrategyTyre_Strategy_StrategyId",
                        column: x => x.StrategyId,
                        principalTable: "Strategy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrategyTyre_Tyre_TyreId",
                        column: x => x.TyreId,
                        principalTable: "Tyre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Climate",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Icon",
                value: "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>");

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Length",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Length",
                value: 150);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Length",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Tyre",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Length",
                value: 500);

            migrationBuilder.CreateIndex(
                name: "IX_Result_StrategyId",
                table: "Result",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyTyre_StrategyId",
                table: "StrategyTyre",
                column: "StrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_StrategyTyre_TyreId",
                table: "StrategyTyre",
                column: "TyreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Strategy_StrategyId",
                table: "Result",
                column: "StrategyId",
                principalTable: "Strategy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
