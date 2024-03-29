namespace SimTECH.Common.Enums;

public enum TeamRole
{
    None = 0,
    Main,
    Support
}

public static class TeamRoleEnumHelper
{
    public static readonly Dictionary<TeamRole, string> GetTeamRoleSelection = new()
        {
            { TeamRole.None, "None" },
            { TeamRole.Main, "Main" },
            { TeamRole.Support, "Support" },
        };
}
