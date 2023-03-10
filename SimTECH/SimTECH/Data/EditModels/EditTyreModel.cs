using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditTyreModel
    {
        private readonly Tyre _tyre;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public State State { get; set; }

        public int Length { get; set; }
        public int Pace { get; set; }
        public int WearMax { get; set; }
        public int WearMin { get; set; }

        public EditTyreModel() { _tyre = new Tyre(); }
        public EditTyreModel(Tyre tyre)
        {
            Id = tyre.Id;
            Name = tyre.Name;
            Colour = tyre.Colour;
            State = tyre.State;
            Length = tyre.Length;
            Pace = tyre.Pace;
            WearMax = tyre.WearMax;
            WearMin = tyre.WearMin;

            _tyre = tyre;
        }

        public Tyre Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Colour = Colour,
                State = State,
                Length = Length,
                Pace = Pace,
                WearMax = WearMax,
                WearMin = WearMin
            };

        public bool IsDirty => _tyre != Record;
    }
}
