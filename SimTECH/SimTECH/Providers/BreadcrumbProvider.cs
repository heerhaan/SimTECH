using MudBlazor;

namespace SimTECH.Providers;

//Navigation provider for breadcrumbs (https://stackoverflow.com/questions/64473844/how-can-i-modify-the-layout-from-a-blazor-page)
public class BreadcrumbProvider
{
    public List<BreadcrumbItem> Breadcrumbs { get; set; } = [];

    public bool Visible { get; set; }

    public event Action OnChange;

    public void SetVisible(bool isVisible)
    {
        Visible = isVisible;
        NotifyStateChanged();
    }

    public void Reset()
    {
        Breadcrumbs.Clear();
        Visible = false;

        NotifyStateChanged();
    }

    public void SetBreadcrumbs(List<BreadcrumbItem> breadcrumbs)
    {
        Breadcrumbs = breadcrumbs;
        Visible = true;

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
