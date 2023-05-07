using MudBlazor;
using SimTECH.Data.Models;

namespace SimTECH.Data
{
    public static class DataInitializer
    {
        public static void SeedTyres()
        {
            var temp = new List<Tyre>
            {
                new Tyre { Name = "Soft", },
            };

            Console.WriteLine(temp);
        }

        public static void SeedIncidents()
        {
            var temp = new List<Incident>
            {
                new Incident { Name = "Damage", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Collision", Category = CategoryIncident.Driver, Limit = 2, Punishment = 3, Odds = 1},
                new Incident { Name = "Accident", Category = CategoryIncident.Driver, Limit = 5, Punishment = 3, Odds = 1},
                new Incident { Name = "Puncture", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Engine", Category = CategoryIncident.Engine, Limit = 5, Punishment = 10, Odds = 1},
                new Incident { Name = "Electrics", Category = CategoryIncident.Car, Limit = 3, Punishment = 5, Odds = 1},
                new Incident { Name = "Exhaust", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Gearbox", Category = CategoryIncident.Car, Limit = 4, Punishment = 5, Odds = 1},
                new Incident { Name = "Hydraulics", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Wheel", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Brakes", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1},
                new Incident { Name = "Illegal", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1},
                new Incident { Name = "Fuel", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1},
                new Incident { Name = "Dangerous", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1},
                new Incident { Name = "Death", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 1},
            };

            Console.WriteLine(temp);
        }

        public static void SeedClimates()
        {
            var temp = new List<Climate>
            {
                new Climate { Terminology = "Overcast", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 1.1, Odds = 1, },
            };

            Console.WriteLine(temp);
        }
    }
}
