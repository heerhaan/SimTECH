namespace SimTECH.Common.Enums;

public enum QualyFormat
{
    TripleEliminate,
    OneSession,
}

public static class QualyFormatEnumHelper
{
    public static int SessionCount(this QualyFormat format) => format switch
    {
        QualyFormat.OneSession => 1,
        QualyFormat.TripleEliminate => 3,
        _ => 0
    };
}
