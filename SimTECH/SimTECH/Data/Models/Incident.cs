using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    /*
     * Damage, Collision, Accident, Puncture,
       Engine,
       Electrics, Exhaust, Clutch, Hydraulics, Wheel, Brakes,
       Illegal, Fuel, Dangerous,
       Death
     */

    public enum CategoryIncident
    {
        Driver, Car, Engine, Disqualified, Lethal
    }

    public sealed class Incident : ModelState
    {
        public string Name { get; set; }
        //public string Icon { get; set; }
        public CategoryIncident Category { get; set; }
        public int Limit { get; set; }
        public int Punishment { get; set; }
        public int Odds { get; set; }
    }

    public static class ExtendIncident
    {
        public static bool HasLimit(this Incident incident) => incident.Limit > 0;

        public static Incident TakeRandomIncident(this List<Incident> incidents)
        {
            var weightedList = new List<Incident>();

            foreach (var incident in incidents)
            {
                for (int i = 0; i < incident.Odds; i++)
                {
                    weightedList.Add(incident);
                }
            }

            return weightedList.TakeRandomItem();
        }

        public static double DoubledPunishment(this Incident incident) => Convert.ToDouble(incident.Punishment) + 0.2;
    }
}
