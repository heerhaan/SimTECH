namespace SimTECH
{
    public class SimConfig
    {
        public const string SectionKey = "SimConfig";

        public int DisqualifyChance { get; set; }
        public double GapMarge { get; set; }

        // MistakeAmountRolls refers to the consequent rolls that need to be done before a driver makes a mistake, mistakes are accounted for in each stint for each driver
        public int MistakeAmountRolls { get; set; }
        public int MistakeLowerValue { get; set; }
        public int MistakeUpperValue { get; set; }

        // Applies to the upper RNG value of a stint
        public int RainAdditionalRNG { get; set; }
        public int StormAdditionalRNG { get; set; }
        public int RainDriverReliabilityModifier { get; set; }
        public int StormDriverReliabilityModifier { get; set; }

        public double SunnyEngineMultiplier { get; set; }
        public double OvercastEngineMultiplier { get; set; }
        public double WetEngineMultiplier { get; set; }

        // The additional chassis value a #1 driver gets and a #2 driver loses
        public int CarDriverStatusModifier { get; set; }
    }
}
