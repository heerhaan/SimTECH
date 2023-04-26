﻿namespace SimTECH.Data.Models
{
    public class SeasonTeam
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Principal { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public string Accent { get; set; } = default!;

        public int Points { get; set; }
        public int HiddenPoints { get; set; }

        public int BaseValue { get; set; } = 100;
        public int Aero { get; set; } = 10;
        public int Chassis { get; set; } = 10;
        public int Powertrain { get; set; } = 10;//More like a "drivetrain" in this instance
        public int Reliability { get; set; } = 1000;

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
