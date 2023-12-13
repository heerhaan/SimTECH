namespace SimTECH.Providers;

public static class GuideBookProvider
{
    private static readonly Dictionary<Type, Type> pageGuidanceDict = new()
    {
        { typeof(Pages.Index), typeof(Pages.Guide.Topics.Intro) }
    };

    public static Type GetHelpContentForPage(Type page)
    {
        if (pageGuidanceDict.TryGetValue(page, out Type? value))
            return value;

        return typeof(Pages.Guide.Topics.UnknownTopic);
    }
}
