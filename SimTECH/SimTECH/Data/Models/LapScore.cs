using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public class LapScore
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public int Score { get; set; }
        public RacerEvent RacerEvents { get; set; }

        public long ResultId { get; set; }
        public Result Result { get; set; }
    }

    public static class ExtendLapScore
    {
        public static IEnumerable<Enum> ListOfDubiousEvents(this LapScore lapScore) =>
            lapScore.RacerEvents.GetFlagged().Where(e => e.ToString() != "Unknown" && e.ToString() != "Racing");
    }
}
