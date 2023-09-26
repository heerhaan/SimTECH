using MudBlazor;
using SimTECH.PageModels;
using Graph = ApexCharts;

namespace SimTECH.Constants;

public static class DefaultOptions
{
    public static readonly DialogOptions StatisticDialogDefaultOptions = new()
    {
        MaxWidth = MaxWidth.Large,
        NoHeader = true,
        CloseOnEscapeKey = true,
        DisableBackdropClick = false,
    };

    public static readonly DialogOptions StatisticDialogDefaultOptionsXl = new()
    {
        MaxWidth = MaxWidth.ExtraLarge,
        NoHeader = true,
        CloseOnEscapeKey = true,
        DisableBackdropClick = false,
    };

    #region charts
    public static readonly Graph.ApexChartOptions<DataPoint> ChartOptionsDefault = new()
    {
        Chart = new()
        {
            Animations = new() { Enabled = false },
            Background = "#080808",
            Toolbar = new() { Show = false },
        },
        Theme = new()
        {
            Mode = Graph.Mode.Dark,
        },
    };
    #endregion
}
