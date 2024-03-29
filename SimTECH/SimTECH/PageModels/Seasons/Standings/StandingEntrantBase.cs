﻿using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Standings;

public abstract class StandingEntrantBase
{
    public long Id { get; set; }
    public int Position { get; set; }
    public string Name { get; set; }
    public Country Nationality { get; set; }
    public int Points { get; set; }
    public double HiddenPoints { get; set; }
    public bool Mark { get; set; }

    public string Colour { get; set; }
    public string Accent { get; set; }

    public List<StandingResultCell> ResultCells { get; set; }

    public double Average
    {
        get
        {
            var finishedResults = ResultCells.Where(e => e.Status == RaceStatus.Racing);

            if (finishedResults.Any())
                return Math.Round(finishedResults.Select(e => e.Position).Average(), 1);

            return 0;
        }
    }
}
