using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class DriverService
    {
        private IList<Driver> _drivers = new List<Driver>
            {
                new Driver()
                {
                    Id = 1,
                    FirstName = "Max",
                    LastName = "Verstappen",
                    Abbreviation = "VER",
                    DateOfBirth = new DateTime(),
                    Country = Country.NL,
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
                    Country = Country.MO,
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
                    Country = Country.DE,
                    Biography = "Blush",
                    State = State.Archived
                },
            };

        public IList<Driver> GetTestNames()
        {
            return _drivers;
        }

        public void AddDriver(Driver driver)
        {
            _drivers.Add(driver);
        }
    }
}
