using System.ComponentModel;

namespace SimTECH.Extensions;

public static class AttributeHelper
{
    public static string GetDescription<T>(this T source)
    {
        if (source == null)
            return string.Empty;

        var sourceString = source.ToString() ?? string.Empty;

        var field = source.GetType().GetField(sourceString);

        if (field == null)
            return string.Empty;

        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes?.Length > 0)
            return attributes[0].Description;

        return sourceString;
    }
}
