namespace SimTECH.Data.Models
{
    public record SeasonTeam
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Principal { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public string Accent { get; set; } = default!;

        public int Points { get; set; }
        public int HiddenPoints { get; set; }

        public int BaseValue { get; set; }
        public int Aero { get; set; }
        public int Chassis { get; set; }
        public int Powertrain { get; set; }//More like a "drivetrain" in this instance
        public int Reliability { get; set; }

        //public int StandingsGoal { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; } = default!;
        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public long SeasonEngineId { get; set; }
        public SeasonEngine SeasonEngine { get; set; } = default!;
        public long ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = default!;

        public IList<SeasonDriver>? SeasonDrivers { get; set; }
        public IList<Result>? Results { get; set; }
    }
}
