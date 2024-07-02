using SimTECH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTECH.Tests.Data.Factories;

public static class TrackFactory
{
    public static Track GenerateTrack()
    {
        return new()
        {
            Name = "Track1",
            Country = Common.Enums.Country.AF,
            Length = 4d,
            AeroMod = 1d,
            ChassisMod = 1d,
            PowerMod = 1d,
            QualifyingMod = 1d,
            DefenseMod = 1d,
            State = Common.Enums.State.Active,
        };
    }
}
