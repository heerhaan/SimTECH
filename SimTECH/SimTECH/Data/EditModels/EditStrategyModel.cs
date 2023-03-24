﻿using SimTECH.Data.Models;

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
        public EditStrategyModel(Strategy strategy)
        {
            Id = strategy.Id;
            StintLength = strategy.StintLength;
            State = strategy.State;
            StrategyTyres = strategy.StrategyTyres?
                .Select(e => new EditStrategyTyreModel(e))
                .ToList()
                ?? new List<EditStrategyTyreModel>();

            _strategy = strategy;
        }

        public Strategy Record =>
            new()
            {
                Id = Id,
                StintLength = StintLength,
                State = State,

                StrategyTyres = StrategyTyres
                    .Select(e => e.Record)
                    .ToList()
            };

        public bool IsDirty => _strategy != Record || StrategyTyres.Any(e => e.IsDirty);
    }

    public class EditStrategyTyreModel
    {
        private readonly StrategyTyre _strategyTyre;

        public long Id { get; set; }
        public int NumberStint { get; set; }
        public long StrategyId { get; set; }
        public long TyreId { get; set; }
        public Tyre? Tyre { get; set; }

        public EditStrategyTyreModel() { _strategyTyre = new StrategyTyre(); }
        public EditStrategyTyreModel(StrategyTyre strategyTyre)
        {
            Id = strategyTyre.Id;
            NumberStint = strategyTyre.NumberStint;
            StrategyId = strategyTyre.StrategyId;
            TyreId = strategyTyre.TyreId;
            Tyre = strategyTyre.Tyre;

            _strategyTyre = strategyTyre;
        }

        public StrategyTyre Record =>
            new()
            {
                Id = Id,
                NumberStint = NumberStint,
                StrategyId = StrategyId,
                TyreId = TyreId
            };

        public bool IsDirty => _strategyTyre != Record;
    }
}