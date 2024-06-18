using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Tests.Data.Factories;

public static class IncidentFactory
{
    public static List<Incident> GenerateTestIncidentList =>
        [
            new() { Name = "Damage", Category = IncidentCategory.Driver, Limit = 0, Punishment = 0, Odds = 2, Penalized = false, State = State.Active, },
            new() { Name = "Collision", Category = IncidentCategory.Driver, Limit = 2, Punishment = 3, Odds = 1, Penalized = true, State = State.Active, },
            new() { Name = "Accident", Category = IncidentCategory.Driver, Limit = 5, Punishment = 3, Odds = 2, Penalized = true, State = State.Active, },
            new() { Name = "Puncture", Category = IncidentCategory.Driver, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
            new() { Name = "Engine", Category = IncidentCategory.Engine, Limit = 5, Punishment = 10, Odds = 3, Penalized = true, State = State.Active, },
            new() { Name = "Electrics", Category = IncidentCategory.Car, Limit = 3, Punishment = 5, Odds = 1, Penalized = true, State = State.Active, },
            new() { Name = "Exhaust", Category = IncidentCategory.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
            new() { Name = "Gearbox", Category = IncidentCategory.Car, Limit = 4, Punishment = 5, Odds = 2, Penalized = true, State = State.Active, },
            new() { Name = "Hydraulics", Category = IncidentCategory.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
            new() { Name = "Wheel", Category = IncidentCategory.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
            new() { Name = "Brakes", Category = IncidentCategory.Car, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, },
            new() { Name = "Illegal", Category = IncidentCategory.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
            new() { Name = "Fuel", Category = IncidentCategory.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
            new() { Name = "Dangerous", Category = IncidentCategory.Disqualified, Limit = 0, Punishment = 10, Odds = 1, Penalized = true, State = State.Active, },
            new() { Name = "Hospital", Category = IncidentCategory.Lethal, Limit = 0, Punishment = 0, Odds = 5, Penalized = false, State = State.Active, },
            new() { Name = "Death", Category = IncidentCategory.Lethal, Limit = 0, Punishment = 0, Odds = 1, Penalized = false, State = State.Active, }
        ];
}
