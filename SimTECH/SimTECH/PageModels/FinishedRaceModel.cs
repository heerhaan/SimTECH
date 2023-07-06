namespace SimTECH.PageModels
{
    public class FinishedRaceModel
    {
        public long RaceId { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        public Country Country { get; set; }
        public string LeagueName { get; set; }

        public CompactDriver WinningDriver { get; set; }
        public CompactTeam WinningTeam { get; set; }
    }
}
