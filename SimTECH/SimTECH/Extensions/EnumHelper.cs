namespace SimTECH.Extensions;

public static class EnumHelper
{
    public static IEnumerable<Enum> GetFlagged(this Enum e) => Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);

    //public static IEnumerable<TEnum> GetEnumFlagged<TEnum>(this Enum e) where TEnum : Enum
    //    => Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);

    public static TEnum[] GetEnumValues<TEnum>() => (TEnum[])Enum.GetValues(typeof(TEnum));
}