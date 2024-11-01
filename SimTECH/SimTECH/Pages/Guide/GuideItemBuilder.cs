using MudBlazor;
using SimTECH.Constants;
using SimTECH.PageModels;

namespace SimTECH.Pages.Guide;

public static class GuideItemBuilder
{
    public static List<GuideGroupItem> GetGroupedGuideItems =>
    [
        new GuideGroupItem
        {
            Title = "FAQ",
            Icon = Icons.Custom.FileFormats.FileExcel,
            ExpandedIcon = Icons.Custom.FileFormats.FileExcel,
            Type = typeof(Topics.Intro),
            Order = 0,
        },
        new GuideGroupItem
        {
            Title = "Quick start",
            Icon = Icons.Custom.FileFormats.FileExcel,
            ExpandedIcon = Icons.Custom.FileFormats.FileExcel,
            Type = typeof(Topics.QuickStartTopic),
            Order = 5,
        },
        new GuideGroupItem
        {
            Title = "Glossary",
            Icon = Icons.Material.Filled.MenuBook,
            ExpandedIcon = Icons.Material.Filled.MenuBook,
            Type = typeof(Topics.Glossary),
            Order = 99,
        },
        new GuideGroupItem
        {
            Title = "Racing information",
            Icon = Icons.Custom.Uncategorized.Folder,
            ExpandedIcon = Icons.Custom.Uncategorized.FolderOpen,
            Type = typeof(Topics.UnknownTopic),
            Order = 10,
            Children =
            [
                new("Practice", IconCollection.Polygon, typeof(Topics.RaceCategory.PracticeTopic)),
                new("Qualifying", IconCollection.Polygon, typeof(Topics.RaceCategory.QualifyingTopic)),
                new("Race", IconCollection.Polygon, typeof(Topics.RaceCategory.RaceTopic)),
                new("Distance", IconCollection.Polygon, typeof(Topics.RaceCategory.Distance)),
                new("Advance", IconCollection.Polygon, typeof(Topics.RaceCategory.Advance)),
                new("Reliability", IconCollection.Polygon, typeof(Topics.RaceCategory.Reliability)),
                new("Strategy", IconCollection.Polygon, typeof(Topics.RaceCategory.Strategy)),
                new("Overtaking", IconCollection.Polygon, typeof(Topics.RaceCategory.Overtaking)),
                new("Safety car", IconCollection.AlertTriangle, typeof(Topics.RaceCategory.SafetyMoment)),
                new("Modifiers", IconCollection.Polygon, typeof(Topics.RaceCategory.Modifiers)),
            ]
        },
    ];
}
