using MudBlazor;

namespace SimTECH.Common.Enums;

[Flags]
public enum SituationOccurrence
{
    Unknown = 0,
    Raced = 1,
    Caution = 2,
    Halted = 3,
}

public static class SituationOccurrenceEnumHelper
{
    public static string SituationColour(this SituationOccurrence situation) => situation switch
    {
        SituationOccurrence.Raced => "Green",
        SituationOccurrence.Caution => "Yellow",
        SituationOccurrence.Halted => "Red",
        _ => "muted-background-primary"
    };

    public static Color GetIndicatorBackgroundColour(this SituationOccurrence situation) => situation switch
    {
        SituationOccurrence.Raced => Color.Dark,
        SituationOccurrence.Caution => Color.Warning,
        SituationOccurrence.Halted => Color.Error,
        _ => Color.Dark
    };

    public static Color GetIndicatorTextColour(this SituationOccurrence situation) => situation switch
    {
        SituationOccurrence.Raced => Color.Default,
        SituationOccurrence.Caution => Color.Dark,
        SituationOccurrence.Halted => Color.Default,
        _ => Color.Default
    };
}
