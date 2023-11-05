using SimTECH.Common.Enums;
using SimTECH.Extensions;

namespace SimTECH.PageModels.Seasons.Standings
{
    public class StandingResultCell
    {
        public int Position { get; set; }
        public RaceStatus Status { get; set; }
        public int Round { get; set; }

        public bool Pole { get; set; }
        public bool FL { get; set; }
    }

    public static class ExtendStandingResultCell
    {
        public static string GetResultStyle(this StandingResultCell cell, int lowestScoringPosition)
        {
            if (cell.Status == RaceStatus.Racing)
            {
                return cell.Position switch
                {
                    1 => "background-color: gold;",
                    2 => "background-color: silver;",
                    3 => "background-color: burlywood;",
                    int n when n <= lowestScoringPosition => "background-color: lightgreen;",
                    _ => "background-color: cornflowerblue",
                };
            }

            return cell.Status.StatusStyles();
        }

        public static string GetResultText(this StandingResultCell cell)
        {
            if (cell.Status == RaceStatus.Racing)
                return cell.Position.ToString();

            return cell.Status.ReadableStatus();
        }
    }
}
