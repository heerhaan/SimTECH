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

    public static class ExtendTrait
    {
        public static Dictionary<string, int> RetrieveNotZeroValues(this Trait trait)
        {
            var assignedValues = new Dictionary<string, int>();

            if (trait.QualifyingPace != 0)
                assignedValues.Add("Qualifying pace", trait.QualifyingPace);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Race pace", trait.RacePace);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Driver reliability", trait.DriverReliability);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Car reliability", trait.CarReliability);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Engine reliability", trait.EngineReliability);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Wear max", trait.WearMax);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Wear min", trait.WearMin);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Rng min", trait.RngMin);
            if (trait.QualifyingPace != 0)
                assignedValues.Add("Rng max", trait.RngMax);

            return assignedValues;
        }
    }
}
