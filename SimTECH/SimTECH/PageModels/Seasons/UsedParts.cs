using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.PageModels.Seasons;

public class UsedParts
{
    public string Name { get; set; }
    public int Number { get; set; }
    public Country Country { get; set; }
    public string Team { get; set; }
    public string Colour { get; set; }
    public string Accent { get; set; }

    public Dictionary<Incident, int> AmountPenalizableIncidents { get; set; } = new();
}
