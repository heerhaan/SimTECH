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
        public long EngineId { get; set; }
        public long SeasonId { get; set; }

        public IList<EditSeasonTeamModel>? SeasonTeams { get; set; }

        public EditSeasonEngineModel(SeasonEngine? seasonEngine)
        {
            if (seasonEngine == null)
            {
                Reliability = 1000;
                Power = 100;

                _seasonEngine = new();
            }
            else
            {
                Id = seasonEngine.Id;
                Name = seasonEngine.Name;
                Power = seasonEngine.Power;
                Reliability = seasonEngine.Reliability;
                EngineId = seasonEngine.EngineId;
                SeasonId = seasonEngine.SeasonId;

                if (seasonEngine.SeasonTeams?.Any() == true)
                    SeasonTeams = seasonEngine.SeasonTeams.Select(e => new EditSeasonTeamModel(e)).ToList();

                _seasonEngine = seasonEngine;
            }
        }

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;

            if (SeasonTeams?.Any() == true)
            {
                foreach (var team in SeasonTeams)
                    team.ResetIdentifierFields();
            }
        }

        public void SetSeasonIdForAll(long seasonId)
        {
            SeasonId = seasonId;

            if (SeasonTeams?.Any() == true)
            {
                foreach (var team in SeasonTeams)
                    team.SetSeasonIdForAll(seasonId);
            }
        }

        public SeasonEngine Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Power = Power,
                Reliability = Reliability,
                EngineId = EngineId,
                SeasonId = SeasonId,
                SeasonTeams = SeasonTeams?.Select(e => e.Record).ToList(),
            };

        public bool IsDirty => _seasonEngine != Record;
    }
}
