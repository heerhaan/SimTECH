using SimTECH.Common.Enums;
using SimTECH.Common.Constants;

namespace SimTECH.PageModels;

public abstract class CompactEntrant
{
    public string Name { get; set; }
    public Country Country { get; set; } = Globals.DefaultCountry;
    public string Colour { get; set; } = "var(--mud-palette-background)";
    public string Accent { get; set; } = "var(--mud-palette-text-primary)";
}

public class CompactDriver : CompactEntrant
{
    public int Number { get; set; }
}

public class CompactTeam : CompactEntrant { }
