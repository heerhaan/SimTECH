using MudBlazor;

namespace SimTECH
{
    public static class Constants
    {
        public static string DefaultColour => "var(--mud-palette-background)";

        public static string DefaultAccent => "var(--mud-palette-text-primary)";

        public static DialogOptions DefaultChartDialogSettings => new() { MaxWidth = MaxWidth.ExtraLarge, CloseButton = true };
    }
}
