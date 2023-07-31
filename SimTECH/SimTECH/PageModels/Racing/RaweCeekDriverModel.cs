using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.PageModels.Racing;

public class RaweCeekDriver
{
    public long SeasonDriverId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Country Nationality { get; set; }
    public int Number { get; set; }
    public TeamRole Role { get; set; }

    public long SeasonTeamId { get; set; }
    public string TeamName { get; set; }
    public string Colour { get; set; }
    public string Accent { get; set; }

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
    public bool HasFastestLap { get; set; }

    public int? Penalty { get; set; }
    public string? Reasons { get; set; }

    public long ResultId { get; set; }
    public int Grid { get; set; }
    public int Position { get; set; }
    public int Score { get; set; }
    public RaceStatus Status { get; set; }
    //public int Setup { get; set; }
    public int TyreLife { get; set; }
    public bool FastestLap { get; set; }
    public int Overtaken { get; set; }
    public int Defended { get; set; }

    public Tyre Tyre { get; set; }
    public Incident? Incident { get; set; }

    public string FullName => FirstName + " " + LastName;
}

public static class ExtendRaweCeekDriver
{
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

            TeamName = driver.TeamName,
            Colour = driver.Colour,
            Accent = driver.Accent,

            Power = driver.RacePower,
            Attack = driver.Attack,
            Defense = driver.Defense,

            Status = driver.Status,
            Position = driver.Position,
            Grid = driver.Grid,
            //Setup = driver.Setup,
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

            HasFastestLap = driver.HasFastestLap,
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
            Score = driver.Score,
            Status = driver.Status,
            //Setup = Setup,
            TyreLife = driver.TyreLife,
            FastestLap = driver.HasFastestLap,
            Overtaken = driver.Overtaken,
            Defended = driver.Defended,

            SeasonDriverId = driver.SeasonDriverId,
            SeasonTeamId = driver.SeasonTeamId,
            RaceId = raceId,
            TyreId = driver.Tyre.Id,
            IncidentId = driver.Incident?.Id,
        };
    }

    public static ScoredPoints MapToScoredPoints(this RaweCeekDriver driver, Dictionary<int, int> allotments, int polePoints, int fastLapPoints)
    {
        var points = 0;
        var hiddenPoints = 0;

        if (driver.Grid == 1)
            points += polePoints;

        if (driver.HasFastestLap)
            points += fastLapPoints;

        if (driver.Status == RaceStatus.Racing)
        {
            if (allotments.ContainsKey(driver.Position))
                points += allotments[driver.Position];

            hiddenPoints = ((double)200000 / driver.Position).RoundDouble();
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
