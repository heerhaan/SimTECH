using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public sealed class LapScore
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lapScore"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> ListOfDubiousEvents(this LapScore lapScore) =>
            lapScore.RacerEvents.GetFlagged().Where(e => e.ToString() != "Unknown" && e.ToString() != "Racing");

        public static string DetermineLapColour(this LapScore lapScore)
        {
            if (lapScore.RacerEvents.HasFlag(RacerEvent.DriverDnf) 
                || lapScore.RacerEvents.HasFlag(RacerEvent.CarDnf) 
                || lapScore.RacerEvents.HasFlag(RacerEvent.EngineDnf))
            {
                return "red";
            }
            else if (lapScore.RacerEvents.HasFlag(RacerEvent.Caution))
            {
                return "yellow";
            }
            else
            {
                return Constants.DefaultAccent;
            }
        }
    }
}
