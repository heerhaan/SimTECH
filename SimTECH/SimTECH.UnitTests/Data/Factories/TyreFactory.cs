using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.UnitTests.Data.Factories;

public static class TyreFactory
{
    public static Tyre GenerateSoftTyre => new()
    {
        Name = "Soft",
        Colour = "#fa0536ff",
        Pace = 200,
        MinimumLife = -25,
        PitWhenBelow = 20,
        WearMin = 15,
        WearMax = 25,
        DistanceMin = 50,
        DistanceMax = 125,
        State = State.Active,
    };

    public static List<Tyre> GenerateDefaultListTyres =>
        [
            new()
            {
                Name = "Soft",
                Colour = "#fa0536ff",
                Pace = 200,
                MinimumLife = -25,
                PitWhenBelow = 20,
                WearMin = 15,
                WearMax = 25,
                DistanceMin = 50,
                DistanceMax = 125,
                State = State.Active,
            },
            new()
            {
                Name = "Medium", Colour = "#f4ea26ff", Pace = 180, MinimumLife = -25, PitWhenBelow = 15, WearMin = 9, WearMax = 15, DistanceMin = 125, DistanceMax = 999, State = State.Active,
            },
            new()
            {
                Name = "Hard", Colour = "#dfdde9ff", Pace = 160, MinimumLife = -25, PitWhenBelow = 10, WearMin = 6, WearMax = 10, DistanceMin = 175, DistanceMax = 999, State = State.Active,
            },
            new()
            {
                Name = "Grooved", Colour = "#bded80ff", Pace = 100, MinimumLife = -25, WearMin = 1, WearMax = 3, DistanceMin = 100, DistanceMax = 999, State = State.Closed,
            },
            new()
            {
                Name = "Wet",
                Colour = "#3399ffff",
                Pace = 50,
                MinimumLife = -25,
                WearMin = 0,
                WearMax = 1,
                DistanceMin = 50,
                DistanceMax = 999,
                ForWet = true,
                State = State.Active,
            }
        ];
}
