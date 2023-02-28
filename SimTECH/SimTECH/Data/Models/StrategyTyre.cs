namespace SimTECH.Data.Models
{
    public class StrategyTyre
    {
        public int Id { get; set; }
        public int NumberStint { get; set; }

        public int StrategyId { get; set; }
        public Strategy Strategy { get; set; }
        public int TyreId { get; set; }
        public Tyre Tyre { get; set; }
    }
}
