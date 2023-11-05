namespace SimTECH.Common.Enums;

[Flags]
public enum LeagueOptions
{
    None = 0,
    EnablePenalty = 1,
    EnableFatality = 2,
    AllowContracting = 4,
    PersonalNumbers = 8,
}

public static class LeagueOptionsEnumHelper
{
}
