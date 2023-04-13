using SimTECH.Extensions;

namespace SimTECH.PageModels
{
    public abstract class WinnerModel
    {
        public string Name { get; set; }
        public Country Country { get; set; } = EnumHelper.GetDefaultCountry();
        public string Colour { get; set; } = "var(--mud-palette-background)";
        public string Accent { get; set; } = "var(--mud-palette-text-primary)";
    }

    public class DriverWinner : WinnerModel
    {
        public int Number { get; set; }
    }

    public class TeamWinner : WinnerModel { }
}
