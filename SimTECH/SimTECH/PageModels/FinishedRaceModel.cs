namespace SimTECH.PageModels
{
    public class FinishedRaceModel
    {
        public long RaceId { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public Country Country { get; set; }
        public string LeagueName { get; set; }

        public DriverWinner WinningDriver { get; set; }
        public TeamWinner WinningTeam { get; set; }
    }
}
