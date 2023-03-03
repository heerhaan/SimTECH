using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditLeague
    {
        private League _league;

        public long Id { get; set; }
        public string? Name { get; set; }
        public State State { get; set; }
        public IList<EditDevelopmentRange> DevelopmentRanges { get; set; }

        public EditLeague()
        {
            DevelopmentRanges = new List<EditDevelopmentRange>();
            _league = new League();
        }
        public EditLeague(League league)
        {
            Id = league.Id;
            Name = league.Name;
            State = league.State;
            DevelopmentRanges = league.DevelopmentRanges
                .Select(range => new EditDevelopmentRange(range))
                .ToList();
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

        public bool IsDirty => _league != this.Record;
    }
}
