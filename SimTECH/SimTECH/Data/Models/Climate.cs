namespace SimTECH.Data.Models
{
    // Defaults: Sunny, Overcast, Rain, Storm
    public class Climate : ModelState
    {
        public string Terminology { get; set; }
        public string Icon { get; set; }
        public bool IsWet { get; set; }
        public double EngineMultiplier { get; set; }
        public int ReliablityModifier { get; set; }
        public int RngModifier { get; set; }
    }
}
