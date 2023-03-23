namespace SimTECH.Data.Models
{
    public record PointAllotment
    {
        public long Id { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
    }
}
