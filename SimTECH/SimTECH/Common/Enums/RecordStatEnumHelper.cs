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
}
