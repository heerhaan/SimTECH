namespace SimTECH.Data.Models
{
    public sealed class Race
    {
        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; } = default!;
        public int RaceLength { get; set; }
        public Weather Weather { get; set; }//deprecated
        public State State { get; set; }

        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public long TrackId { get; set; }
        public Track Track { get; set; } = default!;
        public long ClimateId { get; set; }
        public Climate Climate { get; set; }

        public IList<Penalty> Penalties { get; set; } = default!;
        public IList<Result> Results { get; set; } = default!;
    }

    public static class ExtendRace
    {
        public static bool IsWet(this Race race) => race.Weather == Weather.Rain || race.Weather == Weather.Storm;
    }
}
