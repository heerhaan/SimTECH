using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.PageModels;
using Graph = ApexCharts;

namespace SimTECH.Constants;

public static class Globals
{
    public const string DefaultBackground = "var(--mud-palette-background)";
    public const string DefaultColour = "var(--mud-palette-primary)";
    public const string DefaultAccent = "var(--mud-palette-text-primary)";

    public const Country DefaultCountry = Country.FM;

    public static readonly string[] AllWeatherIcons =
        [
            Icons.Material.Filled.WbSunny,
            Icons.Material.Filled.Cloud,
            Icons.Material.Filled.Air,
            Icons.Material.Filled.WaterDrop,
            IconCollection.Sunset,
            IconCollection.CloudBolt,
            Icons.Material.Filled.Tsunami,
            IconCollection.Storm,
            IconCollection.Mist,
            IconCollection.Rainbow,
            IconCollection.Snowflake,
            IconCollection.Comet,
            Icons.Material.Filled.NightsStay,
        ];

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

    public static Graph.ApexChartOptions<DataPoint> CreateDefaultChartOptions => new()
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
}
