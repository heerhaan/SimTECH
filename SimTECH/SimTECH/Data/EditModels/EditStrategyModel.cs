using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditStrategyModel
    {
        private readonly Strategy _strategy;

        public long Id { get; set; }
        public int StintLength { get; set; }
        public State State { get; set; }
        public IList<EditStrategyTyreModel> StrategyTyres { get; set; } = new List<EditStrategyTyreModel>();

        public EditStrategyModel() { _strategy = new Strategy(); }
        //public EditStrategyModel(Strategy strategy) {  }

        public Strategy Record =>
            new()
            {
                Id = Id,
                StintLength = StintLength,
                State = State,

                StrategyTyres = StrategyTyres
                    .Select(e => new StrategyTyre { NumberStint = e.NumberStint, TyreId = e.TyreId, StrategyId = Id })
                    .ToList()
            };

        public bool IsDirty => _strategy != Record;
    }

    public class EditStrategyTyreModel
    {
        private readonly StrategyTyre _strategyTyre;

        //public long Id { get; set; }
        public int NumberStint { get; set; }
        //public long StrategyId { get; set; }
        public long TyreId { get; set; }

        public EditStrategyTyreModel() { _strategyTyre = new StrategyTyre(); }
        //public EditStrategyTyreModel(StrategyTyre strategyTyre) { }
    }
}
