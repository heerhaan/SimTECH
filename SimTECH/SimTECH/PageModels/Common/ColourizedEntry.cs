using SimTECH.Common.Constants;

namespace SimTECH.PageModels.Common;

public class ColourizedEntry
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Colour { get; set; } = Globals.DefaultColour;

    public string Accent { get; set; } = Globals.DefaultAccent;
}
