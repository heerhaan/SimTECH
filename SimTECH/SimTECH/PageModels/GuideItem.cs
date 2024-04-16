using MudBlazor;
using SimTECH.Constants;

namespace SimTECH.PageModels;

public class GuideItem
{
    public string Link { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Icon { get; set; } = IconCollection.Pyramid;

    public int Order { get; set; }
}

public class GuideGroupItem : GuideItem
{
    public bool Expanded { get; set; } = false;

    public string ExpandedIcon { get; set; } = Icons.Custom.Uncategorized.FolderOpen;

    public List<GuideItem> Children { get; set; } = [];

    public bool HasChildren => Children.Count != 0;
}
