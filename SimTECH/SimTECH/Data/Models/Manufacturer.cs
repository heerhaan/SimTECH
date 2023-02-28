namespace SimTECH.Data.Models
{
    public class Manufacturer
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }

        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Pace { get; set; }
        public int WearMaximum { get; set; }
        public int WearMinimum { get; set; }
    }
}
