using SimTECH.PageModels.Stats;
using SimTECH.Pages.Stats.Dialogs;

namespace SimTECH.Pages.Stats;

public static class StatisticTopicsBuilder
{
    public static List<StatTopic> GetStatisticTopics =>
    [
        new StatTopic(
            "Compare drivers",
            "Compare statistics between multiple drivers",
            typeof(DriverCompareStat)),
        new StatTopic(
            "Records drivers",
            "Leaderboards of drivers with the most of something",
            typeof(DriverRecord))
    ];
}
