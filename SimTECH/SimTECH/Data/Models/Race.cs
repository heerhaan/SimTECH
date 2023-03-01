namespace SimTECH.Data.Models
{
    public class Race
    {
        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; } = default!;
        public Weather Weather { get; set; }
        public State State { get; set; }

        public IList<Stint> Stints { get; set; } = default!;
        public IList<Penalty> Penalties { get; set; } = default!;
        public IList<Result> Results { get; set; } = default!;

        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public long TrackId { get; set; }
        public Track Track { get; set; } = default!;
    }
}
