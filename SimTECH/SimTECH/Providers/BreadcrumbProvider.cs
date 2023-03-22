using MudBlazor;

namespace SimTECH.Providers
{
    public class BreadcrumbProvider
    {
        public List<BreadcrumbItem> Breadcrumbs { get; set; } = new();
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
            Visible= false;

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
}
