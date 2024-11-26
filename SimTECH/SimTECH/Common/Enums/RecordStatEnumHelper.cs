namespace SimTECH.Common.Enums;

public enum RecordStat
{
    None = 0,
    Entry,
    Start,
    Win,
    Pole,
    Retired,
    FastestLap,
    Overtakes,
    Defended,
}

public static class RecordStatEnumHelper
{
    public static readonly RecordStat[] DriverRecordStats =
        [
            RecordStat.Entry,
            RecordStat.Start,
            RecordStat.Win,
            RecordStat.Pole,
            RecordStat.Retired,
            RecordStat.Overtakes,
            RecordStat.Defended,
        ];

    public static string GetReadableText(this RecordStat item)
    {
        return item switch
        {
            RecordStat.Entry => "Entries",
            RecordStat.Start => "Starts",
            RecordStat.Win => "Wins",
            RecordStat.Pole => "Pole positions",
            RecordStat.Retired => "Retirements",
            RecordStat.Overtakes => "Average overtakes per race",
            RecordStat.Defended => "Average overtakes prevented per race",
            _ => throw new ArgumentOutOfRangeException("Unrecognized RecordStat-enum"),
        };
    }
}
