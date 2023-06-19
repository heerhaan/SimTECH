namespace SimTECH.PageModels.StandingEntrants
{
    public class StandingDriverModel : StandingEntrantBase
    {
        public int Number { get; set; }
        public TeamRole TeamRole { get; set; }
        public string Team { get; set; }
        public long? SeasonTeamId { get; set; }
    }
}
