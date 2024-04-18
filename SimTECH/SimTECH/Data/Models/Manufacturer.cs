namespace SimTECH.Data.Models;

public sealed class Manufacturer : ModelState
{
    public string Name { get; set; }
    public string Colour { get; set; }
    public string Accent { get; set; }

    public int Pace { get; set; }
    public int WearMin { get; set; }
    public int WearMax { get; set; }
}
