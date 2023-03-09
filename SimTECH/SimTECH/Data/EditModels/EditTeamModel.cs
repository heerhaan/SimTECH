using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditTeamModel
    {
        private readonly Team _team;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; }
        public string? Biography { get; set; }
        public State State { get; set; }

        public List<long> TraitIds { get; set; } = new();

        public EditTeamModel() { _team = new Team(); }
        public EditTeamModel(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Country = team.Country;
            Biography = team.Biography;
            State = team.State;

            _team = team;
        }

        public Team Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Country = Country,
                Biography = Biography,
                State = State,

                TeamTraits = TraitIds.ConvertAll(e => new TeamTrait { TraitId = e, TeamId = Id })
            };

        public bool IsDirty => _team != Record;
    }
}
