using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditLeagueModel
    {
        private readonly League _league;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public State State { get; set; }
        public IList<EditDevRangeModel> DevelopmentRanges { get; set; } = new List<EditDevRangeModel>();

        public EditLeagueModel()
        {
            _league = new League();
        }
        public EditLeagueModel(League league)
        {
            Id = league.Id;
            Name = league.Name;
            State = league.State;
            DevelopmentRanges = league.DevelopmentRanges?
                .Select(range => new EditDevRangeModel(range))
                .ToList()
                ?? new List<EditDevRangeModel>();

            _league = league;
        }

        public League Record =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty,
                State = State,
                DevelopmentRanges = DevelopmentRanges
                    .Select(range => range.Record)
                    .ToList()
            };

        // Checks if the league record has any changes
        public bool IsDirty => _league != Record || DevelopmentRanges.Any(e => e.IsDirty);
    }
}
