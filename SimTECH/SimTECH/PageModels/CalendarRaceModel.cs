using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class CalendarRaceModel
    {
        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public string Weather { get; set; }
        public string WeatherIcon { get; set; }
        public long TrackId { get; set; }

        public DriverWinner PoleSitter { get; set; }
        public DriverWinner DriverWinner { get; set; }
        public TeamWinner TeamWinner { get; set; }
    }
}
