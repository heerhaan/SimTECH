using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class ManufacturerService
    {
        private readonly List<Manufacturer> _manufacturers = new()
        {
            new Manufacturer()
            {
                Id = 1,
                Name = "Hankook",
                Colour = "#000000ff",
                Accent = "#f5e20eff",
                State = State.Active,

                Pace = 10,
                WearMax = -4,
                WearMin = -2
            }
        };

        public List<Manufacturer> GetTestData()
        {
            return _manufacturers;
        }

        public void CreateManufacturer(Manufacturer manufacturer)
        {
            _manufacturers.Add(manufacturer);
        }
    }
}
