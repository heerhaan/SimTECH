namespace SimTECH.Common.Enums;

[Flags]
public enum SituationOccurrence
{
    Unknown = 0,
    Raced = 1,
    Caution = 2,
}

public static class SituationOccurrenceEnumHelper
{
    public static string SituationColour(this SituationOccurrence situation) => situation switch
    {
        SituationOccurrence.Raced => "Green",
        SituationOccurrence.Caution => "Yellow",
        _ => "muted-background-primary"
    };
}
