namespace SimTECH.Data.Models
{
    public record StrategyTyre
    {
        public long Id { get; set; }

        public long StrategyId { get; set; }
        public Strategy Strategy { get; set; } = default!;
        public long TyreId { get; set; }
        public Tyre Tyre { get; set; } = default!;
    }
}
