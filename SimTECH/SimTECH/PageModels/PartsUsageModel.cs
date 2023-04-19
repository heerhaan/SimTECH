namespace SimTECH.PageModels
{
    public class PartsUsageModel
    {
        public long SeasonDriverId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Team { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Accidents { get; set; }
        public int Collisions { get; set; }
        public int Engines { get; set; }
        public int Electrics { get; set; }
        public int Hydraulics { get; set; }
        public int TotalDnf { get; set; }
        public int TotalDsq { get; set; }
    }
}
