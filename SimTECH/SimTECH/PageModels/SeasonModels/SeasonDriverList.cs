using SimTECH.Data.Models;

namespace SimTECH.PageModels.SeasonModels;

public class SeasonDriverList
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Number { get; set; }
    public Country Country { get; set; }
    public TeamRole Role { get; set; }
    public int Skill { get; set; }
    public int Reliability { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string Team { get; set; } = string.Empty;
    public string Colour { get; set; } = string.Empty;
    public string Accent { get; set; } = string.Empty;

    public SeasonDriver SeasonDriver { get; set; }
}
