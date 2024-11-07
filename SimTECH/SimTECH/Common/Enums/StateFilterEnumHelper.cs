namespace SimTECH.Common.Enums;

public enum StateFilter { All, Default, Active, Closed, Archived }

public static class StateFilterEnumHelper
{
    public static State[] StatesForFilter(this StateFilter filter) => filter switch
    {
        StateFilter.All => [State.Concept, State.Active, State.Advanced, State.Closed, State.Archived],
        StateFilter.Default => [State.Concept, State.Active, State.Advanced, State.Closed],
        StateFilter.Active => [State.Active, State.Advanced],
        StateFilter.Closed => [State.Closed],
        StateFilter.Archived => [State.Archived],
        _ => [State.Concept, State.Active, State.Advanced, State.Closed]
    };
}
