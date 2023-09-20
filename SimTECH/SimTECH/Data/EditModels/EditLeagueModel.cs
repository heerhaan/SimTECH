using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditLeagueModel
    {
        private readonly League _league;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RaceLength { get; set; } = 300;
        public bool UsePenalty { get; set; }
        public bool EnableFatality { get; set; }
        public bool AllowContracting { get; set; }
        public State State { get; set; }
        public IList<EditRangeModel> DevelopmentRanges { get; set; } = new List<EditRangeModel>();
        public IList<EditLeagueTyreModel> LeagueTyres { get; set; } = new List<EditLeagueTyreModel>();

        public EditLeagueModel(League? league)
        {
            if (league == null)
            {
                _league = new();
            }
            else
            {
                Id = league.Id;
                Name = league.Name;
                RaceLength = league.RaceLength;
                UsePenalty = league.Options.HasFlag(LeagueOptions.EnablePenalty);
                EnableFatality = league.Options.HasFlag(LeagueOptions.EnableFatality);
                AllowContracting = league.Options.HasFlag(LeagueOptions.AllowContracting);
                State = league.State;
                DevelopmentRanges = league.DevelopmentRanges?
                    .Select(range => new EditRangeModel(range))
                    .ToList()
                    ?? new List<EditRangeModel>();

                if (league.LeagueTyres?.Any() == true)
                    LeagueTyres = league.LeagueTyres.Select(e => new EditLeagueTyreModel(e)).ToList();

                _league = league;
            }
        }

        public League Record =>
            new()
            {
                Id = Id,
                Name = Name ?? string.Empty,
                RaceLength = RaceLength,
                Options = DetermineOptions(),
                State = State,
                DevelopmentRanges = DevelopmentRanges.Select(e => e.Record).ToList(),
                LeagueTyres = LeagueTyres.Select(e => e.Record).ToList()
            };

        public bool IsDirty => _league != Record || DevelopmentRanges.Any(e => e.IsDirty) || LeagueTyres.Any(e => e.IsDirty);

        private LeagueOptions DetermineOptions()
        {
            var baseOptions = LeagueOptions.None;

            if (UsePenalty)
                baseOptions |= LeagueOptions.EnablePenalty;
            if (EnableFatality)
                baseOptions |= LeagueOptions.EnableFatality;
            if (AllowContracting)
                baseOptions |= LeagueOptions.AllowContracting;

            return baseOptions;
        }
    }

    public class EditRangeModel
    {
        private readonly DevelopmentRange _range;

        public long Id { get; set; }
        public Aspect Type { get; set; }
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

    public sealed class EditLeagueTyreModel
    {
        private readonly LeagueTyre _leagueTyre;

        public long LeagueId { get; set; }
        public long TyreId { get; set; }

        public EditLeagueTyreModel() { _leagueTyre = new(); }
        public EditLeagueTyreModel(LeagueTyre leagueTyre)
        {
            LeagueId = leagueTyre.LeagueId;
            TyreId = leagueTyre.TyreId;

            _leagueTyre = leagueTyre;
        }

        public LeagueTyre Record =>
            new()
            {
                LeagueId = LeagueId,
                TyreId = TyreId
            };

        public bool IsDirty => _leagueTyre != Record;
    }
}
