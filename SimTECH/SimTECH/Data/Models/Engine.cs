namespace SimTECH.Data.Models;

public sealed class Engine : ModelState
{
    public string Name { get; set; }
    public string Colour { get; set; }
    public string Accent { get; set; }
    public bool Mark { get; set; }

    public IList<SeasonEngine> SeasonEngines { get; set; }
}
