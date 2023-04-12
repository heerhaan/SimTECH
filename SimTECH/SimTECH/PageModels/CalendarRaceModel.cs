namespace SimTECH.PageModels
{
    public class CalendarRaceModel
    {
        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }
        public State State { get; set; }
        public long TrackId { get; set; }

        public string? WinningDriver { get; set; }
        public Country? DriverNationality { get; set; }
        public int? DriverNumber { get; set; }

        public string? WinningTeam { get; set; }
        public Country? TeamNationality { get; set; }
        public string TeamColour { get; set; } = "var(--mud-palette-background)";
        public string TeamAccent { get; set; } = "var(--mud-palette-text-primary)";
    }
}
