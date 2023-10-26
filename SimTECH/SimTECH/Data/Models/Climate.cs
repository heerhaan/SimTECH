using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public class Climate : ModelState
    {
        public string Terminology { get; set; }
        public string Icon { get; set; }
        public bool IsWet { get; set; }
        public double EngineMultiplier { get; set; }
        public int ReliablityModifier { get; set; }
        public int RngModifier { get; set; }
        public int Odds { get; set; }
        public string? Colour { get; set; }
    }

    public static class ExtendClimate
    {
        public static Climate TakeRandomClimate(this List<Climate> climates)
        {
            var weightedList = new List<long>();

            foreach (var climate in climates)
            {
                for (int i = 0; i < climate.Odds; i++)
                {
                    weightedList.Add(climate.Id);
                }
            }

            var randomId = weightedList.TakeRandomItem();
            return climates.First(e => e.Id == randomId);
        }
    }
}
