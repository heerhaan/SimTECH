using MudBlazor;

namespace SimTECH.Constants;

public static class CustomTypos
{
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
}
