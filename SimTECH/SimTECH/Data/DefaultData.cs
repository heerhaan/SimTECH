using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data;

public static class DefaultData
{
    public static List<Track> DefaultTracks => new()
    {
        new Track { Name = "Spa-Francorchamps", Country = Country.BE, Length = 7.01, AeroMod = 0.55, ChassisMod = 1.1, PowerMod = 1.35, QualifyingMod = 0.7, DefenseMod = 0.9, State = State.Active },
        new Track { Name = "Circuit de Monaco", Country = Country.MO, Length = 3.05, AeroMod = 1.5, ChassisMod = 1.25, PowerMod = 0.50, QualifyingMod = 2.0, DefenseMod = 2.0, State = State.Active },
        new Track { Name = "Autodromo de Interlagos", Country = Country.BR, Length = 4.31, AeroMod = 0.85, ChassisMod = 1.05, PowerMod = 1.1, QualifyingMod = 0.9, DefenseMod = 0.8, State = State.Active },
        new Track { Name = "TT Assen", Country = Country.NL, Length = 4.55, AeroMod = 0.95, ChassisMod = 1.1, PowerMod = 0.95, QualifyingMod = 1.1, DefenseMod = 1.3, State = State.Active },
        new Track { Name = "Fuji Speedway", Country = Country.JP, Length = 5.99, AeroMod = 1.05, ChassisMod = 0.90, PowerMod = 1.05, QualifyingMod = 0.9, DefenseMod = 0.66, State = State.Active },
        new Track { Name = "Österreichring", Country = Country.AT, Length = 4.33, AeroMod = 1.05, ChassisMod = 0.95, PowerMod = 1.1, QualifyingMod = 0.8, DefenseMod = 1.0, State = State.Active },
        new Track { Name = "Autodromo Nazionale di Monza", Country = Country.IT, Length = 5.79, AeroMod = 0.8, ChassisMod = 0.75, PowerMod = 1.25, QualifyingMod = 1.2, DefenseMod = 1.2, State = State.Active },
        new Track { Name = "Sepang", Country = Country.FM, Length = 5.54, AeroMod = 1.1, ChassisMod = 0.9, PowerMod = 1.1, QualifyingMod = 1, DefenseMod = 0.9, State = State.Active }
    };

    public static List<Trait> DefaultTraits => new()
    {
        new Trait { Name = "Rainmeister", Description = "Faster when it's wet", Type = Entrant.Driver, ForWetConditions = true, QualifyingPace = 3, RacePace = 5, DriverReliability = 3, },
        new Trait { Name = "Manufacturer", Description = "Owns the engine", Type = Entrant.Team, QualifyingPace = 1, RacePace = 1, EngineReliability = 2, },
        new Trait { Name = "Street Circuit", Description = "Street is a circuit", Type = Entrant.Track, DriverReliability = -2, CarReliability = -1, WearMax = 2, RngMin = -5, Defense = 10 }
    };

    public static List<Tyre> DefaultTyres => new()
    {
        new Tyre { Name = "Soft", Colour = "#fa0536ff", Pace = 200, PitWhenBelow = 20, WearMin = 15, WearMax = 25, DistanceMin = 50, DistanceMax = 125, State = State.Active, },
        new Tyre { Name = "Medium", Colour = "#f4ea26ff", Pace = 180, PitWhenBelow = 15, WearMin = 9, WearMax = 15, DistanceMin = 125, DistanceMax = 999, State = State.Active, },
        new Tyre { Name = "Hard", Colour = "#dfdde9ff", Pace = 160, PitWhenBelow = 10, WearMin = 6, WearMax = 10, DistanceMin = 175, DistanceMax = 999, State = State.Active, },
        new Tyre { Name = "Grooved", Colour = "#bded80ff", Pace = 100, WearMin = 1, WearMax = 3, DistanceMin = 100, DistanceMax = 999, State = State.Closed, },
        new Tyre { Name = "Wet", Colour = "#3399ffff", Pace = 50, WearMin = 0, WearMax = 1, DistanceMin = 50, DistanceMax = 999, ForWet = true, State = State.Active, }
    };

    public static List<Manufacturer> DefaultManufacturers => new()
    {
        new Manufacturer { Name = "Hankook", Colour = "#0b0b0d", Accent = "#e56103", Pace = 0, WearMin = 0, WearMax = 0, State = State.Active }
    };

    public static List<Climate> DefaultClimates => new()
    {
        new Climate { Terminology = "Sunny", Icon = Icons.Material.Filled.WbSunny, EngineMultiplier = 1.1, Odds = 3, State = State.Active, },
        new Climate { Terminology = "Overcast", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.9, Odds = 3, State = State.Active, },
        new Climate { Terminology = "Rain", Icon = Icons.Material.Filled.Cloud, EngineMultiplier = 0.75, Odds = 1, State = State.Active, }
    };

    public static List<Incident> DefaultIncidents => new()
    {
        new Incident { Name = "Damage", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 2, Penalized = false, State = State.Active, },
        new Incident { Name = "Collision", Category = CategoryIncident.Driver, Limit = 2, Punishment = 3, Odds = 1, Penalized = true, State = State.Active, },
        new Incident { Name = "Accident", Category = CategoryIncident.Driver, Limit = 5, Punishment = 3, Odds = 2, Penalized = true, State = State.Active, },
        new Incident { Name = "Puncture", Category = CategoryIncident.Driver, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },

        new Incident { Name = "Engine", Category = CategoryIncident.Engine, Limit = 5, Punishment = 10, Odds = 3, Penalized = true, State = State.Active, },
        
        new Incident { Name = "Electrics", Category = CategoryIncident.Car, Limit = 3, Punishment = 5, Odds = 1, Penalized = true, State = State.Active, },
        new Incident { Name = "Exhaust", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
        new Incident { Name = "Gearbox", Category = CategoryIncident.Car, Limit = 4, Punishment = 5, Odds = 2, Penalized = true, State = State.Active, },
        new Incident { Name = "Hydraulics", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
        new Incident { Name = "Wheel", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
        new Incident { Name = "Brakes", Category = CategoryIncident.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
        
        new Incident { Name = "Illegal", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
        new Incident { Name = "Fuel", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
        new Incident { Name = "Dangerous", Category = CategoryIncident.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
        
        new Incident { Name = "Hospital", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 5, Penalized = false, State = State.Active, },
        new Incident { Name = "Death", Category = CategoryIncident.Lethal, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, }
    };
}
