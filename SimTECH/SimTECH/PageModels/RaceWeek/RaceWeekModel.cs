using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek;

public class RaweCeekModel
{
    public Race Race { get; set; }
    public Climate Climate { get; set; }
    public Season Season { get; set; }

    public LeagueOptions LeagueOptions { get; set; }

    public List<RaweCeekDriver> RaweCeekDrivers { get; set; }

    public List<long> ConsumablePenalties { get; set; } = new();

    public double GapMarge { get; set; }

    // Update the raweceek driver references based upon the race driver results
    public void SetRacerData(List<RaceDriver> raceDrivers)
    {
        foreach (var raceDriver in raceDrivers)
        {
            var matchDriver = RaweCeekDrivers.First(e => e.ResultId == raceDriver.ResultId);

            matchDriver.Position = raceDriver.Position;
            matchDriver.AbsolutePosition = raceDriver.AbsolutePosition;
            matchDriver.Score = raceDriver.LapSum;
            matchDriver.Status = raceDriver.Status;
            matchDriver.TyreLife = raceDriver.TyreLife;
            matchDriver.Tyre = raceDriver.CurrentTyre;
            matchDriver.FastestLap = raceDriver.HasFastestLap;
            matchDriver.Overtaken = raceDriver.OvertakeCount;
            matchDriver.Defended = raceDriver.DefensiveCount;
            matchDriver.Incident = raceDriver.Incident;
        }
    }
}

public static class ExtendRaweCeek
{
    public static Dictionary<long, long[]> ResultsGroupedByRaceClass(this RaweCeekModel raweCeek)
    {
        return raweCeek.RaweCeekDrivers
            .GroupBy(e => e.ClassId)
            .ToDictionary(e => e.Key, e => e.Select(rd => rd.ResultId).ToArray());
    }
}
