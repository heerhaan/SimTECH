using MudBlazor;
using MudBlazor.Utilities;

namespace SimTECH
{
    public static class Constants
    {
        public static string DefaultBackground => "var(--mud-palette-background)";
        public static string DefaultColour => "var(--mud-palette-background)";
        public static string DefaultAccent => "var(--mud-palette-text-primary)";

        public static Country DefaultCountry => Country.FM;

        public static DialogOptions DefaultChartDialogSettings => new()
        {
            FullScreen = true,
            CloseButton = true,
        };

        public static string[] AllWeatherIcons => new string[]
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

        #region themes
        private static readonly Typography CommonTypo = new()
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

        private static readonly MudColor VeryDeepDarkPurple = new("#0d0612");
        private static readonly MudColor DarkFadedPurple = new("#19141f"); // Background
        private static readonly MudColor DeepDarkFadedPurple = new("#100d14"); // Gray
        private static readonly MudColor AltDarkFadedPurple = new("#16121c"); // Surface
        private static readonly MudColor DarkFadedBlue = new("#0c1e27");
        private static readonly MudColor DeepDarkFadedBlue = new("#09131a");
        private static readonly MudColor LiteDarkFadedBlue = new("#122d3b");

        public static MudTheme DefaultTheme => new()
        {
            Typography = CommonTypo,
        };

        public static MudTheme SynthTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Default,
                //Triadic
                Secondary = Colors.Pink.Default,
                Tertiary = Colors.Orange.Accent3,

                AppbarBackground = Colors.DeepPurple.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.DeepPurple.Lighten1,
                // Triadic
                Secondary = Colors.Pink.Darken2,
                Tertiary = Colors.Amber.Darken2,

                Background = DarkFadedPurple,
                BackgroundGrey = DeepDarkFadedPurple,
                Surface = AltDarkFadedPurple,
                DrawerBackground = DeepDarkFadedPurple,
                AppbarBackground = DeepDarkFadedPurple
            },
            Typography = CommonTypo,
        };

        public static MudTheme SynthAltTheme => new()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Default,
                // Analogous
                Secondary = Colors.Indigo.Default,
                Tertiary = Colors.Purple.Accent3,

                AppbarBackground = Colors.DeepPurple.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.DeepPurple.Lighten1,
                // Analogous
                Secondary = Colors.Indigo.Darken1,
                Tertiary = Colors.Purple.Darken1,

                Background = DarkFadedPurple,
                BackgroundGrey = DeepDarkFadedPurple,
                Surface = AltDarkFadedPurple,
                DrawerBackground = DeepDarkFadedPurple,
                AppbarBackground = DeepDarkFadedPurple
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

                Background = DarkFadedBlue,
                BackgroundGrey = DeepDarkFadedBlue,
                Surface = LiteDarkFadedBlue,
                DrawerBackground = DeepDarkFadedBlue,
                AppbarBackground = DeepDarkFadedBlue
            },
            Typography = CommonTypo,
        };
        #endregion
    }
}
