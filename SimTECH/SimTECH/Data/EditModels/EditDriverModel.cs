using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.EditModels
{
    public sealed class EditDriverModel
    {
        private readonly Driver _driver;

        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Abbreviation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Country Country { get; set; }
        public string? Biography { get; set; }
        public bool Alive { get; set; }
        public State State { get; set; }

        public IList<EditDriverTraitModel> DriverTraits { get; set; } = new List<EditDriverTraitModel>();

        public EditDriverModel(Driver? driver)
        {
            if (driver == null)
            {
                DateOfBirth = DateTime.Today;
                Country = EnumHelper.GetDefaultCountry();
                State = State.Active;
                Alive = true;

                _driver = new Driver();
            }
            else
            {
                Id = driver.Id;
                FirstName = driver.FirstName;
                LastName = driver.LastName;
                Abbreviation = driver.Abbreviation;
                DateOfBirth = driver.DateOfBirth;
                Country = driver.Country;
                Biography = driver.Biography;
                Alive = driver.Alive;
                State = driver.State;

                if (driver.DriverTraits != null)
                    DriverTraits = driver.DriverTraits.Select(e => new EditDriverTraitModel(e)).ToList();

                _driver = driver;
            }
        }

        public Driver Record =>
            new()
            {
                Id = Id,
                FirstName = FirstName ?? string.Empty,
                LastName = LastName ?? string.Empty,
                Abbreviation = Abbreviation ?? string.Empty,
                DateOfBirth = DateOfBirth,
                Country = Country,
                Biography = Biography ?? string.Empty,
                Alive = Alive,
                State = State,

                DriverTraits = DriverTraits.Select(e => e.Record).ToList()
            };

        public bool IsDirty => _driver != Record || DriverTraits.Any(e => e.IsDirty);
    }

    public sealed class EditDriverTraitModel
    {
        private readonly DriverTrait _driverTrait;

        public long DriverId { get; set; }
        public long TraitId { get; set; }

        public EditDriverTraitModel() { _driverTrait = new DriverTrait(); }
        public EditDriverTraitModel(DriverTrait driverTrait)
        {
            DriverId = driverTrait.DriverId;
            TraitId = driverTrait.TraitId;

            _driverTrait = driverTrait;
        }

        public DriverTrait Record =>
            new()
            {
                DriverId = DriverId,
                TraitId = TraitId
            };

        public bool IsDirty => _driverTrait != Record;
    }
}
