using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditDevRangeModel
    {
        private readonly DevelopmentRange _range;

        public long Id { get; set; }
        public RangeType Type { get; set; }
        public int Comparer { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public long LeagueId { get; set; }

        public EditDevRangeModel()
        {
            _range = new DevelopmentRange();
        }
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
