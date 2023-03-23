namespace SimTECH.Data.Models
{
    public record TeamTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = default!;
        public long TeamId { get; set; }
        public Team Team { get; set; } = default!;
    }
}
