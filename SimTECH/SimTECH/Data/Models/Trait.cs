namespace SimTECH.Data.Models
{
    public class Trait
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }
        public string Description { get; set; } = default!;
        public Participant Type { get; set; }

        public int QualifyingPace { get; set; }
        public int DriverPace { get; set; }
        public int CarPace { get; set; }
        public int EnginePace { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int WearMaximum { get; set; }
        public int WearMinimum { get; set; }
        public int MaxRNG { get; set; }
        public int MinRNG { get; set; }

        public bool ForWetConditions { get; set; }

        public IList<DriverTrait> DriverTraits { get; set; } = null!;
        public IList<TeamTrait> TeamTraits { get; set; } = null!;
        public IList<TrackTrait> TrackTraits { get; set; } = null!;
    }

    public class DriverTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
        public long DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
    }

    public class TeamTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
        public long TeamId { get; set; }
        public Team Team { get; set; } = null!;
    }

    public class TrackTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
        public long TrackId { get; set; }
        public Track Track { get; set; } = null!;
    }
}
