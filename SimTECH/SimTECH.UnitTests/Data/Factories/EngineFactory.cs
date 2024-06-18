using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class EngineFactory
{
    public static Engine GenerateTestEngine => new()
    {
        Name = "Engine",
        Colour = "#FFFFFF",
        Accent = "#FFFFFF",
    };
}
