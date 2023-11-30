using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

// Relatively commonly shared base models

// Can be used to make generic models sharing properties, think State
// ModelBase<T> if I can see why one would add the generic <T> for an implementation (generics? interfaces?)
public abstract class ModelBase
{
    public long Id { get; set; }
}

public abstract class ModelState : ModelBase
{
    public State State { get; set; }
}

public abstract class ContractBase : ModelBase
{
    public int Duration { get; set; }
    // TODO: Field {Cost} are related to finance functionality, which isn't implemented yet
    //public int Cost { get; set; }

    public long LeagueId { get; set; }
    public League League { get; set; }
}

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
