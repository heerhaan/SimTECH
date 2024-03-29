﻿using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Development;

public class DevelopedEntrant
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Country? Nationality { get; set; }
    public bool Mark { get; set; }

    public int? Optional { get; set; }

    public int Old { get; set; }
    public int Change { get; set; }
    public int New { get; set; }

    public int Min { get; set; }
    public int Max { get; set; }

    public int ValueMinimumLimit { get; set; }
    public int ValueMaximumLimit { get; set; } = int.MaxValue;
}
