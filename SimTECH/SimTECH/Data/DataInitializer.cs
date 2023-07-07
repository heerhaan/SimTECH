using Microsoft.EntityFrameworkCore;
using MudBlazor;
using SimTECH.Data.Models;

namespace SimTECH.Data
{
    public class DataInitializer
    {
        private readonly ModelBuilder builder;

        public DataInitializer(ModelBuilder builder)
        {
            this.builder = builder;
        }

        public void Seed()
        {
            SeedTyres();
            SeedIncidents();
            SeedClimates();
            SeedTracks();
            SeedTraits();
            SeedManufacturers();
        }

        private void SeedTracks()
        {
            builder.Entity<Track>().HasData(
                new Track { Id = 1, Name = "Spa-Francorchamps", Country = Country.BE, Length = 7.01, AeroMod = 0.55, ChassisMod = 1.1, PowerMod = 1.35, QualifyingMod = 0.7, DefenseMod = 0.9, State = State.Active },
                new Track { Id = 2, Name = "Circuit de Monaco", Country = Country.MO, Length = 3.05, AeroMod = 1.5, ChassisMod = 1.25, PowerMod = 0.50, QualifyingMod = 2.0, DefenseMod = 2.0, State = State.Active }
            );
        }

        private void SeedTraits()
        {
            builder.Entity<Trait>().HasData(
                new Trait { Id = 1, Name = "Rainmeister", Description = "Speed in moist", Type = Entrant.Driver, ForWetConditions = true, QualifyingPace = 3, RacePace = 5, DriverReliability = 3, },
                new Trait { Id = 2, Name = "Manufacturer", Description = "Owns the engine", Type = Entrant.Team, QualifyingPace = 1, RacePace = 1, EngineReliability = 2, },
                new Trait { Id = 3, Name = "Street Circuit", Description = "Street is a circuit", Type = Entrant.Track, DriverReliability = -2, CarReliability = -1, WearMax = 2, RngMin = -5, Defense = 10 }
            );
        }

        private void SeedTyres()
        {
            builder.Entity<Tyre>().HasData(
                    new Tyre { Id = 1, Name = "Soft", Colour = "#fa0536  ", Pace = 200, WearMin = 15, WearMax = 25, DistanceMin = 50, DistanceMax = 125, State = State.Active, },
                    new Tyre { Id = 2, Name = "Medium", Colour = "#f4ea26  ", Pace = 180, WearMin = 9, WearMax = 15, DistanceMin = 125, DistanceMax = 999, State = State.Active, },
                    new Tyre { Id = 3, Name = "Hard", Colour = "#dfdde9  ", Pace = 160, WearMin = 6, WearMax = 10, DistanceMin = 175, DistanceMax = 999, State = State.Active, },
                    new Tyre { Id = 4, Name = "Grooved", Colour = "#bded80  ", Pace = 100, WearMin = 1, WearMax = 3, DistanceMin = 100, DistanceMax = 999, State = State.Closed, },
                    new Tyre { Id = 5, Name = "Wet", Colour = "#3399ff  ", Pace = 50, WearMin = 0, WearMax = 1, DistanceMin = 50, DistanceMax = 999, ForWet = true, State = State.Active, }
            );
        }

        private void SeedManufacturers()
        {
            builder.Entity<Manufacturer>().HasData(
                new Manufacturer { Id = 1, Name = "Hankook", Colour = "#0b0b0d", Accent = "#e56103", Pace = 1, WearMin = 0, WearMax = 0, State = State.Active }
            );
        }

        private void SeedClimates()
        {
            builder.Entity<Climate>().HasData(
                new Climate { Id = 1, Terminology = "Sunny", Icon = Icons.Material.Filled.WbSunny, EngineMultiplier = 1.1, Odds = 3, State = State.Active, },
                new Climate { Id = 2, Terminology = "Overcast", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.9, Odds = 3, State = State.Active, },
                new Climate { Id = 3, Terminology = "Rain", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.75, Odds = 1, State = State.Active, }
            );
        }

        private void SeedIncidents()
        {
            builder.Entity<Incident>().HasData(
                new Incident { Id = 1, Name = "Damage", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 2, State = State.Active, },
                new Incident { Id = 2, Name = "Collision", Category = CategoryIncident.Driver, Limit = 2, Punishment = 3, Odds = 1, State = State.Active, },
                new Incident { Id = 3, Name = "Accident", Category = CategoryIncident.Driver, Limit = 5, Punishment = 3, Odds = 2, State = State.Active, },
                new Incident { Id = 4, Name = "Puncture", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Id = 5, Name = "Engine", Category = CategoryIncident.Engine, Limit = 5, Punishment = 10, Odds = 3, State = State.Active, },
                new Incident { Id = 6, Name = "Electrics", Category = CategoryIncident.Car, Limit = 3, Punishment = 5, Odds = 1, State = State.Active, },
                new Incident { Id = 7, Name = "Exhaust", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Id = 8, Name = "Gearbox", Category = CategoryIncident.Car, Limit = 4, Punishment = 5, Odds = 2, State = State.Active, },
                new Incident { Id = 9, Name = "Hydraulics", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Id = 10, Name = "Wheel", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Id = 11, Name = "Brakes", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, },
                new Incident { Id = 12, Name = "Illegal", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Id = 13, Name = "Fuel", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Id = 14, Name = "Dangerous", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, State = State.Active, },
                new Incident { Id = 15, Name = "Hospital", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 5, State = State.Active, },
                new Incident { Id = 16, Name = "Death", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 1, State = State.Active, }
            );
        }
    }
}
