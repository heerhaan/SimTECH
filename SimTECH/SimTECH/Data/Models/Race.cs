﻿namespace SimTECH.Data.Models
{
    public class Race
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }
    }
}
