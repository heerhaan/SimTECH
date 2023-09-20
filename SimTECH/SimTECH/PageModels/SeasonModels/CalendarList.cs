using SimTECH.Data.Models;

namespace SimTECH.PageModels.SeasonModels;

public class CalendarList
{
    public long RaceId { get; set; }
    public int Round { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public State State { get; set; }
    public string Weather { get; set; }
    public string WeatherIcon { get; set; }
    public string WeatherColour { get; set; }
    public long TrackId { get; set; }

    public CompactDriver? PoleSitter { get; set; }
    public CompactDriver? DriverWinner { get; set; }
    public CompactTeam? TeamWinner { get; set; }

    public Race Race { get; set; }
}
