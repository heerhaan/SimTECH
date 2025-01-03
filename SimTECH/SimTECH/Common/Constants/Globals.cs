﻿using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.PageModels.Stats;
using Graph = ApexCharts;

namespace SimTECH.Common.Constants;

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

    public static DialogOptions StatDialogOptions => new()
    {
        MaxWidth = MaxWidth.Large,
        NoHeader = true,
        CloseOnEscapeKey = true,
        BackdropClick = true,
    };

    public static DialogOptions StatDialogOptionsLarge => new()
    {
        MaxWidth = MaxWidth.ExtraLarge,
        NoHeader = true,
        CloseOnEscapeKey = true,
        BackdropClick = true,
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
