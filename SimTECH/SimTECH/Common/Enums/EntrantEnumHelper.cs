namespace SimTECH.Common.Enums;

public enum Entrant
{
    None = 0,
    Driver,
    Team,
    Track,
    Engine,
}

public static class EntrantEnumHelper
{
    public static readonly Dictionary<Entrant, string> GetEntrantSelection = new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

    public static Entrant[] GetReliabilityEntrants => [Entrant.Driver, Entrant.Team, Entrant.Engine];
}
