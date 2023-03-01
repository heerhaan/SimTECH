using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class DriverService
    {
        public IEnumerable<Driver> GetTestNames()
        {
            return new List<Driver>
            {
                new Driver()
                {
                    Id = 1,
                    FirstName = "Max",
                    LastName = "Verstappen",
                    Abbreviation = "VER",
                    DateOfBirth = new DateTime(),
                    Country = "NL",
                    Biography = "Super",
                    State = State.Active
                },
                new Driver()
                {
                    Id = 2,
                    FirstName = "Sharl",
                    LastName = "Eclair",
                    Abbreviation = "LEG",
                    DateOfBirth = new DateTime(),
                    Country = "MO",
                    Biography = "Stoopid",
                    State = State.Active
                },
                new Driver()
                {
                    Id = 3,
                    FirstName = "Seb",
                    LastName = "Vet",
                    Abbreviation = "VET",
                    DateOfBirth = new DateTime(),
                    Country = "DE",
                    Biography = "Blush",
                    State = State.Archived
                },
            };
        }
    }
}
