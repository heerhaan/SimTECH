using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek;

public class RaceModel
{
    public Race Race { get; set; }

    public Climate Climate { get; set; }

    public Season Season { get; set; }

    public League League { get; set; }

    public List<Incident> Incidents { get; set; }

    public List<Tyre> Tyres { get; set; }
}
