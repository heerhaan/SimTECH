﻿namespace SimTECH.Data.Models;

public class TrackTrait
{
    public long TraitId { get; set; }
    public Trait Trait { get; set; }
    public long TrackId { get; set; }
    public Track Track { get; set; }
}
