﻿namespace SimTECH.Data.Models
{
    public record League
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public int RaceLength { get; set; }
        public State State { get; set; }

        // TODO: Tiers could be helpful in adding them to the same races but it's a bigger thing, requires total design
        //public int Tier { get; set; }

        // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

        public IList<DevelopmentRange>? DevelopmentRanges { get; set; }
        public IList<Season>? Seasons { get; set; }
        public IList<Contract>? Contracts { get; set; }
    }
}
