using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons
{
    public class SeasonListModel
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public State State { get; set; }

        public string League { get; set; }

        public string DriverName { get; set; }
        public int DriverNumber { get; set; }
        public Country DriverNationality { get; set; }
        public string DriverColour { get; set; }
        public string DriverAccent { get; set; }

        public string TeamName { get; set; }
        public string TeamColour { get; set; }
        public Country TeamNationality { get; set; }
    }
}
