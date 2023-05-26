using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditEngineModel
    {
        private readonly Engine _engine;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public State State { get; set; }

        public EditEngineModel(Engine? engine)
        {
            if (engine == null)
            {
                _engine = new();
            }
            else
            {
                Id = engine.Id;
                Name = engine.Name;
                State = engine.State;

                _engine = engine;
            }
        }

        public Engine Record =>
            new()
            {
                Id = Id,
                Name = Name,
                State = State.HasFlag(State.Concept) ? State.Active : State,
            };

        public bool IsDirty => _engine != Record;
    }
}
