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

        public EditLeagueModel() { _league = new League(); }
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

    public class EditDevRangeModel
    {
        private readonly DevelopmentRange _range;

        public long Id { get; set; }
        public RangeType Type { get; set; }
        public int Comparer { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public long LeagueId { get; set; }

        public EditDevRangeModel() { _range = new DevelopmentRange(); }
        public EditDevRangeModel(DevelopmentRange range)
        {
            Id = range.Id;
            Type = range.Type;
            Comparer = range.Comparer;
            Minimum = range.Minimum;
            Maximum = range.Maximum;
            LeagueId = range.LeagueId;

            _range = range;
        }

        public DevelopmentRange Record =>
            new()
            {
                Id = this.Id,
                Type = this.Type,
                Comparer = this.Comparer,
                Minimum = this.Minimum,
                Maximum = this.Maximum,
                LeagueId = this.LeagueId
            };

        public bool IsDirty => _range != this.Record;
    }
}
