namespace SimTECH.Data.Models
{
    public class RaceClass : ModelBase
    {
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;

        public long SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
