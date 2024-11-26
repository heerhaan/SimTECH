using SimTECH.Common.Constants;
using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class TeamFactory
{
    public static Team GenerateTestTeam => new()
    {
        Name = "Test",
        Country = Globals.DefaultCountry,
        Biography = "Yep, a team",
    };
}
