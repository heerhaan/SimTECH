using SimTECH.Common.Enums;

namespace SimTECH.Data.Models
{
    public sealed class Trait : ModelState
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Entrant Type { get; set; }

        public int QualifyingPace { get; set; }
        public int RacePace { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
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
                assignedValues.Add("Qualy pace", trait.QualifyingPace);
            if (trait.RacePace != 0)
                assignedValues.Add("Race pace", trait.RacePace);
            if (trait.Attack != 0)
                assignedValues.Add("Attack", trait.Attack);
            if (trait.Defense != 0)
                assignedValues.Add("Defense", trait.Defense);
            if (trait.DriverReliability != 0)
                assignedValues.Add("Driver rel.", trait.DriverReliability);
            if (trait.CarReliability != 0)
                assignedValues.Add("Car rel.", trait.CarReliability);
            if (trait.EngineReliability != 0)
                assignedValues.Add("Engine rel.", trait.EngineReliability);
            if (trait.WearMin != 0)
                assignedValues.Add("Min. wear mod", trait.WearMin);
            if (trait.WearMax != 0)
                assignedValues.Add("Max. wear mod", trait.WearMax);
            if (trait.RngMin != 0)
                assignedValues.Add("Rng min", trait.RngMin);
            if (trait.RngMax != 0)
                assignedValues.Add("Rng max", trait.RngMax);

            return assignedValues;
        }
    }
}
