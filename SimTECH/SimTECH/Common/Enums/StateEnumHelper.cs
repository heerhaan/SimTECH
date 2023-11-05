namespace SimTECH.Common.Enums;

public enum State
{
    Concept = 0,
    Active,
    Closed,
    Archived,
    Advanced, // Advanced is a specific state, logically fits between active and closed
}

public static class StateEnumHelper
{
}
