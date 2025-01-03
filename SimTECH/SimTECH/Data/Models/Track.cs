﻿using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

public sealed class Track : ModelState
{
    public string Name { get; set; }

    public Country Country { get; set; }

    public double Length { get; set; }

    public double AeroMod { get; set; }

    public double ChassisMod { get; set; }

    //public double GearMod { get; set; }

    // NOTE: Should this rather be used on the engine?
    public double PowerMod { get; set; }

    public double QualifyingMod { get; set; }

    public double DefenseMod { get; set; }

    public IList<Race> Races { get; set; }

    public IList<TrackTrait> TrackTraits { get; set; }
}

public static class ExtendTrack
{
    public static string ShortTextMods(this Track track)
    {
        var retString = "";

        retString += track.AeroMod.ToString("F2");
        retString += "/";
        retString += track.ChassisMod.ToString("F2");
        retString += "/";
        retString += track.PowerMod.ToString("F2");

        return retString;
    }
}
