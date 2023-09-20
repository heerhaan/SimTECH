﻿namespace SimTECH.Data.Models
{
    public abstract class ScoreBase : ModelBase
    {
        public int Index { get; set; }
        public int[]? Scores { get; set; }
        public int Position { get; set; }
        public int AbsolutePosition { get; set; }

        public long RaceId { get; set; }
        public long ResultId { get; set; }
        public Result Result { get; set; }
    }
}
