using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class ManufacturerFactory
{
    public static Manufacturer GenerateTestManufacturer => new()
    {
        Name = "Hankook",
        Colour = "#0b0b0d",
        Accent = "#e56103",
        Pace = 0,
        WearMin = 0,
        WearMax = 0,
        State = State.Active
    };
}
