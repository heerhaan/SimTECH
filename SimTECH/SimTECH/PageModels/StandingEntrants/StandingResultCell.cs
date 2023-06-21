namespace SimTECH.PageModels.StandingEntrants
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
            string? cellStyle;

            if (cell.Status == RaceStatus.Dnq)
            {
                cellStyle = "background-color: rebeccapurple;";
            }
            else if (cell.Status == RaceStatus.Dsq)
            {
                cellStyle = "background-color: black;color:white";
            }
            else if (cell.Status == RaceStatus.Dnf)
            {
                cellStyle = "background-color: red;";
            }
            else if (cell.Status == RaceStatus.Fatal)
            {
                cellStyle = "background-color: black;color:white";
            }
            else
            {
                cellStyle = cell.Position switch
                {
                    1 => "background-color: gold;",
                    2 => "background-color: silver;",
                    3 => "background-color: burlywood;",
                    int n when n <= lowestScoringPosition => "background-color: lightgreen;",
                    _ => "background-color: cornflowerblue",
                };
            }

            // Possibly can return immediatly depending on whether we want to do more with this or not
            return cellStyle;
        }

        public static string GetResultText(this StandingResultCell cell)
        {
            return cell.Status switch
            {
                RaceStatus.Dnq => "DNQ",
                RaceStatus.Dsq => "DSQ",
                RaceStatus.Dnf => "DNF",
                RaceStatus.Fatal => "RIP",
                _ => cell.Position.ToString(),
            };
        }
    }
}
