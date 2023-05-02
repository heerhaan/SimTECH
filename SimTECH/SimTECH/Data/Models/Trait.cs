namespace SimTECH.Data.Models
{
    public sealed class Trait
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Entrant Type { get; set; }
        public State State { get; set; }

        public int QualifyingPace { get; set; }
        public int RacePace { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMax { get; set; }
        public int WearMin { get; set; }
        public int RngMin { get; set; }
        public int RngMax { get; set; }

        public bool ForWetConditions { get; set; }

        public IList<DriverTrait> DriverTraits { get; set; }
        public IList<TeamTrait> TeamTraits { get; set; }
        public IList<TrackTrait> TrackTraits { get; set; }
    }
}
