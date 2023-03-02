using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TraitService
    {
        private readonly List<Trait> _traits = new()
        {
            new Trait()
            {
                Id = 1,
                Name = "Foo",
                Description = "Fighter",
                Type = Entrant.Driver,
                State = State.Active,

                QualifyingPace = NumberHelper.RandomInt(-2, 2),
                DriverPace = NumberHelper.RandomInt(-2, 2),
                CarPace = NumberHelper.RandomInt(-2, 2),
                EnginePace = NumberHelper.RandomInt(-2, 2),
                DriverReliability = NumberHelper.RandomInt(-2, 2),
                CarReliability = NumberHelper.RandomInt(-2, 2),
                EngineReliability = NumberHelper.RandomInt(-2, 2),
                WearMax = NumberHelper.RandomInt(-2, 2),
                WearMin = NumberHelper.RandomInt(-2, 2),

                RngMin = NumberHelper.RandomInt(-2, 2),
                RngMax = NumberHelper.RandomInt(-2, 2),

                ForWetConditions = false,
            },
        };

        public List<Trait> GetTestData()
        {
            return _traits;
        }

        public void CreateTrait(Trait trait)
        {
            ValidateTrait(trait);

            _traits.Add(trait);
        }

        #region validation

        private static void ValidateTrait(Trait trait)
        {
            if (trait == null)
                throw new NullReferenceException("Trait is very null here, yes");
        }

        #endregion
    }
}
