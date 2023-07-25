namespace SimTECH.Data.Models
{
    public sealed class Race : ModelState
    {
        public int Round { get; set; }
        public string Name { get; set; } = default!;
        public int RaceLength { get; set; }
        public DateTime? DateFinished { get; set; }

        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public long TrackId { get; set; }
        public Track Track { get; set; } = default!;
        public long ClimateId { get; set; }
        public Climate Climate { get; set; }

        public IList<RaceOccurrence> Occurrences { get; set; } = default!;
        public IList<Result> Results { get; set; } = default!;
    }

    public static class ExtendRace
    {
        public static Race? FindNext(this IEnumerable<Race> races)
        {
            if (!races.Any())
                return null;

            return races
                .OrderBy(e => e.Round)
                .FirstOrDefault(e => e.State == State.Concept || e.State == State.Active || e.State == State.Advanced);
        }
    }
}
