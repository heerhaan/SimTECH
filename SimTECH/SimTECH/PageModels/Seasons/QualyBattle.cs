using SimTECH.Data.Models;

namespace SimTECH.PageModels.Seasons
{
    public class QualyBattle
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public Country Country { get; set; }
        public int VictoryCount { get; set; }

        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public SeasonTeam Team { get; set; }
    }
}
