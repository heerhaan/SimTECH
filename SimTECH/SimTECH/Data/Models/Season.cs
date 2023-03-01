namespace SimTECH.Data.Models
{
    public class Season
    {
        public long Id { get; set; }
        public State State { get; set; }
        public int Year { get; set; }

        public int MaximumDriversInRace { get; set; }
        public int QualifyingAmountQ2 { get; set; }
        public int QualifyingAmountQ3 { get; set; }
        public int QualifyingRNG { get; set; }
        public int RunAmountSession { get; set; }
        public int GridBonus { get; set; }
        public int PitMinimum { get; set; }
        public int PitMaximum { get; set; }

        public int PointsPole { get; set; }
        public int PointsFastestLap { get; set; }

        public IList<PointAllotment> PointAllotments { get; set; } = default!;
        public IList<Race> Races { get; set; } = default!;
        public IList<SeasonDriver> SeasonDrivers { get; set; } = default!;
        public IList<SeasonTeam> SeasonTeams { get; set; } = default!;
        public IList<SeasonEngine> SeasonEngines { get; set; } = default!;

        public long LeagueId { get; set; }
        public League League { get; set; } = default!;
    }
}
