using MudBlazor;
using SimTECH.Constants;

namespace SimTECH.PageModels;

public class GuideItem
{
    public GuideItem() { }
    public GuideItem(string title, string icon, Type type, int order = 5)
    {
        Title = title;
        Icon = icon;
        Type = type;
        Order = order;
    }

    public string Title { get; set; } = string.Empty;

    public string Icon { get; set; } = IconCollection.Pyramid;

    public Type Type { get; set; }

    public int Order { get; set; }
}

public class GuideGroupItem : GuideItem
{
    public bool Expanded { get; set; } = false;

    public string ExpandedIcon { get; set; } = Icons.Custom.Uncategorized.FolderOpen;

    public List<GuideItem> Children { get; set; } = [];

    public bool HasChildren => Children.Count != 0;
}
