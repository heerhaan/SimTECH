using MudBlazor;

namespace SimTECH
{
    public static class Constants
    {
        public static string DefaultColour => "var(--mud-palette-background)";

        public static string DefaultAccent => "var(--mud-palette-text-primary)";

        public static DialogOptions DefaultChartDialogSettings => new() { MaxWidth = MaxWidth.ExtraLarge, CloseButton = true };

        #region themes
        public static MudTheme DefaultTheme => new();

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
        };
        #endregion
    }
}
