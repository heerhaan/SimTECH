using MudBlazor;

namespace SimTECH
{
    public static class Constants
    {
        public static string DefaultColour => "var(--mud-palette-background)";

        public static string DefaultAccent => "var(--mud-palette-text-primary)";

        public static Typography CommonTypo = new Typography()
        {
            Default = new Default()
            {
                FontFamily = new[] { "NovaRegular" }
            },
            Button = new Button()
            {
                FontFamily = new[] { "NovaBlack" }
            },
            H1 = new H1()
            {
                FontFamily = new[] { "F1Regular" }
            },
            H2 = new H2()
            {
                FontFamily = new[] { "F1Regular" }
            },
            H3 = new H3()
            {
                FontFamily = new[] { "F1Regular" }
            },
            H4 = new H4()
            {
                FontFamily = new[] { "F1Regular" }
            },
            H5 = new H5()
            {
                FontFamily = new[] { "F1Regular" }
            },
            H6 = new H6()
            {
                FontFamily = new[] { "F1Regular" }
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
                FontFamily = new[] { "F1Regular" }
            },
        };

        public static DialogOptions DefaultChartDialogSettings => new() { MaxWidth = MaxWidth.ExtraLarge, CloseButton = true };

        #region themes
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

                AppbarBackground = Colors.DeepPurple.Lighten1,
            },
            Typography = CommonTypo,
        };
        #endregion
    }
}
