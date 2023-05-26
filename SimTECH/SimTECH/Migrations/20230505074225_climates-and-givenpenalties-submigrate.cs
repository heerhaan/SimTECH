using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimTECH.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class climatesandgivenpenaltiessubmigrate : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IncidentId",
                table: "Result",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClimateId",
                table: "Race",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Climate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Terminology = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "varchar(max)", nullable: false),
                    IsWet = table.Column<bool>(type: "bit", nullable: false),
                    EngineMultiplier = table.Column<double>(type: "float", nullable: false),
                    ReliablityModifier = table.Column<int>(type: "int", nullable: false),
                    RngModifier = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ForEntrant = table.Column<int>(type: "int", nullable: false),
                    ForStatus = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    Punishment = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GivenPenalty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumed = table.Column<bool>(type: "bit", nullable: false),
                    SeasonDriverId = table.Column<long>(type: "bigint", nullable: false),
                    IncidentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GivenPenalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GivenPenalty_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GivenPenalty_SeasonDriver_SeasonDriverId",
                        column: x => x.SeasonDriverId,
                        principalTable: "SeasonDriver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Result_IncidentId",
                table: "Result",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_ClimateId",
                table: "Race",
                column: "ClimateId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenPenalty_IncidentId",
                table: "GivenPenalty",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenPenalty_SeasonDriverId",
                table: "GivenPenalty",
                column: "SeasonDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race",
                column: "ClimateId",
                principalTable: "Climate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Incident_IncidentId",
                table: "Result",
                column: "IncidentId",
                principalTable: "Incident",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Race_Climate_ClimateId",
                table: "Race");

            migrationBuilder.DropForeignKey(
                name: "FK_Result_Incident_IncidentId",
                table: "Result");

            migrationBuilder.DropTable(
                name: "Climate");

            migrationBuilder.DropTable(
                name: "GivenPenalty");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropIndex(
                name: "IX_Result_IncidentId",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Race_ClimateId",
                table: "Race");

            migrationBuilder.DropColumn(
                name: "IncidentId",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "ClimateId",
                table: "Race");
        }
    }
}
