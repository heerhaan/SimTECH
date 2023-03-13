namespace SimTECH.Data.Models
{
    public record Track
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public Country Country { get; set; }
        public double Length { get; set; }
        public State State { get; set; }

        public double AeroMod { get; set; }
        public double ChassisMod { get; set; }
        public double PowerMod { get; set; }
        public double QualifyingMod { get; set; }

        public IList<Race>? Races { get; set; }
        public IList<TrackTrait>? TrackTraits { get; set; }
    }
}
