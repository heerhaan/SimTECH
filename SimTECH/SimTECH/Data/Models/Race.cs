﻿using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

public sealed class Race : ModelState
{
    public int Round { get; set; }
    public string Name { get; set; }
    public int RaceLength { get; set; }
    public DateTime? DateFinished { get; set; }

    public long SeasonId { get; set; }
    public Season Season { get; set; }
    public long TrackId { get; set; }
    public Track Track { get; set; }
    public long ClimateId { get; set; }
    public Climate Climate { get; set; }

    public IList<RaceOccurrence> Occurrences { get; set; }
    public IList<Result> Results { get; set; }
}

public static class ExtendRace
{
    public static Race? FindNext(this IEnumerable<Race> races)
    {
        if (!races.Any())
            return null;

        return races
            .OrderBy(e => e.Round)
            .FirstOrDefault(e => e.State == State.Concept || e.State == State.Active || e.State == State.Advanced);
    }
}
