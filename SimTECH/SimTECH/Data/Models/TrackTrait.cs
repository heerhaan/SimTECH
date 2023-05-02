﻿namespace SimTECH.Data.Models
{
    public class TrackTrait
    {
        public long TraitId { get; set; }
        public Trait Trait { get; set; } = default!;
        public long TrackId { get; set; }
        public Track Track { get; set; } = default!;
    }
}
