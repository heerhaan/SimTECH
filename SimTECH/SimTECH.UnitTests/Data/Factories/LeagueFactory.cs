using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class LeagueFactory
{
    public static League GenerateTestLeague => new()
    {
        Id = 1,
        Name = "Formula 0",
        RaceLength = 300,
    };
}
