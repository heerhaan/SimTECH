﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaweCeekModel
    {
        public Race Race { get; set; }
        public Climate Climate { get; set; }
        public Season Season { get; set; }

        public LeagueOptions LeagueOptions { get; set; }

        public List<RaweCeekDriver> RaweCeekDrivers { get; set; }

        public List<long> ConsumablePenalties { get; set; } = new();
    }
}
