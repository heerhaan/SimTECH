﻿namespace SimTECH.PageModels.RaceWeek;

public class ScoredPoints
{
    public long SeasonDriverId { get; set; }
    public long SeasonTeamId { get; set; }

    public int Points { get; set; }
    public double HiddenPoints { get; set; }
}
