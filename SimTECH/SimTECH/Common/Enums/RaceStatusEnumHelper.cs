using SimTECH.Common.Constants;

namespace SimTECH.Common.Enums;

public enum RaceStatus
{
    None = 0,
    Racing, Dnf, Dsq, Dnq, Fatal,
}

public static class RaceStatusEnumHelper
{
    public static string ReadableStatus(this RaceStatus status) => status switch
    {
        RaceStatus.Dnf => "DNF",
        RaceStatus.Dsq => "DSQ",
        RaceStatus.Dnq => "DNQ",
        RaceStatus.Fatal => "INJURY",
        _ => "[Unknown]"
    };

    public static string StatusColour(this RaceStatus status) => status switch
    {
        RaceStatus.Dnf => "red",
        RaceStatus.Dsq => "black",
        RaceStatus.Dnq => "rebeccapurple",
        RaceStatus.Fatal => "white",
        _ => Globals.DefaultColour
    };

    public static string StatusStyles(this RaceStatus status) => status switch
    {
        RaceStatus.Dnf => "background-color: red",
        RaceStatus.Dsq => "background-color: black;color:white !important",
        RaceStatus.Dnq => "background-color: rebeccapurple",
        RaceStatus.Fatal => "background-color: white",
        _ => Globals.DefaultColour
    };
}
