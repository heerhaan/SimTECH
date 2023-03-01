﻿namespace SimTECH.Data.Models
{
    public class Penalty
    {
        public long Id { get; set; }
        public string Reason { get; set; } = default!;
        public int Punishment { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; } = default!;
        public long RaceId { get; set; }
        public Race Race { get; set; } = default!;
    }
}
