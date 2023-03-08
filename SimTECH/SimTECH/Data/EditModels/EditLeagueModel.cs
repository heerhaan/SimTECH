using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditLeagueModel
    {
        private readonly League _league;

        public long Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public IList<EditDevRangeModel> DevelopmentRanges { get; set; }

        public EditLeagueModel()
        {
            Name = string.Empty;
            State = State.Concept;
            DevelopmentRanges = new List<EditDevRangeModel>();

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
                Id = this.Id,
                Name = this.Name ?? string.Empty,
                State = this.State,
                DevelopmentRanges = this.DevelopmentRanges
                    .Select(range => range.Record)
                    .ToList()
            };

        // Checks if the league record has any changes
        public bool IsDirty => _league != this.Record || this.DevelopmentRanges.Any(e => e.IsDirty);
    }
}
