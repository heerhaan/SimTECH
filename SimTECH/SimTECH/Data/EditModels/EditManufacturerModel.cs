using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditManufacturerModel
    {
        private readonly Manufacturer _manufacturer;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Accent { get; set; } = string.Empty;
        public State State { get; set; }

        public int Pace { get; set; }
        public int WearMax { get; set; }
        public int WearMin { get; set; }

        public EditManufacturerModel() { _manufacturer = new Manufacturer(); }
        public EditManufacturerModel(Manufacturer manufacturer)
        {
            Id = manufacturer.Id;
            Name = manufacturer.Name;
            Colour = manufacturer.Colour;
            Accent = manufacturer.Accent;
            State = manufacturer.State;
            Pace = manufacturer.Pace;
            WearMax = manufacturer.WearMax;
            WearMin = manufacturer.WearMin;

            _manufacturer = manufacturer;
        }

        public Manufacturer Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Colour = Colour,
                Accent = Accent,
                State = State.HasFlag(State.Concept) ? State.Active : State,
                Pace = Pace,
                WearMax = WearMax,
                WearMin = WearMin
            };

        public bool IsDirty => _manufacturer != Record;
    }
}
