namespace SimTECH.PageModels.Racing
{
    public abstract class DriverBase
    {
        public long ResultId { get; set; }
        public long SeasonDriverId { get; set; }
        public long SeasonTeamId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
    }
}
