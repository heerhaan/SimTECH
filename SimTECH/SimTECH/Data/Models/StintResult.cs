using SimTECH.PageModels;

namespace SimTECH.Data.Models
{
    public record StintResult
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public int StintScore { get; set; }
        public int TotalScore { get; set; }
        public int Position { get; set; }
        public RacerEvent RacerEvents { get; set; }

        public long StintId { get; set; }
        public Stint Stint { get; set; } = default!;
        public long ResultId { get; set; }
        public Result Result { get; set; } = default!;

        public RaceStint ToRaceStint()
        {
            return new RaceStint
            {
                Order = Order,
                Score = StintScore,
                TotalScore = TotalScore,
                Position = Position,
                RacerEvents = RacerEvents,
            };
        }
    }
}
