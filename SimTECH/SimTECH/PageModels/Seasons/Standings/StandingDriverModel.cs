using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Standings
{
    public class StandingDriverModel : StandingEntrantBase
    {
        public int Number { get; set; }
        public TeamRole TeamRole { get; set; }
        public string Team { get; set; }
        public long? SeasonTeamId { get; set; }
    }
}
