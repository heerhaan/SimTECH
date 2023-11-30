using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels;

public abstract class EditBase
{
    public long Id { get; set; }

    public bool IsNew => Id == default;
}

public abstract class EditStateBase : EditBase
{
    public State State { get; set; }
}
