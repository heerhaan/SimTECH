using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Common;

namespace SimTECH.PageModels.RaceWeek;

// TODO: Refactor this model away, retrieve necessary objects per tab
public class RaweCeekDriver
{
    public long SeasonDriverId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Country Nationality { get; set; }
    public int Number { get; set; }
    public TeamRole Role { get; set; }
    public StrategyPreference StrategyPreference { get; set; }

    public long SeasonTeamId { get; set; }
    public string TeamName { get; set; }
    public string Colour { get; set; }
    public string Accent { get; set; }
    public CarParts Parts { get; set; }

    public long ClassId { get; set; }
    public RaceClass? Class { get; set; }

    public long ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    public string ManufacturerColour { get; set; }
    public string ManufacturerAccent { get; set; }

    public int QualyPower { get; set; }
    public int RacePower { get; set; }

    public int Attack { get; set; }
    public int Defense { get; set; }
    public int DriverReliability { get; set; }
    public int CarReliability { get; set; }
    public int EngineReliability { get; set; }
    public int WearMinMod { get; set; }
    public int WearMaxMod { get; set; }
    public int RngMinMod { get; set; }
    public int RngMaxMod { get; set; }
    public int LifeBonus { get; set; }

    public int? Penalty { get; set; }

    public long ResultId { get; set; }
    public int Grid { get; set; }
    public int Position { get; set; }
    public int AbsoluteGrid { get; set; }
    public int AbsolutePosition { get; set; }
    public int Score { get; set; }
    public RaceStatus Status { get; set; }
    public int Setup { get; set; }
    public int TyreLife { get; set; }
    public bool FastestLap { get; set; }
    public int Overtaken { get; set; }
    public int Defended { get; set; }

    public List<string> TraitNames { get; set; } = [];

    public Engine Engine { get; set; }
    public Tyre Tyre { get; set; }
    public Incident? Incident { get; set; }

    public int ExpectedPosition { get; set; }
    public int RelativePower => (QualyPower / 2) + RacePower;
    public string FullName => FirstName + " " + LastName;
}

// Unused for now but it should eventually replace the one above
public class FixedRaweCeekDriver
{
    public SeasonDriver SeasonDriver { get; set; }

    public SeasonTeam SeasonTeam { get; set; }

    public SeasonEngine SeasonEngine { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public RaceClass? Class { get; set; }

    public Result Result { get; set; }

    public Tyre Tyre { get; set; }
}

public static class ExtendRaweCeekDriver
{
    private const double baseHiddenScoreValue = 10000000000000000000;

    public static SessionDriver MapToSessionDriver(this RaweCeekDriver driver, int amountRuns)
    {
        return new SessionDriver
        {
            SeasonDriverId = driver.SeasonDriverId,
            ResultId = driver.ResultId,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Nationality = driver.Nationality,
            Number = driver.Number,
            TeamName = driver.TeamName,
            Colour = driver.Colour,
            Accent = driver.Accent,
            Power = driver.QualyPower,
            ClassId = driver.ClassId,
            Class = driver.Class,
            Scores = new int[amountRuns]
        };
    }

    public static RaceDriver MapToRaceDriver(this RaweCeekDriver driver)
    {
        return new RaceDriver
        {
            ResultId = driver.ResultId,
            SeasonDriverId = driver.SeasonDriverId,
            SeasonTeamId = driver.SeasonTeamId,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            FullName = driver.FullName,
            Nationality = driver.Nationality,
            Number = driver.Number,
            Role = driver.Role,
            StrategyPreference = driver.StrategyPreference,

            TeamName = driver.TeamName,
            Colour = driver.Colour,
            Accent = driver.Accent,

            ClassId = driver.ClassId,
            Class = driver.Class,

            Power = driver.RacePower + driver.Setup,
            Attack = driver.Attack,
            Defense = driver.Defense,

            Status = driver.Status,
            Position = driver.Position,
            Grid = driver.Grid,
            AbsolutePosition = driver.AbsolutePosition,
            AbsoluteGrid = driver.AbsoluteGrid,
            Setup = driver.Setup,
            TyreLife = driver.TyreLife,
            CurrentTyre = driver.Tyre,
            DriverReliability = driver.DriverReliability,
            CarReliability = driver.CarReliability,
            EngineReliability = driver.EngineReliability,
            WearMinMod = driver.WearMinMod,
            WearMaxMod = driver.WearMaxMod,
            RngMinMod = driver.RngMinMod,
            RngMaxMod = driver.RngMaxMod,
            LifeBonus = driver.LifeBonus,

            HasFastestLap = driver.FastestLap,
            OvertakeCount = driver.Overtaken,
            DefensiveCount = driver.Defended,
            Incident = driver.Incident,
        };
    }

    public static Result MapToResult(this RaweCeekDriver driver, long raceId)
    {
        return new Result
        {
            Id = driver.ResultId,
            Grid = driver.Grid,
            Position = driver.Position,
            AbsoluteGrid = driver.AbsoluteGrid,
            AbsolutePosition = driver.AbsolutePosition,
            Score = driver.Score,
            Status = driver.Status,
            Setup = driver.Setup,
            TyreLife = driver.TyreLife,
            FastestLap = driver.FastestLap,
            Overtaken = driver.Overtaken,
            Defended = driver.Defended,

            SeasonDriverId = driver.SeasonDriverId,
            SeasonTeamId = driver.SeasonTeamId,
            RaceId = raceId,
            TyreId = driver.Tyre.Id,
            IncidentId = driver.Incident?.Id,
        };
    }

    public static ScoredPoints MapToScoredPoints(this RaweCeekDriver driver,
        Dictionary<int, int> allotments, int polePoints, int fastLapPoints)
    {
        var points = 0;
        var hiddenPoints = 0d;

        if (driver.Grid == 1)
            points += polePoints;

        if (driver.FastestLap)
            points += fastLapPoints;

        if (driver.Status == RaceStatus.Racing)
        {
            if (allotments.ContainsKey(driver.Position))
                points += allotments[driver.Position];

            double divider = Math.Pow(10, driver.Position) / 10;
            hiddenPoints = baseHiddenScoreValue / divider;
        }

        return new ScoredPoints
        {
            SeasonDriverId = driver.SeasonDriverId,
            SeasonTeamId = driver.SeasonTeamId,
            Points = points,
            HiddenPoints = hiddenPoints,
        };
    }
}
