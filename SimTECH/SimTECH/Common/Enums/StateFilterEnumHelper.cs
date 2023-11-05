namespace SimTECH.Common.Enums;

public enum StateFilter { All, Default, Active, Closed, Archived }

public static class StateFilterEnumHelper
{
    public static State[] StatesForFilter(this StateFilter filter) => filter switch
    {
        StateFilter.All => new State[] { State.Concept, State.Active, State.Advanced, State.Closed, State.Archived },
        StateFilter.Default => new State[] { State.Concept, State.Active, State.Advanced, State.Closed },
        StateFilter.Active => new State[] { State.Active, State.Advanced },
        StateFilter.Closed => new State[] { State.Closed },
        StateFilter.Archived => new State[] { State.Archived },
        _ => new State[] { State.Concept, State.Active, State.Advanced, State.Closed }
    };
}
