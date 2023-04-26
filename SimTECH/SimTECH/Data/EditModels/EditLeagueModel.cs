using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditLeagueModel
    {
        private readonly League _league;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RaceLength { get; set; }
        public bool UsePenalty { get; set; }
        public bool EnableFatality { get; set; }
        public State State { get; set; }
        public IList<EditRangeModel> DevelopmentRanges { get; set; } = new List<EditRangeModel>();

        public EditLeagueModel() { _league = new League(); }
        public EditLeagueModel(League league)
        {
            Id = league.Id;
            Name = league.Name;
            RaceLength = league.RaceLength;
            UsePenalty = league.Options.HasFlag(LeagueOptions.EnablePenalty);
            UsePenalty = league.Options.HasFlag(LeagueOptions.EnableFatality);
            State = league.State;
            DevelopmentRanges = league.DevelopmentRanges?
                .Select(range => new EditRangeModel(range))
                .ToList()
                ?? new List<EditRangeModel>();

            _league = league;
        }

        public League Record =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty,
                RaceLength = RaceLength,
                Options = DetermineOptions(),
                State = State,
                DevelopmentRanges = DevelopmentRanges
                    .Select(range => range.Record)
                    .ToList()
            };

        // Checks if the league record has any changes
        public bool IsDirty => _league != Record || DevelopmentRanges.Any(e => e.IsDirty);

        private LeagueOptions DetermineOptions()
        {
            var baseOptions = LeagueOptions.None;

            if (UsePenalty)
                baseOptions |= LeagueOptions.EnablePenalty;
            if (EnableFatality)
                baseOptions |= LeagueOptions.EnableFatality;

            return baseOptions;
        }
    }

    public class EditRangeModel
    {
        private readonly DevelopmentRange _range;

        public long Id { get; set; }
        public RangeType Type { get; set; }
        public int Comparer { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public long LeagueId { get; set; }

        public EditRangeModel() { _range = new DevelopmentRange(); }
        public EditRangeModel(DevelopmentRange range)
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
                Id = Id,
                Type = Type,
                Comparer = Comparer,
                Minimum = Minimum,
                Maximum = Maximum,
                LeagueId = LeagueId
            };

        public bool IsDirty => _range != Record;
    }
}
