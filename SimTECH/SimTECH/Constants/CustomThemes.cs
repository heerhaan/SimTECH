using MudBlazor;

namespace SimTECH.Constants;

public static class CustomThemes
{
    public static Dictionary<string, MudTheme> SimThemes => new()
        {
            { "Galaxy", GalacticTheme },
            { "Galaxy Alt", GalacticAltTheme },
            { "Oceanic", OceanicTheme },
            { "Fiery", FieryTheme },
            { "Neon", NeonTheme },
            { "Basic", DefaultTheme },
        };

    public static readonly MudTheme DefaultTheme = new()
    {
        Palette = new PaletteLight()
        {
            Primary = CustomColours.BrightRed,
        },
        PaletteDark = new PaletteDark()
        {
            Primary = CustomColours.BrightRed,
            Secondary = CustomColours.LightBeige,
            Tertiary = Colors.LightBlue.Lighten2,

            Background = CustomColours.DeepDarkBlack,
            BackgroundGrey = Colors.Shades.Black,
            Surface = CustomColours.DarkBlack,
            DrawerBackground = CustomColours.DarkBlack,
            AppbarBackground = CustomColours.DarkBlack,
        },
        Typography = CustomTypos.CommonTypo,
    };

    public static readonly MudTheme GalacticTheme = new()
    {
        Palette = new PaletteDark()
        {
            Primary = Colors.DeepPurple.Lighten1,
            Secondary = Colors.Pink.Lighten1,
            Tertiary = Colors.Amber.Darken3,

            Background = CustomColours.DarkFadedPurple,
            BackgroundGrey = CustomColours.DeepDarkFadedPurple,
            Surface = CustomColours.DarkFadedPurple,
            DrawerBackground = CustomColours.DeepDarkFadedPurple,
            AppbarBackground = CustomColours.DeepDarkFadedPurple,
        },
        PaletteDark = new PaletteDark()
        {
            Primary = Colors.DeepPurple.Lighten1,
            Secondary = Colors.Pink.Lighten1,
            Tertiary = Colors.Amber.Darken3,

            Background = CustomColours.UltraDeepDarkPurple,
            BackgroundGrey = CustomColours.UltraDarkContrastPurple,
            Surface = CustomColours.DarkFadedPurple,
            DrawerBackground = CustomColours.DeepDarkFadedPurple,
            AppbarBackground = CustomColours.DeepDarkFadedPurple,
        },
        Typography = CustomTypos.CommonTypo,
    };

    public static readonly MudTheme GalacticAltTheme = new()
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
            Primary = Colors.DeepPurple.Lighten1,
            Secondary = Colors.Blue.Default,
            Tertiary = Colors.Red.Lighten2,

            Background = CustomColours.DeepDarkFadedPurple,
            BackgroundGrey = CustomColours.UltraDarkContrastPurple,
            Surface = CustomColours.DarkFadedPurple,
            DrawerBackground = CustomColours.UltraDeepDarkPurple,
            AppbarBackground = CustomColours.UltraDeepDarkPurple
        },
        Typography = CustomTypos.CommonTypo,
    };

    public static readonly MudTheme OceanicTheme = new()
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

            Background = CustomColours.DeepDarkFadedBlue,
            BackgroundGrey = CustomColours.UltraDarkFadedBlue,
            Surface = CustomColours.DarkFadedBlue,
            DrawerBackground = CustomColours.UltraDarkFadedBlue,
            AppbarBackground = CustomColours.UltraDarkFadedBlue
        },
        Typography = CustomTypos.CommonTypo,
    };

    public static readonly MudTheme FieryTheme = new()
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

            Background = CustomColours.DeepDarkFadedRed,
            BackgroundGrey = CustomColours.DeepDarkFadedRed,
            Surface = CustomColours.DarkFadedRed,
            DrawerBackground = CustomColours.UltraDarkFadedRed,
            AppbarBackground = CustomColours.UltraDarkFadedRed,
        },
        Typography = CustomTypos.CommonTypo,
    };

    public static readonly MudTheme NeonTheme = new()
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

            Background = CustomColours.DeepDarkFadedPink,
            BackgroundGrey = CustomColours.DeepDarkFadedPink,
            Surface = CustomColours.DarkFadedPink,
            DrawerBackground = CustomColours.UltraDarkFadedPink,
            AppbarBackground = CustomColours.UltraDarkFadedPink,
        },
        Typography = CustomTypos.CommonTypo,
    };
}
