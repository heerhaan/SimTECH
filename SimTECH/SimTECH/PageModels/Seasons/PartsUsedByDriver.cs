﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels.Seasons
{
    public class PartsUsedByDriver
    {
        public long SeasonDriverId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Team { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int TotalDnf { get; set; }
        public int TotalDsq { get; set; }

        public Dictionary<long, int> DriverIncidents { get; set; } = new();
        public List<GivenPenalty> GivenPenalties { get; set; } = new();
    }
}
