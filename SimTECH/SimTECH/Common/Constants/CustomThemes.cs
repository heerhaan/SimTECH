using MudBlazor;

namespace SimTECH.Constants;

public static class CustomThemes
{
    public static Dictionary<string, MudTheme> SimThemes => new()
    {
        { "Galaxy", GalacticTheme },
        { "Galaxy Alt", GalacticAltTheme },
        { "Oceanic", OceanicTheme },
        { "Cyberpunk", CyberTheme },
        { "Fiery", FieryTheme },
        { "Neon", NeonTheme },
        { "Basic", DefaultTheme },
    };

    public static readonly Typography CommonTypo = new()
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
        Typography = CommonTypo,
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
        Typography = CommonTypo,
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
        Typography = CommonTypo,
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
        Typography = CommonTypo,
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
            BackgroundGrey = CustomColours.UltraDarkFadedRed,
            Surface = CustomColours.DarkFadedRed,
            DrawerBackground = CustomColours.UltraDarkFadedRed,
            AppbarBackground = CustomColours.UltraDarkFadedRed,
        },
        Typography = CommonTypo,
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
        Typography = CommonTypo,
    };

    public static readonly MudTheme CyberTheme = new()
    {
        Palette = new PaletteLight()
        {
            Primary = CustomColours.ActiveDarkRed,
            Secondary = CustomColours.MildCyan,
            Tertiary = CustomColours.StreetGreen,

            DrawerBackground = CustomColours.MaroonRed,
            AppbarBackground = CustomColours.MaroonRed,
        },
        PaletteDark = new PaletteDark()
        {
            Primary = CustomColours.FadedRed,
            Secondary = CustomColours.BrightCyan,
            Tertiary = CustomColours.StreetGreen,

            Background = CustomColours.DeepDarkFadedRed,
            BackgroundGrey = CustomColours.UltraDarkFadedBlue,
            Surface = CustomColours.DarkFadedRedAlt,
            DrawerBackground = CustomColours.UltraDeepDarkFadedBlue,
            AppbarBackground = CustomColours.UltraDeepDarkFadedBlue,
        },
        Typography = CommonTypo,
    };
}
