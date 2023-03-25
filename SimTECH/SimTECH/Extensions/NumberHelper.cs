using SimTECH.Data.Models;
using SimTECH.PageModels;

namespace SimTECH.Extensions
{
    public static class NumberHelper
    {
        private static readonly Random _rng = new();

        public static int RoundDouble(this double number)
        {
            if (number < 0)
            {
                return (int)(number - 0.5);
            }
            return (int)(number + 0.5);
        }

        public static int RandomInt(int max) => RandomInt(0, max);
        public static int RandomInt(int min, int max)
        {
            return _rng.Next(min, max + 1);
        }

        public static double RandomDouble(double max) => RandomDouble(0, max);
        public static double RandomDouble(double min, double max)
        {
            return _rng.NextDouble() * (max - min) + min;
        }

        public static TraitEffect SumTraitEffects(IEnumerable<Trait> traits)
        {
            return new TraitEffect
            {
                QualifyingPace          = traits.Sum(e => e.QualifyingPace),
                DriverPace              = traits.Sum(e => e.DriverPace),
                CarPace                 = traits.Sum(e => e.CarPace),
                EnginePace              = traits.Sum(e => e.EnginePace),
                DriverReliability       = traits.Sum(e => e.DriverReliability),
                CarReliability          = traits.Sum(e => e.CarReliability),
                EngineReliability       = traits.Sum(e => e.EngineReliability),
                WearMaximum             = traits.Sum(e => e.WearMax),
                WearMinimum             = traits.Sum(e => e.WearMin),
                MaxRNG                  = traits.Sum(e => e.RngMax),
                MinRNG                  = traits.Sum(e => e.RngMin),
            };
        }
    }
}
