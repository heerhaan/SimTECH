using SimTECH.Shared.GuideItems;

namespace SimTECH.Providers;

public static class GuideBookProvider
{
    private static readonly Dictionary<Type, Type> pageGuidanceDict = new()
    {
        { typeof(Pages.Index), typeof(Pages.Guide.Topics.Intro) },
        { typeof(Pages.Climates.Index), typeof(ClimateTopic) },
        { typeof(Pages.Leagues.ContractManager.Index), typeof(ContractTopic) },
        { typeof(Pages.Seasons.Overview.Development.Index), typeof(DevelopTopic) },
        { typeof(Pages.Entrants.Drivers.Index), typeof(DriverTopic) },
        { typeof(Pages.Entrants.Engines.Index), typeof(EngineTopic) },
        { typeof(Pages.Incidents.Index), typeof(IncidentTopic) },
        { typeof(Pages.Leagues.Index), typeof(LeagueTopic) },
        { typeof(Pages.Manufacturers.Index), typeof(ManufacturerTopic) },
        { typeof(Pages.Seasons.Index), typeof(SeasonTopic) },
        { typeof(Pages.Entrants.Teams.Index), typeof(TeamTopic) },
        { typeof(Pages.Entrants.Tracks.Index), typeof(TrackTopic) },
        { typeof(Pages.Traits.Index), typeof(TraitTopic) },
        { typeof(Pages.Tyres.Index), typeof(TyreTopic) },
    };

    public static Type GetHelpContentForPage(Type page)
    {
        if (pageGuidanceDict.TryGetValue(page, out Type? value))
            return value;

        return typeof(NotFoundTopic);
    }
}
