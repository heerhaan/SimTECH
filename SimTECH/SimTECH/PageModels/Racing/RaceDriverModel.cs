using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.PageModels.Racing
{
    public class RaceDriver : DriverBase
    {
        public RaceStatus Status { get; set; }
        public Incident? Incident { get; set; }

        public int Position { get; set; }
        public int Grid { get; set; }
        public int AbsolutePosition { get; set; }
        public int AbsoluteGrid { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }
        public Tyre CurrentTyre { get; set; }

        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMinMod { get; set; }
        public int WearMaxMod { get; set; }
        public int RngMinMod { get; set; }
        public int RngMaxMod { get; set; }
        public int LifeBonus { get; set; }

        public bool HasFastestLap { get; set; }
        public bool InstantOvertaken { get; set; }
        public bool RecentMistake { get; set; }

        public List<LapScore> LapScores { get; set; } = new();

        public int LapSum => LapScores.Sum(e => e.Score);
        public int GridChange => Grid - Position;
        public int LastScore { get; set; }
        public int OvertakeCount { get; set; }
        public int DefensiveCount { get; set; }
        public string? SingleOccurrence { get; set; }
    }

    public static class ExtendRaceDriver
    {
        public static List<(double, string)> ColoursOfUsedTyres(this RaceDriver driver, int calculationCount) =>
            driver.LapScores.ConvertAll(e => (NumberHelper.Percentage(1, calculationCount), e.TyreColour ?? Generals.DefaultColour));

        public static int QualifyingBonus(this RaceDriver driver, int racerCount, int gridBonus) =>
            (racerCount * gridBonus) - ((driver.AbsoluteGrid - 1) * gridBonus);

        public static void SetPositions(this List<RaceDriver> raceDrivers, double gapMarge)
        {
            int absoluteIndex = 0;
            int scoreAboveDriver = 0;

            var positionIndexDict = raceDrivers.Select(e => e.ClassId).Distinct().ToDictionary(e => e, e => 0);

            foreach (var driver in raceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
            {
                driver.Position = ++positionIndexDict[driver.ClassId];
                driver.AbsolutePosition = ++absoluteIndex;

                driver.GapAbove = driver.AbsolutePosition == 1
                    ? "LEADER"
                    : "+" + (Math.Round((scoreAboveDriver - driver.LapSum) * gapMarge, 2)).ToString("F2");

                scoreAboveDriver = driver.LapSum;
            }
        }
    }
}
