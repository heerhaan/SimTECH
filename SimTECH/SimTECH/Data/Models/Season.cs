namespace SimTECH.Data.Models
{
    public class Season
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }
    }
}
