namespace SimTECH.Data.Models
{
    public class StrategyTyre
    {
        public long Id { get; set; }
        public int NumberStint { get; set; }

        public int Order { get; set; } //temp

        public long StrategyId { get; set; }
        public Strategy Strategy { get; set; } = default!;
        public long TyreId { get; set; }
        public Tyre Tyre { get; set; } = default!;
    }
}
