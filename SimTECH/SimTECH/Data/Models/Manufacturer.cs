namespace SimTECH.Data.Models
{
    public sealed class Manufacturer : ModelState
    {
        public string Name { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public string Accent { get; set; } = default!;

        public int Pace { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
    }
}
