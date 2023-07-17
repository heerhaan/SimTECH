using MudBlazor;
using MudBlazor.Utilities;
using SimTECH.PageModels;
using Chart = ApexCharts;

namespace SimTECH
{
    public static class Constants
    {
        public const string DefaultBackground = "var(--mud-palette-background)";
        public const string DefaultColour = "var(--mud-palette-primary)";
        public const string DefaultAccent = "var(--mud-palette-text-primary)";
        public const string PrimaryColourDefault = "var(--mud-palette-primary)";

        public const Country DefaultCountry = Country.FM;

        public static readonly string[] AllWeatherIcons = new string[]
        {
            Icons.Material.Filled.WbSunny,
            Icons.Material.Filled.Cloud,
            Icons.Material.Filled.WaterDrop,
            Icons.Material.Filled.Tsunami,
            Icons.Material.Filled.Air,
            Icons.Material.Filled.NightsStay,
            IconCollection.Storm,
            IconCollection.Snowflake,
            IconCollection.CloudBolt,
        };

        #region charts
        public static readonly Chart.ApexChartOptions<DataPoint> ChartOptionsDefault = new()
        {
            Chart = new()
            {
                Animations = new() { Enabled = false },
                Background = "#525252BF",
                Toolbar = new() { Show = false },
            },
            Theme = new()
            {
                Mode = Chart.Mode.Dark,
            },
        };
        #endregion

        #region themes

        #region custom colours
        private static readonly MudColor UltraDarkFadedRed = new("#100a0a");
        private static readonly MudColor DeepDarkFadedRed = new("#110808");
        private static readonly MudColor DarkFadedRed = new("#250e0e");

        private static readonly MudColor UltraDarkFadedPink = new("#130D0F");
        private static readonly MudColor DeepDarkFadedPink = new("#170B11");
        private static readonly MudColor DarkFadedPink = new("#200A12");
        private static readonly MudColor LiteFadedPink = new("#280b16");

        private static readonly MudColor UltraDeepDarkPurple = new("#0d0a10");
        private static readonly MudColor DeepDarkFadedPurple = new("#100d14");
        private static readonly MudColor DarkFadedPurple = new("#19141f");
        private static readonly MudColor LiteDarkFadedPurple = new("#140a24");

        private static readonly MudColor UltraDarkFadedBlue = new("#060f14");
        private static readonly MudColor DeepDarkFadedBlue = new("#09131a");
        private static readonly MudColor DarkFadedBlue = new("#0c1e27");
        private static readonly MudColor LiteDarkFadedBlue = new("#122d3b");
        #endregion

        public static Dictionary<string, MudTheme> SimThemes => new()
        {
            { "Galaxy", GalacticTheme },
            { "Galaxy (Alt)", GalacticAltTheme },
            { "Oceanic", OceanicTheme },
            { "Fiery", FieryTheme },
            { "Neon", NeonTheme },
            { "Basic", DefaultTheme },
        };

        public static MudTheme DefaultTheme => new()
        {
            Typography = CommonTypo,
        };

        public static MudTheme GalacticTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Default,
                Secondary = Colors.Pink.Default,
                Tertiary = Colors.Orange.Accent3,

                AppbarBackground = Colors.DeepPurple.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.DeepPurple.Lighten1,
                Secondary = Colors.Pink.Lighten1,
                Tertiary = Colors.Amber.Darken3,

