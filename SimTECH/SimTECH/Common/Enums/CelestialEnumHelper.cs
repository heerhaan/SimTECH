namespace SimTECH.Common.Enums;

public enum Celestial
{
    Mercury,
    Venus,
    Earth,
    Mars,
    Jupiter,
    Saturn,
    Uranus,
    Neptune,
    Pluto,
    Forbidden,
}

public static class CelestialEnumHelper
{
    public static string GetDescription(this Celestial item) => item switch
    {
        Celestial.Mercury => "First planet. Alike to a bunch of tangled-up snakes, slow to move and impossible to hurt.",
        Celestial.Venus => "Second planet.",
        Celestial.Earth => "Third planet.",
        Celestial.Mars => "Fourth planet.",
        Celestial.Jupiter => "Fifth planet. ",
        Celestial.Saturn => "Sixth planet. ",
        Celestial.Uranus => "Seventh planet. ",
        Celestial.Neptune => "Eighth planet. ",
        Celestial.Pluto => "Ninth planet(?). ",
        Celestial.Forbidden => "[ERROR 403: FORBIDDEN]",
        _ => ""
    };
}
