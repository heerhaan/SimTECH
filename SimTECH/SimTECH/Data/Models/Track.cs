namespace SimTECH.Data.Models
{
    public class Track
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public double Length { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }

        public double AeroMod { get; set; }
        public double ChassisMod { get; set; }
        public double PowerMod { get; set; }
        public double QualifyingMod { get; set; }

        public IList<Race> Races { get; set; } = default!;
        public IList<TrackTrait> TrackTraits { get; set; } = default!;
    }
}
