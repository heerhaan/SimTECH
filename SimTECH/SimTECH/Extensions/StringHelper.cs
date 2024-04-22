namespace SimTECH.Extensions;

public static class StringHelper
{
    public static string? TrimEmptyToNull(this string? text)
    {
        var trimmed = text?.Trim() ?? string.Empty;

        if (trimmed.Length == 0)
            return null;

        return trimmed;
    }
}
