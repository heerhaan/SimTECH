using MudBlazor;

namespace SimTECH.Common.Enums;

public enum Gender { All, Male, Female, Other }

public static class GenderEnumHelper
{
    public static string GenderIcon(this Gender gender) => gender switch
    {
        Gender.All => Icons.Material.Filled.AllInclusive,
        Gender.Male => Icons.Material.Filled.Male,
        Gender.Female => Icons.Material.Filled.Female,
        Gender.Other => Icons.Custom.Uncategorized.Baguette,
        _ => Icons.Material.Filled.QuestionMark
    };
}
