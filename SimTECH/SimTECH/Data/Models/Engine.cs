﻿namespace SimTECH.Data.Models
{
    public class Engine
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }

        public IList<SeasonEngine> SeasonEngines { get; set; } = default!;
    }
}