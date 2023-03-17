using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditTeamModel
    {
        private readonly Team _team;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; }
        public string Biography { get; set; } = string.Empty;
        public State State { get; set; }

        public IList<EditTeamTraitModel> TeamTraits { get; set; } = new List<EditTeamTraitModel>();

        public EditTeamModel() { _team = new Team(); }
        public EditTeamModel(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Country = team.Country;
            Biography = team.Biography;
            State = team.State;

            if (team.TeamTraits != null)
                TeamTraits = team.TeamTraits.Select(e => new EditTeamTraitModel(e)).ToList();

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

                TeamTraits = TeamTraits.Select(e => e.Record).ToList()
            };

        public bool IsDirty => _team != Record || TeamTraits.Any(e => e.IsDirty);
    }

    public class EditTeamTraitModel
    {
        private readonly TeamTrait _teamTrait;

        public long TeamId { get; set; }
        public long TraitId { get; set; }

        public EditTeamTraitModel() { _teamTrait = new TeamTrait(); }
        public EditTeamTraitModel(TeamTrait teamTrait)
        {
            TeamId = teamTrait.TeamId;
            TraitId = teamTrait.TraitId;

            _teamTrait = teamTrait;
        }

        public TeamTrait Record =>
            new()
            {
                TeamId = TeamId,
                TraitId = TraitId
            };

        public bool IsDirty => _teamTrait != Record;
    }
}
