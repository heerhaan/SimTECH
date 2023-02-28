namespace SimTECH.Data.Models
{
    public class Track
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }
        public double Length { get; set; }
        public string Country { get; set; } = default!;

        public double AeroMod { get; set; }
        public double ChassisMod { get; set; }
        public double PowerMod { get; set; }

        public IList<Race> Races { get; set; }
        public IList<TrackTrait> TrackTraits { get; set; }
    }
}
