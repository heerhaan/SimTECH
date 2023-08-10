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
        //public int Setup { get; set; }
        public int TyreLife { get; set; }
        public Tyre CurrentTyre { get; set; }

        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMaxMod { get; set; }
        public int WearMinMod { get; set; }
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
            driver.LapScores.ConvertAll(e => (NumberHelper.Percentage(1, calculationCount), e.TyreColour ?? Constants.DefaultColour));
    }
}
