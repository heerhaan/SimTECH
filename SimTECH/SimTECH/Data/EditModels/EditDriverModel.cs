using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.EditModels
{
    public class EditDriverModel
    {
        private readonly Driver _driver;

        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Abbreviation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Country Country { get; set; }
        public string? Biography { get; set; }
        public State State { get; set; }

        public List<long> TraitIds { get; set; } = new();

        public EditDriverModel()
        {
            DateOfBirth = DateTime.Today;
            Country = EnumHelper.GetDefaultCountry();
            State = State.Concept;

            _driver = new Driver();
        }
        public EditDriverModel(Driver driver)
        {
            Id = driver.Id;
            FirstName = driver.FirstName;
            LastName = driver.LastName;
            Abbreviation = driver.Abbreviation;
            DateOfBirth = driver.DateOfBirth;
            Country = driver.Country;
            Biography = driver.Biography;
            State = driver.State;

            if (driver.DriverTraits != null)
                TraitIds = driver.DriverTraits.Select(e => e.TraitId).ToList();

            _driver = driver;
        }

        public Driver Record =>
            new()
            {
                Id = this.Id,
                FirstName = FirstName ?? string.Empty,
                LastName = LastName ?? string.Empty,
                Abbreviation = Abbreviation ?? string.Empty,
                DateOfBirth = DateOfBirth,
                Country = Country,
                Biography = Biography ?? string.Empty,
                State = State,

                DriverTraits = TraitIds.ConvertAll(e => new DriverTrait { TraitId = e, DriverId = Id })
            };

        public bool IsDirty => _driver != Record;
    }
}
