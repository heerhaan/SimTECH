﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaceWeekModel
    {
        public Race Race { get; set; }

        public List<RaceWeekDriver> RaceWeekDrivers { get; set; }

        public List<Strategy>? AvailableStrategies { get; set; }

        public List<Trait>? TrackTraits { get; set; }
    }

    public class RaceWeekDriver
    {
        // Properties related to the driver of this result
        public long SeasonDriverId { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Country { get; set; }

        // Properties related to the team of this result
        public long SeasonTeamId { get; set; }
        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        // Properties related to the result
        public long ResultId { get; set; }
        public int Grid { get; set; }
        public int Position { get; set; }
        public RaceStatus Status { get; set; }
        public long StrategyId { get; set; }
        public Strategy? Strategy { get; set; }
    }
}