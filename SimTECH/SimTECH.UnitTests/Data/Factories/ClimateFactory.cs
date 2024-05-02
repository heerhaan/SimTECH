using SimTECH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTECH.UnitTests.Data.Factories;

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
