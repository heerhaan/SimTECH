namespace SimTECH.Common.Enums;

public enum RaceSession
{
    Concept = 0,
    Practice = 1,
    Qualifying = 2,
    Race = 4,
    Finished = 8,
}

public static class RaceSessionEnumHelper
{
    public static bool IsAccessible(RaceSession activeSession, RaceSession targetSession)
    {
        // if active is race(4), then it can access all equal and below this enum-integer
        return (int)activeSession >= (int)targetSession;
    }
}
