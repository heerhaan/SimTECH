using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditClimate
    {
        private readonly Climate _climate;

        public long Id { get; set; }
        public string Terminology { get; set; }
        public string Icon { get; set; }
        public string? Colour { get; set; }
        public int Odds { get; set; }
        public bool IsWet { get; set; }
        public State State { get; set; }

        public double EngineMultiplier { get; set; }
        public int ReliablityModifier { get; set; }
        public int RngModifier { get; set; }

        public EditClimate(Climate? climate)
        {
            if (climate == null)
            {
                _climate = new();
            }
            else
            {
                Id = climate.Id;
                Terminology = climate.Terminology;
                Icon = climate.Icon;
                Colour = climate.Colour;
                Odds = climate.Odds;
                IsWet = climate.IsWet;
                State = climate.State;
                EngineMultiplier = climate.EngineMultiplier;
                ReliablityModifier = climate.ReliablityModifier;
                RngModifier = climate.RngModifier;

                _climate = climate;
            }
        }

        public Climate Record =>
            new()
            {
                Id = Id,
                Terminology = Terminology,
                Icon = Icon,
                Colour = Colour,
                Odds = Odds,
                IsWet = IsWet,
                State = State,
                EngineMultiplier = EngineMultiplier,
                ReliablityModifier = ReliablityModifier,
                RngModifier = RngModifier
            };

        public bool IsDirty => _climate != Record;
    }
}
