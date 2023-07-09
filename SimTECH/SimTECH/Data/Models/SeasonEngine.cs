namespace SimTECH.Data.Models
{
    public sealed class SeasonEngine : ModelBase
    {
        public string Name { get; set; } = default!;
        public int Power { get; set; } = 50;
        public int Reliability { get; set; } = 1000;
        public bool Rebadged { get; set; }

        public long EngineId { get; set; }
        public Engine Engine { get; set; } = default!;
        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;

        public IList<SeasonTeam>? SeasonTeams { get; set; }
    }

    public static class ExtendSeasonEngine
    {
        public static int RetrieveAspectValue(this SeasonEngine engine, Aspect aspect) => aspect switch
        {
            Aspect.Engine => engine.Power,
            Aspect.Reliability => engine.Reliability,
            _ => throw new InvalidOperationException("Invalid aspect for this entrant")
        };
    }
}
