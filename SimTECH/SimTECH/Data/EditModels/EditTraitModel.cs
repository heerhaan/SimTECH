using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels
{
    public class EditTraitModel
    {
        private readonly Trait _trait;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Entrant Type { get; set; }
        public State State { get; set; }
        public int QualifyingPace { get; set; }
        public int RacePace { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
        public int RngMin { get; set; }
        public int RngMax { get; set; }
        public bool ForWetConditions { get; set; }

        public EditTraitModel(Trait? trait)
        {
            if (trait == null)
            {
                _trait = new();
            }
            else
            {
                Id = trait.Id;
                Name = trait.Name;
                Description = trait.Description;
                Type = trait.Type;
                State = trait.State;
                QualifyingPace = trait.QualifyingPace;
                RacePace = trait.RacePace;
                Attack = trait.Attack;
                Defense = trait.Defense;
                DriverReliability = trait.DriverReliability;
                CarReliability = trait.CarReliability;
                EngineReliability = trait.EngineReliability;
                WearMax = trait.WearMax;
                WearMin = trait.WearMin;
                RngMin = trait.RngMin;
                RngMax = trait.RngMax;
                ForWetConditions = trait.ForWetConditions;

                _trait = trait;
            }
        }

        public Trait Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Type = Type,
                State = State,
                QualifyingPace = QualifyingPace,
                RacePace = RacePace,
                Attack = Attack,
                Defense = Defense,
                DriverReliability = DriverReliability,
                CarReliability = CarReliability,
                EngineReliability = EngineReliability,
                WearMax = WearMax,
                WearMin = WearMin,
                RngMin = RngMin,
                RngMax = RngMax,
                ForWetConditions = ForWetConditions
            };

        public bool IsDirty => _trait != Record;
    }
}
