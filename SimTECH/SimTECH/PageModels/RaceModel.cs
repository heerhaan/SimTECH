﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public abstract class SessionBase
    {
        public long RaceId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Climate { get; set; }
        public string ClimateIcon { get; set; }
        public bool IsWet { get; set; }
        public bool IsFinished { get; set; }

        public int AmountRuns { get; set; }//Can be used for both runs in qualy and stints in race
    }

    public class RaceModel : SessionBase
    {
        public long ClimateId { get; set; }
        public long TrackId { get; set; }
        public double TrackLength { get; set; }
        public int Round { get; set; }
        public int RaceLength { get; set; }

        public List<RaceDriver> RaceDrivers { get; set; }

        public Season Season { get; set; }//pick values intead of whole object?
        public LeagueOptions LeagueOptions { get; set; }

        public int QualifyingBonus(int grid) => (RaceDrivers.Count * Season.GridBonus) - ((grid - 1) * Season.GridBonus);
    }

    public class QualifyingModel : SessionBase
    {
        public int MaximumRaceDrivers { get; set; }
        public int QualyRng { get; set; }
        public int QualyAmountQ2 { get; set; }
        public int QualyAmountQ3 { get; set; }
        public QualyFormat QualifyingFormat { get; set; }

        public List<long> PenaltiesToConsume { get; set; } = new();
        public List<QualifyingDriver> QualifyingDrivers { get; set; } = new();
    }

    public class PracticeModel : SessionBase
    {
        public int PracticeRng { get; set; }

        public List<PracticeDriver> PracticeDrivers { get; set; }
    }
}
