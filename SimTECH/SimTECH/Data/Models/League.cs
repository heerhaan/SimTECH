namespace SimTECH.Data.Models
{
    public record League
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }

        // TODO: Tiers could be helpful in adding them to the same races but it's a bigger thing, requires total design
        //public int Tier { get; set; }

        // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

        public IList<DevelopmentRange> DevelopmentRanges { get; set; } = default!;
        public IList<Season> Seasons { get; set; } = default!;
        public IList<Contract> Contracts { get; set; } = default!;
    }
}
