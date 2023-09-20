using System.ComponentModel.DataAnnotations.Schema;

namespace SimTECH.Data.Models
{
    public sealed class Season : ModelState
    {
        public int Year { get; set; }

        public int MaximumDriversInRace { get; set; }
        public int QualifyingAmountQ2 { get; set; }
        public int QualifyingAmountQ3 { get; set; }
        public int QualifyingRNG { get; set; }
        public int RunAmountSession { get; set; }
        public int GridBonus { get; set; }
        public int PitMinimum { get; set; }
        public int PitMaximum { get; set; }
        public int PitCostSubtractCaution { get; set; }
        public int RngMinimum { get; set; }
        public int RngMaximum { get; set; }
        public int MistakeRolls { get; set; }
        public int MistakeMinimum { get; set; }
        public int MistakeMaximum { get; set; }

        public int PointsPole { get; set; }
        public int PointsFastestLap { get; set; }

        public QualyFormat QualifyingFormat { get; set; }

        public IList<PointAllotment>? PointAllotments { get; set; }
        public IList<Race>? Races { get; set; }
        public IList<SeasonDriver>? SeasonDrivers { get; set; }
        public IList<SeasonTeam>? SeasonTeams { get; set; }
        public IList<SeasonEngine>? SeasonEngines { get; set; }
        public IList<RaceClass> RaceClasses { get; set; }

        public long LeagueId { get; set; }
        public League League { get; set; } = default!;

        [NotMapped]
        public bool HasRaceClasses => RaceClasses?.Any() == true;
    }
}
