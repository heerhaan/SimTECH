namespace SimTECH.Data.Models
{
    public class DriverTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = null!;
        public long DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
    }
}
