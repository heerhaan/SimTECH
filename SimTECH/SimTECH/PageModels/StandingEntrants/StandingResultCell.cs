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
            var cellStyle = string.Empty;

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
                switch (cell.Position)
                {
                    case 1:
                        cellStyle = "background-color: gold;";
                        break;
                    case 2:
                        cellStyle = "background-color: silver;";
                        break;
                    case 3:
                        cellStyle = "background-color: burlywood;";
                        break;
                    case int n when n <= lowestScoringPosition:
                        cellStyle = "background-color: lightgreen;";
                        break;
                    default:
                        cellStyle = "background-color: cornflowerblue";
                        break;
                }
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
