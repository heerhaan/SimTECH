using MudBlazor;

namespace SimTECH.Constants;

public static class Generals
{
    public const string DefaultBackground = "var(--mud-palette-background)";
    public const string DefaultColour = "var(--mud-palette-primary)";
    public const string DefaultAccent = "var(--mud-palette-text-primary)";

    public const Country DefaultCountry = Country.FM;

    public static readonly string[] AllWeatherIcons = new string[]
        {
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
        };
}
