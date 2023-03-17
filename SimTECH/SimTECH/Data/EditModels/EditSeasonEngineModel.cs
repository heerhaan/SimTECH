using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditSeasonEngineModel
    {
        private readonly SeasonEngine _seasonEngine;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Power { get; set; }
        public int Reliability { get; set; }
        public bool Rebadged { get; set; }
        public long EngineId { get; set; }
        public long SeasonId { get; set; }

        public EditSeasonEngineModel() { _seasonEngine = new SeasonEngine(); }
        public EditSeasonEngineModel(SeasonEngine seasonEngine)
        {
            Id = seasonEngine.Id;
            Name = seasonEngine.Name;
            Power = seasonEngine.Power;
            Reliability = seasonEngine.Reliability;
            Rebadged = seasonEngine.Rebadged;
            EngineId = seasonEngine.EngineId;
            SeasonId = seasonEngine.SeasonId;

            _seasonEngine = seasonEngine;
        }

        public SeasonEngine Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Power = Power,
                Reliability = Reliability,
                Rebadged = Rebadged,
                EngineId = EngineId,
                SeasonId = SeasonId
            };

        public bool IsDirty => _seasonEngine != Record;
    }
}
