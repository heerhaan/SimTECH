namespace SimTECH.Data.Models
{
    /*
     * Damage, Collision, Accident, Puncture,
       Engine,
       Electrics, Exhaust, Clutch, Hydraulics, Wheel, Brakes,
       Illegal, Fuel, Dangerous,
       Death
     */
    public sealed class Incident : ModelState
    {
        public string Name { get; set; }
        //public string Icon { get; set; }
        public Entrant ForEntrant { get; set; }
        public RaceStatus ForStatus { get; set; }
        public int Limit { get; set; }
        public int Punishment { get; set; }
    }

    public static class ExtendIncident
    {
        public static bool HasLimit(this Incident incident) => incident.Limit > 0;
    }
}
