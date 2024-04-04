using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels
{
    public class EditEngineModel
    {
        private readonly Engine _engine;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Accent { get; set; } = string.Empty;
        public bool Mark { get; set; }
        public State State { get; set; }

        public EditEngineModel(Engine? engine)
        {
            if (engine == null)
            {
                Name = "NewEngine";
                Colour = "#ffffffff";
                Accent = "#000000ff";

                _engine = new();
            }
            else
            {
                Id = engine.Id;
                Name = engine.Name;
                Colour = engine.Colour;
                Accent = engine.Accent;
                State = engine.State;
                Mark = engine.Mark;

                _engine = engine;
            }
        }

        public Engine Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Colour = Colour,
                Accent = Accent,
                Mark = Mark,
                State = State.HasFlag(State.Concept) ? State.Active : State,
            };

        public bool IsDirty => _engine != Record;
    }
}
