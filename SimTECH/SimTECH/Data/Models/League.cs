namespace SimTECH.Data.Models
{
    public class League
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }
        //public int Tier { get; set; }

        // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

        public IList<Range> DevelopmentRanges { get; set; }
        public IList<Season> Seasons { get; set; }
    }
}