                Background = DarkFadedPurple,
                BackgroundGrey = DeepDarkFadedPurple,
                Surface = DarkFadedPurple,
                DrawerBackground = DeepDarkFadedPurple,
                AppbarBackground = DeepDarkFadedPurple,
            },
            Typography = CommonTypo,
        };

        public static MudTheme GalacticAltTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Default,
                Secondary = Colors.Indigo.Default,
                Tertiary = Colors.Purple.Accent3,

                AppbarBackground = Colors.DeepPurple.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.DeepPurple.Default,
                Secondary = Colors.Indigo.Darken1,
                Tertiary = Colors.Pink.Lighten1,

                Background = DeepDarkFadedPurple,
                BackgroundGrey = UltraDeepDarkPurple,
                Surface = DarkFadedPurple,
                DrawerBackground = UltraDeepDarkPurple,
                AppbarBackground = UltraDeepDarkPurple
            },
            Typography = CommonTypo,
        };

        public static MudTheme OceanicTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.Teal.Default,
                Secondary = Colors.Green.Default,
                Tertiary = Colors.LightBlue.Accent3,

                AppbarBackground = Colors.Teal.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Teal.Lighten1,
                Secondary = Colors.Green.Lighten2,
                Tertiary = Colors.LightBlue.Darken3,

                Background = DeepDarkFadedBlue,
                BackgroundGrey = UltraDarkFadedBlue,
                Surface = DarkFadedBlue,
                DrawerBackground = UltraDarkFadedBlue,
                AppbarBackground = UltraDarkFadedBlue
            },
            Typography = CommonTypo,
        };

        public static MudTheme FieryTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.Red.Default,
                Secondary = Colors.DeepOrange.Default,
                Tertiary = Colors.Orange.Default,

                AppbarBackground = Colors.Red.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Red.Default,
                Secondary = Colors.Orange.Darken1,
                Tertiary = Colors.Yellow.Lighten2,
                
                TertiaryContrastText = Colors.Shades.Black,

                Background = DeepDarkFadedRed,
                BackgroundGrey = DeepDarkFadedRed,
                Surface = DarkFadedRed,
                DrawerBackground = UltraDarkFadedRed,
                AppbarBackground = UltraDarkFadedRed,
            },
            Typography = CommonTypo,
        };

        public static MudTheme NeonTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.Cyan.Darken2,
                Secondary = Colors.Pink.Accent2,
                Tertiary = Colors.DeepPurple.Default,

                AppbarBackground = Colors.Cyan.Darken2,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Cyan.Default,
                Secondary = Colors.Pink.Accent2,
                Tertiary = Colors.DeepPurple.Lighten2,

                Background = DeepDarkFadedPink,
                BackgroundGrey = DeepDarkFadedPink,
                Surface = DarkFadedPink,
                DrawerBackground = UltraDarkFadedPink,
                AppbarBackground = UltraDarkFadedPink,
            },
            Typography = CommonTypo,
        };

        private static Typography CommonTypo => new()
        {
            Default = new Default()
            {
                FontFamily = new[] { "NovaRegular" }
            },
            H1 = new H1()
            {
                FontFamily = new[] { "F1Regular" },
                FontSize = "4rem",
            },
            H2 = new H2()
            {
                FontFamily = new[] { "F1Bold" },
                FontSize = "3rem",
            },
            H3 = new H3()
            {
                FontFamily = new[] { "F1Regular" },
                FontSize = "2rem",
            },
            H4 = new H4()
            {
                FontFamily = new[] { "F1Bold" },
                FontSize = "1rem",
            },
            H5 = new H5()
            {
                FontFamily = new[] { "F1Regular" },
                FontSize = "1rem",
            },
            H6 = new H6()
            {
                FontFamily = new[] { "NovaBlack" },
                FontSize = "1rem",
            },
            Subtitle1 = new Subtitle1()
            {
                FontFamily = new[] { "NovaBold" },
            },
            Subtitle2 = new Subtitle2()
            {
                FontFamily = new[] { "NovaBold" },
            },
            Button = new Button()
            {
                FontFamily = new[] { "NovaBold" },
            },
            Caption = new Caption()
            {
                FontFamily = new[] { "F1Bold" },
            },
            Overline = new Overline()
            {
                FontFamily = new[] { "F1Wide" },
                LetterSpacing = ".01em"
            },
        };
        #endregion
    }
}
