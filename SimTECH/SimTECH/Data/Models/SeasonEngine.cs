namespace SimTECH.Data.Models
{
    public record SeasonEngine
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public int Power { get; set; }
        public int Reliability { get; set; }
        public bool Rebadged { get; set; }

        public IList<SeasonTeam> SeasonTeams { get; set; } = default!;

        public long EngineId { get; set; }
        public Engine Engine { get; set; } = default!;
        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
    }
}
