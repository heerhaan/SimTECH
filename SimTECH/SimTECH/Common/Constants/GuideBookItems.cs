using MudBlazor;
using SimTECH.PageModels;

namespace SimTECH.Common.Constants;

public static class GuideBookItems
{
    // TODO: work this out further, use this as a readable guidebook
    // also, change the naming and such of the popup help to something else (...help...)
    public static readonly List<GuideGroupItem> GuideCategoryItems =
    [
        new()
        {
            Link = "/guide/intro",
            Title = "FAQ",
            Icon = Icons.Custom.FileFormats.FileExcel,
            ExpandedIcon = Icons.Custom.FileFormats.FileExcel,
            Order = 0,
        },
        new()
        {
            Link = "/guide/racing",
            Title = "Racing",
            Icon = Icons.Custom.FileFormats.FileExcel,
            ExpandedIcon = Icons.Custom.FileFormats.FileExcel,
            Order = 0,
            Children =
            [
                new()
                {
                    Link = "/guide/racing/practice",
                    Title = "Practice",
                    Icon = Icons.Material.Outlined.Flag,
                    Order = 3,
                }
            ],
        },
    ];
}
