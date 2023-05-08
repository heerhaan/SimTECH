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
                new Tyre { Name = "Soft", Colour = "#FFFFFF", Pace = 160, WearMax = -25, WearMin = -15, Length = 10, State = State.Active, },
                new Tyre { Name = "Medium", Colour = "#FFFFFF", Pace = 180, WearMax = -15, WearMin = -9, Length = 15, State = State.Active, },
                new Tyre { Name = "Hard", Colour = "#FFFFFF", Pace = 200, WearMax = -10, WearMin = -6, Length = 20, State = State.Active, },
                new Tyre { Name = "Grooved", Colour = "#FFFFFF", Pace = 60, WearMax = -3, WearMin = -1, Length = 60, State = State.Closed, },
            };

            Console.WriteLine(temp);
        }

        public static void SeedIncidents()
        {
            var temp = new List<Incident>
            {
                new Incident { Name = "Damage", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Collision", Category = CategoryIncident.Driver, Limit = 2, Punishment = 3, Odds = 1, State = State.Active, },
                new Incident { Name = "Accident", Category = CategoryIncident.Driver, Limit = 5, Punishment = 3, Odds = 1, State = State.Active, },
                new Incident { Name = "Puncture", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Engine", Category = CategoryIncident.Engine, Limit = 5, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Name = "Electrics", Category = CategoryIncident.Car, Limit = 3, Punishment = 5, Odds = 1, State = State.Active, },
                new Incident { Name = "Exhaust", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Gearbox", Category = CategoryIncident.Car, Limit = 4, Punishment = 5, Odds = 1, State = State.Active, },
                new Incident { Name = "Hydraulics", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Wheel", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Brakes", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Name = "Illegal", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Name = "Fuel", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Name = "Dangerous", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Name = "Death", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
            };

            Console.WriteLine(temp);
        }

        public static void SeedClimates()
        {
            var temp = new List<Climate>
            {
                new Climate { Terminology = "Sunny", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 1.1, Odds = 3, State = State.Active, },
                new Climate { Terminology = "Overcast", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.9, Odds = 3, State = State.Active, },
                new Climate { Terminology = "Rain", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.75, Odds = 1, State = State.Active, },
            };

            Console.WriteLine(temp);
        }

        public static void SeedTraits() { }

        public static void SeedTracks() { }

        public static void SeedManufacturers() { }
    }
}
