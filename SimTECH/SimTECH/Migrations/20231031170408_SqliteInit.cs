using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimTECH.Migrations
{
    /// <inheritdoc />
    public partial class SqliteInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Climate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Terminology = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    IsWet = table.Column<bool>(type: "INTEGER", nullable: false),
                    EngineMultiplier = table.Column<double>(type: "REAL", nullable: false),
                    ReliablityModifier = table.Column<int>(type: "INTEGER", nullable: false),
                    RngModifier = table.Column<int>(type: "INTEGER", nullable: false),
                    Odds = table.Column<int>(type: "INTEGER", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", maxLength: 9, nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Country = table.Column<int>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Biography = table.Column<string>(type: "TEXT", nullable: false),
                    Mark = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Mark = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Limit = table.Column<int>(type: "INTEGER", nullable: false),
                    Punishment = table.Column<int>(type: "INTEGER", nullable: false),
                    Odds = table.Column<int>(type: "INTEGER", nullable: false),
                    Penalized = table.Column<bool>(type: "INTEGER", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", maxLength: 9, nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RaceLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Options = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Accent = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Pace = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMin = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMax = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<int>(type: "nvarchar(200)", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Country = table.Column<int>(type: "varchar(2)", maxLength: 2, nullable: false),
                    Biography = table.Column<string>(type: "TEXT", nullable: false),
                    Mark = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<int>(type: "INTEGER", nullable: false),
                    Length = table.Column<double>(type: "REAL", nullable: false),
                    AeroMod = table.Column<double>(type: "REAL", nullable: false),
                    ChassisMod = table.Column<double>(type: "REAL", nullable: false),
                    PowerMod = table.Column<double>(type: "REAL", nullable: false),
                    QualifyingMod = table.Column<double>(type: "REAL", nullable: false),
                    DefenseMod = table.Column<double>(type: "REAL", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trait",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingPace = table.Column<int>(type: "INTEGER", nullable: false),
                    RacePace = table.Column<int>(type: "INTEGER", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverReliability = table.Column<int>(type: "INTEGER", nullable: false),
                    CarReliability = table.Column<int>(type: "INTEGER", nullable: false),
                    EngineReliability = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMin = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMax = table.Column<int>(type: "INTEGER", nullable: false),
                    RngMin = table.Column<int>(type: "INTEGER", nullable: false),
                    RngMax = table.Column<int>(type: "INTEGER", nullable: false),
                    ForWetConditions = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trait", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tyre",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Pace = table.Column<int>(type: "INTEGER", nullable: false),
                    PitWhenBelow = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMin = table.Column<int>(type: "INTEGER", nullable: false),
                    WearMax = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanceMin = table.Column<int>(type: "INTEGER", nullable: false),
                    DistanceMax = table.Column<int>(type: "INTEGER", nullable: false),
                    ForWet = table.Column<bool>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tyre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentRange",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Comparer = table.Column<int>(type: "INTEGER", nullable: false),
                    Minimum = table.Column<int>(type: "INTEGER", nullable: false),
                    Maximum = table.Column<int>(type: "INTEGER", nullable: false),
                    LeagueId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopmentRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentRange_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumDriversInRace = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingAmountQ2 = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingAmountQ3 = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingRNG = table.Column<int>(type: "INTEGER", nullable: false),
                    RunAmountSession = table.Column<int>(type: "INTEGER", nullable: false),
                    GridBonus = table.Column<int>(type: "INTEGER", nullable: false),
                    PitMinimum = table.Column<int>(type: "INTEGER", nullable: false),
                    PitMaximum = table.Column<int>(type: "INTEGER", nullable: false),
                    PitCostSubtractCaution = table.Column<int>(type: "INTEGER", nullable: false),
                    RngMinimum = table.Column<int>(type: "INTEGER", nullable: false),
                    RngMaximum = table.Column<int>(type: "INTEGER", nullable: false),
                    MistakeRolls = table.Column<int>(type: "INTEGER", nullable: false),
                    MistakeMinimum = table.Column<int>(type: "INTEGER", nullable: false),
                    MistakeMaximum = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsPole = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsFastestLap = table.Column<int>(type: "INTEGER", nullable: false),
                    QualifyingFormat = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    LeagueId = table.Column<long>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Season_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<long>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    LeagueId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverTrait",
                columns: table => new
                {
                    TraitId = table.Column<long>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTrait", x => new { x.DriverId, x.TraitId });
                    table.ForeignKey(
                        name: "FK_DriverTrait_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverTrait_Trait_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Trait",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamTrait",
                columns: table => new
                {
                    TraitId = table.Column<long>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTrait", x => new { x.TeamId, x.TraitId });
                    table.ForeignKey(
                        name: "FK_TeamTrait_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamTrait_Trait_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Trait",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackTrait",
                columns: table => new
                {
                    TraitId = table.Column<long>(type: "INTEGER", nullable: false),
                    TrackId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackTrait", x => new { x.TrackId, x.TraitId });
                    table.ForeignKey(
                        name: "FK_TrackTrait_Track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackTrait_Trait_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Trait",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueTyre",
                columns: table => new
                {
                    LeagueId = table.Column<long>(type: "INTEGER", nullable: false),
                    TyreId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTyre", x => new { x.LeagueId, x.TyreId });
                    table.ForeignKey(
                        name: "FK_LeagueTyre_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueTyre_Tyre_TyreId",
                        column: x => x.TyreId,
                        principalTable: "Tyre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntrantId = table.Column<long>(type: "INTEGER", nullable: false),
                    EntrantGroup = table.Column<int>(type: "INTEGER", nullable: false),
                    DevelopedAspect = table.Column<int>(type: "INTEGER", nullable: false),
                    Change = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopmentLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevelopmentLog_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointAllotment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointAllotment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointAllotment_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Round = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RaceLength = table.Column<int>(type: "INTEGER", nullable: false),
                    DateFinished = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false),
                    TrackId = table.Column<long>(type: "INTEGER", nullable: false),
                    ClimateId = table.Column<long>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Race_Climate_ClimateId",
                        column: x => x.ClimateId,
                        principalTable: "Climate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Race_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Race_Track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceClass_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonEngine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    Reliability = table.Column<int>(type: "INTEGER", nullable: false),
                    Rebadged = table.Column<bool>(type: "INTEGER", nullable: false),
                    EngineId = table.Column<long>(type: "INTEGER", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonEngine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonEngine_Engine_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonEngine_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceOccurrence",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Occurrences = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceOccurrence_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonTeam",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Principal = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Accent = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    HiddenPoints = table.Column<double>(type: "REAL", nullable: false),
                    BaseValue = table.Column<int>(type: "INTEGER", nullable: false),
                    Aero = table.Column<int>(type: "INTEGER", nullable: false),
                    Chassis = table.Column<int>(type: "INTEGER", nullable: false),
                    Powertrain = table.Column<int>(type: "INTEGER", nullable: false),
                    Reliability = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false),
                    SeasonEngineId = table.Column<long>(type: "INTEGER", nullable: false),
                    ManufacturerId = table.Column<long>(type: "INTEGER", nullable: false),
                    ClassId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonTeam_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonTeam_RaceClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "RaceClass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeasonTeam_SeasonEngine_SeasonEngineId",
                        column: x => x.SeasonEngineId,
                        principalTable: "SeasonEngine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonTeam_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeasonTeam_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonDriver",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    HiddenPoints = table.Column<double>(type: "REAL", nullable: false),
                    Skill = table.Column<int>(type: "INTEGER", nullable: false),
                    Reliability = table.Column<int>(type: "INTEGER", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamRole = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonId = table.Column<long>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<long>(type: "INTEGER", nullable: false),
                    SeasonTeamId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonDriver_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonDriver_SeasonTeam_SeasonTeamId",
                        column: x => x.SeasonTeamId,
                        principalTable: "SeasonTeam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeasonDriver_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GivenPenalty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Consumed = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConsumedAtRaceId = table.Column<long>(type: "INTEGER", nullable: true),
                    SeasonDriverId = table.Column<long>(type: "INTEGER", nullable: false),
                    IncidentId = table.Column<long>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grid = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    AbsoluteGrid = table.Column<int>(type: "INTEGER", nullable: false),
                    AbsolutePosition = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Setup = table.Column<int>(type: "INTEGER", nullable: false),
                    TyreLife = table.Column<int>(type: "INTEGER", nullable: false),
                    FastestLap = table.Column<bool>(type: "INTEGER", nullable: false),
                    Overtaken = table.Column<int>(type: "INTEGER", nullable: false),
                    Defended = table.Column<int>(type: "INTEGER", nullable: false),
                    SeasonDriverId = table.Column<long>(type: "INTEGER", nullable: false),
                    SeasonTeamId = table.Column<long>(type: "INTEGER", nullable: false),
                    RaceId = table.Column<long>(type: "INTEGER", nullable: false),
                    TyreId = table.Column<long>(type: "INTEGER", nullable: false),
                    IncidentId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Result_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_SeasonDriver_SeasonDriverId",
                        column: x => x.SeasonDriverId,
                        principalTable: "SeasonDriver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_SeasonTeam_SeasonTeamId",
                        column: x => x.SeasonTeamId,
                        principalTable: "SeasonTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_Tyre_TyreId",
                        column: x => x.TyreId,
                        principalTable: "Tyre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LapScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    TyreColour = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 9, nullable: true),
                    RacerEvents = table.Column<int>(type: "INTEGER", nullable: false),
                    ResultId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LapScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LapScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticeScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Scores = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    AbsolutePosition = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceId = table.Column<long>(type: "INTEGER", nullable: false),
                    ResultId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualifyingScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Scores = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    AbsolutePosition = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceId = table.Column<long>(type: "INTEGER", nullable: false),
                    ResultId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualifyingScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualifyingScore_Result_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Result",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Climate",
                columns: new[] { "Id", "Colour", "EngineMultiplier", "Icon", "IsWet", "Odds", "ReliablityModifier", "RngModifier", "State", "Terminology" },
                values: new object[,]
                {
                    { 1L, null, 1.1000000000000001, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M6.76 4.84l-1.8-1.79-1.41 1.41 1.79 1.79 1.42-1.41zM4 10.5H1v2h3v-2zm9-9.95h-2V3.5h2V.55zm7.45 3.91l-1.41-1.41-1.79 1.79 1.41 1.41 1.79-1.79zm-3.21 13.7l1.79 1.8 1.41-1.41-1.8-1.79-1.4 1.4zM20 10.5v2h3v-2h-3zm-8-5c-3.31 0-6 2.69-6 6s2.69 6 6 6 6-2.69 6-6-2.69-6-6-6zm-1 16.95h2V19.5h-2v2.95zm-7.45-3.91l1.41 1.41 1.79-1.8-1.41-1.41-1.79 1.8z\"/>", false, 3, 0, 0, 1, "Sunny" },
                    { 2L, null, 0.90000000000000002, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>", false, 3, 0, 0, 1, "Overcast" },
                    { 3L, null, 0.75, "<path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z\"/>", false, 1, 0, 0, 1, "Rain" }
                });

            migrationBuilder.InsertData(
                table: "Incident",
                columns: new[] { "Id", "Category", "Colour", "Limit", "Name", "Odds", "Penalized", "Punishment", "State" },
                values: new object[,]
                {
                    { 1L, 0, null, 0, "Damage", 2, false, 0, 1 },
                    { 2L, 0, null, 2, "Collision", 1, true, 3, 1 },
                    { 3L, 0, null, 5, "Accident", 2, true, 3, 1 },
                    { 4L, 0, null, 0, "Puncture", 1, false, 0, 1 },
                    { 5L, 2, null, 5, "Engine", 3, true, 10, 1 },
                    { 6L, 1, null, 3, "Electrics", 1, true, 5, 1 },
                    { 7L, 1, null, 0, "Exhaust", 1, false, 0, 1 },
                    { 8L, 1, null, 4, "Gearbox", 2, true, 5, 1 },
                    { 9L, 1, null, 0, "Hydraulics", 1, false, 0, 1 },
                    { 10L, 1, null, 0, "Wheel", 1, false, 0, 1 },
                    { 11L, 1, null, 0, "Brakes", 1, false, 0, 1 },
                    { 12L, 3, null, 0, "Illegal", 1, true, 10, 1 },
                    { 13L, 3, null, 0, "Fuel", 1, true, 10, 1 },
                    { 14L, 3, null, 0, "Dangerous", 1, true, 10, 1 },
                    { 15L, 4, null, 0, "Hospital", 5, false, 0, 1 },
                    { 16L, 4, null, 0, "Death", 1, false, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "Accent", "Colour", "Name", "Pace", "State", "WearMax", "WearMin" },
                values: new object[] { 1L, "#e56103", "#0b0b0d", "Hankook", 0, 1, 0, 0 });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "Id", "AeroMod", "ChassisMod", "Country", "DefenseMod", "Length", "Name", "PowerMod", "QualifyingMod", "State" },
                values: new object[,]
                {
                    { 1L, 0.55000000000000004, 1.1000000000000001, 22, 0.90000000000000002, 7.0099999999999998, "Spa-Francorchamps", 1.3500000000000001, 0.69999999999999996, 1 },
                    { 2L, 1.5, 1.25, 131, 2.0, 3.0499999999999998, "Circuit de Monaco", 0.5, 2.0, 1 },
                    { 3L, 0.84999999999999998, 1.05, 32, 0.80000000000000004, 4.3099999999999996, "Autodromo de Interlagos", 1.1000000000000001, 0.90000000000000002, 1 },
                    { 4L, 0.94999999999999996, 1.1000000000000001, 157, 1.3, 4.5499999999999998, "TT Assen", 0.94999999999999996, 1.1000000000000001, 1 },
                    { 5L, 1.05, 0.90000000000000002, 112, 0.66000000000000003, 5.9900000000000002, "Fuji Speedway", 1.05, 0.90000000000000002, 1 },
                    { 6L, 1.05, 0.94999999999999996, 15, 1.0, 4.3300000000000001, "Österreichring", 1.1000000000000001, 0.80000000000000004, 1 },
                    { 7L, 0.80000000000000004, 0.75, 110, 1.2, 5.79, "Autodromo Nazionale di Monza", 1.25, 1.2, 1 },
                    { 8L, 1.1000000000000001, 0.90000000000000002, 145, 0.90000000000000002, 5.54, "Sepang", 1.1000000000000001, 1.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Trait",
                columns: new[] { "Id", "Attack", "CarReliability", "Defense", "Description", "DriverReliability", "EngineReliability", "ForWetConditions", "Name", "QualifyingPace", "RacePace", "RngMax", "RngMin", "State", "Type", "WearMax", "WearMin" },
                values: new object[,]
                {
                    { 1L, 0, 0, 0, "Faster when it's wet", 3, 0, true, "Rainmeister", 3, 5, 0, 0, 0, 1, 0, 0 },
                    { 2L, 0, 0, 0, "Owns the engine", 0, 2, false, "Manufacturer", 1, 1, 0, 0, 0, 2, 0, 0 },
                    { 3L, 0, -1, 10, "Street is a circuit", -2, 0, false, "Street Circuit", 0, 0, 0, -5, 0, 3, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tyre",
                columns: new[] { "Id", "Colour", "DistanceMax", "DistanceMin", "ForWet", "Name", "Pace", "PitWhenBelow", "State", "WearMax", "WearMin" },
                values: new object[,]
                {
                    { 1L, "#fa0536ff", 125, 50, false, "Soft", 200, 20, 1, 25, 15 },
                    { 2L, "#f4ea26ff", 999, 125, false, "Medium", 180, 15, 1, 15, 9 },
                    { 3L, "#dfdde9ff", 999, 175, false, "Hard", 160, 10, 1, 10, 6 },
                    { 4L, "#bded80ff", 999, 100, false, "Grooved", 100, 0, 2, 3, 1 },
                    { 5L, "#3399ffff", 999, 50, true, "Wet", 50, 0, 1, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_DriverId",
                table: "Contract",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_LeagueId",
                table: "Contract",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_TeamId",
                table: "Contract",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentLog_SeasonId",
                table: "DevelopmentLog",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentRange_LeagueId",
                table: "DevelopmentRange",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTrait_TraitId",
                table: "DriverTrait",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenPenalty_IncidentId",
                table: "GivenPenalty",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_GivenPenalty_SeasonDriverId",
                table: "GivenPenalty",
                column: "SeasonDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_LapScore_ResultId",
                table: "LapScore",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTyre_TyreId",
                table: "LeagueTyre",
                column: "TyreId");

            migrationBuilder.CreateIndex(
                name: "IX_PointAllotment_SeasonId",
                table: "PointAllotment",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeScore_ResultId",
                table: "PracticeScore",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_QualifyingScore_ResultId",
                table: "QualifyingScore",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_ClimateId",
                table: "Race",
                column: "ClimateId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_SeasonId",
                table: "Race",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Race_TrackId",
                table: "Race",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceClass_SeasonId",
                table: "RaceClass",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceOccurrence_RaceId",
                table: "RaceOccurrence",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_IncidentId",
                table: "Result",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_RaceId",
                table: "Result",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_SeasonDriverId",
                table: "Result",
                column: "SeasonDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_SeasonTeamId",
                table: "Result",
                column: "SeasonTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_TyreId",
                table: "Result",
                column: "TyreId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_LeagueId",
                table: "Season",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonDriver_DriverId",
                table: "SeasonDriver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonDriver_SeasonId",
                table: "SeasonDriver",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonDriver_SeasonTeamId",
                table: "SeasonDriver",
                column: "SeasonTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonEngine_EngineId",
                table: "SeasonEngine",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonEngine_SeasonId",
                table: "SeasonEngine",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_ClassId",
                table: "SeasonTeam",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_ManufacturerId",
                table: "SeasonTeam",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_SeasonEngineId",
                table: "SeasonTeam",
                column: "SeasonEngineId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_SeasonId",
                table: "SeasonTeam",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTeam_TeamId",
                table: "SeasonTeam",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTrait_TraitId",
                table: "TeamTrait",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackTrait_TraitId",
                table: "TrackTrait",
                column: "TraitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "DevelopmentLog");

            migrationBuilder.DropTable(
                name: "DevelopmentRange");

            migrationBuilder.DropTable(
                name: "DriverTrait");

            migrationBuilder.DropTable(
                name: "GivenPenalty");

            migrationBuilder.DropTable(
                name: "LapScore");

            migrationBuilder.DropTable(
                name: "LeagueTyre");

            migrationBuilder.DropTable(
                name: "PointAllotment");

            migrationBuilder.DropTable(
                name: "PracticeScore");

            migrationBuilder.DropTable(
                name: "QualifyingScore");

            migrationBuilder.DropTable(
                name: "RaceOccurrence");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "TeamTrait");

            migrationBuilder.DropTable(
                name: "TrackTrait");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Trait");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "SeasonDriver");

            migrationBuilder.DropTable(
                name: "Tyre");

            migrationBuilder.DropTable(
                name: "Climate");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "SeasonTeam");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "RaceClass");

            migrationBuilder.DropTable(
                name: "SeasonEngine");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Engine");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
