using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class DriverService
    {
        private IDbContextFactory<SimTechDbContext> _dbFactory;

        public DriverService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        private readonly List<Driver> _drivers = new()
        {
            new Driver()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Verstappen",
                Abbreviation = "VER",
                DateOfBirth = DateTime.Today,
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
                DateOfBirth = DateTime.Today,
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
                DateOfBirth = DateTime.Today,
                Country = Country.DE,
                Biography = "Blush",
                State = State.Archived
            },
        };

        public List<Driver> GetTestData()
        {
            return _drivers;
        }

        public async ValueTask CreateDriver(Driver driver)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(driver);

            await context.SaveChangesAsync();

            _drivers.Add(driver);
        }
    }
}
