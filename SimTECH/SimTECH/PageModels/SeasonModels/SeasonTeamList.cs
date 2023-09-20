using SimTECH.Data.Models;

namespace SimTECH.PageModels.SeasonModels;

public class SeasonTeamList
{
    public string Name { get; set; } = string.Empty;
    public Country Country { get; set; }
    public string Principal { get; set; } = string.Empty;
    public string Colour { get; set; } = string.Empty;
    public int BaseValue { get; set; }
    public int Aero { get; set; }
    public int Chassis { get; set; }
    public int Powertrain { get; set; }
    public int Reliability { get; set; }
    public string Engine { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string ManColour { get; set; } = string.Empty;
    public string ManAccent { get; set; } = string.Empty;

    public SeasonTeam SeasonTeam { get; set; }
}
