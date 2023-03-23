namespace SimTECH.Data.Models
{
    // TODO: Implementation for sponsor needs to designed and implemented still
    public record Sponsor
    {
        public long Id { get; set; }
        public int Name { get; set; }
        public State State { get; set; }
    }
}
