using SimTECH.PageModels.Stats;
using SimTECH.Pages.Stats.Dialogs;

namespace SimTECH.Pages.Stats;

public static class StatsDataBuilder
{
    public static List<StatTopic> GetStatisticTopics =>
    [
        new StatTopic(
            "Records drivers",
            "Leaderboards of drivers with the most of something",
            typeof(DriverRecord)),
        new StatTopic(
            "Compare drivers",
            "Compare statistics between multiple drivers",
            typeof(DriverCompareStat)),
        new StatTopic(
            "Compare drivers (all items)",
            "Comparison w/ all topics at once",
            typeof(ObsoleteDriverComparison))
    ];

    // Prefer an enum over string list
    public static List<string> GetResultComparisonItems =>
        [
            "Entries",
            "Starts",
            "Wins",
            "Poles",
            "Fastest laps",
            "Retirements",
        ];
}
