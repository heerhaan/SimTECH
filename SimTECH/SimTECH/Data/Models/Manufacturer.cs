namespace SimTECH.Data.Models
{
    public sealed class Manufacturer
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public string Accent { get; set; } = default!;
        public State State { get; set; }

        public int Pace { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
    }
}
