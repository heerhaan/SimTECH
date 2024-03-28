namespace SimTECH.Extensions;

public static class EnumHelper
{
    public static IEnumerable<Enum> GetFlagged(this Enum e)
    {
        return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
    }

    public static TEnum[] GetEnumValues<TEnum>()
    {
        return (TEnum[])Enum.GetValues(typeof(TEnum));
    }
}