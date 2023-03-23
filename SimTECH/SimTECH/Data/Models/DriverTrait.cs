namespace SimTECH.Data.Models
{
    public record DriverTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
        public long DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
    }
}
