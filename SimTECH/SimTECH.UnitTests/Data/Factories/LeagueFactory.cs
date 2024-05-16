using SimTECH.Data.Models;

namespace SimTECH.UnitTests.Data.Factories;

public static class LeagueFactory
{
    public static League GenerateTestLeague => new()
    {
        Name = "Formula 0",
        RaceLength = 300,
    };
}
