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
                new Tyre { Name = "Soft", Colour = "#FFFFFF", Pace = 200, WearMin = 15, WearMax = 25, Length = 10, State = State.Active, },
                new Tyre { Name = "Medium", Colour = "#FFFFFF", Pace = 180, WearMin = 9, WearMax = 15, Length = 15, State = State.Active, },
                new Tyre { Name = "Hard", Colour = "#FFFFFF", Pace = 160, WearMin = 6, WearMax = 10, Length = 20, State = State.Active, },
                new Tyre { Name = "Grooved", Colour = "#FFFFFF", Pace = 60, WearMin = 1, WearMax = 3, Length = 60, State = State.Closed, },
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

        public static void SeedTraits()
        {
            var temp = new List<Trait>
            {
                new Trait { Name = "Rainmeister", Description = "Speed in moist", Type = Entrant.Driver, ForWetConditions = true, QualifyingPace = 3, RacePace = 5, DriverReliability = 3, },
                new Trait { Name = "Manufacturer", Description = "Owns the engine", Type = Entrant.Team, QualifyingPace = 1, RacePace = 1, EngineReliability = 2, },
                new Trait { Name = "Street Circuit", Description = "Street is a circuit", Type = Entrant.Track, DriverReliability = -2, CarReliability = -1, WearMax = 2, RngMin = -5, Defense = 10 },
            };

            Console.WriteLine(temp);
        }

        public static void SeedTracks()
        {
            var temp = new List<Track>
            {
                new Track { Name = "Spa-Francorchamps", Country = Country.BE, Length = 7.01, AeroMod = 0.55, ChassisMod = 1.1, PowerMod = 1.35, QualifyingMod = 0.7, DefenseMod = 0.9, State = State.Active },
            };

            Console.WriteLine(temp);
        }

        public static void SeedManufacturers()
        {
            var temp = new List<Manufacturer>
            {
                new Manufacturer { Name = "Hankook", Colour = "#0b0b0d", Accent = "#e56103", Pace = 1, WearMin = 0, WearMax = 0, State = State.Active },
            };

            Console.WriteLine(temp);
        }
    }
}
