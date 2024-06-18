using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class TraitFactory
{
    public static Trait GenerateRainDriverTrait => new()
    {
        Name = "Rainmeister",
        Description = "Faster when it's wet",
        Type = Entrant.Driver,
        ForWetConditions = true,
        QualifyingPace = 3,
        RacePace = 5,
        DriverReliability = 3,
    };

    public static Trait GenerateManufacturerTeamTrait => new()
    {
        Name = "Manufacturer",
        Description = "Owns the engine",
        Type = Entrant.Team,
        QualifyingPace = 1,
        RacePace = 1,
        EngineReliability = 2,
    };

    public static Trait GenerateStreetTrackTrait => new()
    {
        Name = "Street Circuit",
        Description = "Street is a circuit",
        Type = Entrant.Track,
        DriverReliability = -2,
        CarReliability = -1,
        WearMax = 2,
        RngMin = -5,
        Defense = 10,
    };
}
