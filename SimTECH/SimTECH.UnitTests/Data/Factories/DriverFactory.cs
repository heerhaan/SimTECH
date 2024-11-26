using SimTECH.Common.Constants;
using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class DriverFactory
{
    public static Driver GenerateTestDriver() => new()
    {
        FirstName = "Firstname",
        LastName = "Lastname",
        Abbreviation = "TES",
        DateOfBirth = DateTime.Now,
        Country = Globals.DefaultCountry,
        Biography = "somehow required",
    };

    public static Driver GenerateBestDriver() => new()
    {
        FirstName = "Max",
        LastName = "Verstappen",
        Abbreviation = "VER",
        DateOfBirth = DateTime.Now,
        Country = Common.Enums.Country.NL,
        Biography = "why do we need one",
        Mark = true,
    };
}
