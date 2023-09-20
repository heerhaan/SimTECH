﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels.Racing
{
    public class RaweCeekModel
    {
        public Race Race { get; set; }
        public Climate Climate { get; set; }
        public Season Season { get; set; }

        public LeagueOptions LeagueOptions { get; set; }

        public List<RaweCeekDriver> RaweCeekDrivers { get; set; }

        public List<long> ConsumablePenalties { get; set; } = new();

        public double GapMarge { get; set; }
    }

    public static class ExtendRaweCeek
    {
        public static Dictionary<long, long[]> SeasonGroupedResultIds(this RaweCeekModel raweCeek)
        {
            return raweCeek.RaweCeekDrivers
                .GroupBy(e => e.ClassId)
                .ToDictionary(e => e.Key, e => e.Select(rd => rd.ResultId).ToArray());
        }
    }
}
