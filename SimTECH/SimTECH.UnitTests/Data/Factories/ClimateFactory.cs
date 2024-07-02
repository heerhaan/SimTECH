using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class ClimateFactory
{
    public static Climate ClimateDry => new()
    {
        Terminology = "Dry",
        IsWet = false,
        EngineMultiplier = 1d,
        ReliablityModifier = 0,
        RngModifier = 0,
    };

    public static Climate ClimateWet => new()
    {
        Terminology = "Wet",
        IsWet = true,
        EngineMultiplier = 1d,
        ReliablityModifier = 0,
        RngModifier = 0,
    };
}
