using MudBlazor;
using MudBlazor.Utilities;

namespace SimTECH
{
    public static class Constants
    {
        public static string DefaultColour => "var(--mud-palette-background)";
        public static string DefaultAccent => "var(--mud-palette-text-primary)";

        public static DialogOptions DefaultChartDialogSettings => new() { MaxWidth = MaxWidth.ExtraLarge, CloseButton = true };

        #region themes
        private static readonly Typography CommonTypo = new()
        {
            Default = new Default()
            {
                FontFamily = new[] { "NovaRegular" }
            },
            Button = new Button()
            {
                FontFamily = new[] { "NovaBold" }
            },
            H1 = new H1()
            {
                FontFamily = new[] { "F1Bold" }
            },
            H2 = new H2()
            {
                FontFamily = new[] { "F1Bold" }
            },
            H3 = new H3()
            {
                FontFamily = new[] { "F1Bold" }
            },
            H4 = new H4()
            {
                FontFamily = new[] { "F1Bold" }
            },
            H5 = new H5()
            {
                FontFamily = new[] { "F1Bold" }
            },
            H6 = new H6()
            {
                FontFamily = new[] { "F1Bold" }
            },
            Caption = new Caption()
            {
                FontFamily = new[] { "F1Bold" }
            },
            Overline = new Overline()
            {
                FontFamily = new[] { "F1Wide" }
            },
            Subtitle1 = new Subtitle1()
            {
                FontFamily = new[] { "F1Regular" }
            },
            Subtitle2 = new Subtitle2()
            {
                FontFamily = new[] { "NovaBlack" }
            },
        };

        private static readonly MudColor VeryDeepDarkPurple = new("#0d0612");
        private static readonly MudColor DarkFadedPurple = new("#19141f"); // Background
        private static readonly MudColor DeepDarkFadedPurple = new("#100d14"); // Gray
        private static readonly MudColor AltDarkFadedPurple = new("#16121c"); // Surface

        public static MudTheme DefaultTheme => new()
        {
            Typography = CommonTypo,
        };

        public static MudTheme SynthTheme => new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.DeepPurple.Default,
                // Analogous
                //Secondary = Colors.Indigo.Default,
                //Tertiary = Colors.Purple.Accent3,

                //Triadic
                Secondary = Colors.Pink.Default,
                Tertiary = Colors.Orange.Accent3,

                AppbarBackground = Colors.DeepPurple.Default,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.DeepPurple.Lighten1,
                // Analogous
                //Secondary = Colors.Indigo.Darken1,
                //Tertiary = Colors.Purple.Darken1,

                // Triadic
                Secondary = Colors.Pink.Darken2,
                Tertiary = Colors.Yellow.Darken2,

                Background = DarkFadedPurple,
                BackgroundGrey = DeepDarkFadedPurple,
                Surface = AltDarkFadedPurple,
                DrawerBackground = DeepDarkFadedPurple,
                AppbarBackground = DeepDarkFadedPurple
            },
            Typography = CommonTypo,
        };
        #endregion
    }
}
