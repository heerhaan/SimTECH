using SimTECH.Shared.HelpBlocks;

namespace SimTECH.Providers;

public static class HelpBlockProvider
{
    private static readonly Dictionary<Type, Type> pageGuideDictionary = new()
    {
        { typeof(Pages.Index), typeof(WorkInProgressHelp) },
        { typeof(Pages.Climates.Index), typeof(ClimateHelp) },
        { typeof(Pages.Leagues.ContractManager.Index), typeof(ContractHelp) },
        { typeof(Pages.Seasons.Overview.Development.Index), typeof(DevelopHelp) },
        { typeof(Pages.Drivers.Index), typeof(DriverHelp) },
        { typeof(Pages.Engines.Index), typeof(EngineHelp) },
        { typeof(Pages.Incidents.Index), typeof(IncidentHelp) },
        { typeof(Pages.Leagues.Index), typeof(LeagueHelp) },
        { typeof(Pages.Manufacturers.Index), typeof(ManufacturerHelp) },
        { typeof(Pages.Seasons.Index), typeof(SeasonHelp) },
        { typeof(Pages.RaceWeek.Index), typeof(RaceweekHelp) },
        { typeof(Pages.Teams.Index), typeof(TeamHelp) },
        { typeof(Pages.Tracks.Index), typeof(TrackHelp) },
        { typeof(Pages.Traits.Index), typeof(TraitHelp) },
        { typeof(Pages.Tyres.Index), typeof(TyreHelp) },
    };

    public static Type GetHelpContentForPage(Type page)
    {
        if (pageGuideDictionary.TryGetValue(page, out Type? value))
            return value;

        return typeof(NotFoundHelp);
    }
}
