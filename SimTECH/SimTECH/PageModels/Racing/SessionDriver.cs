namespace SimTECH.PageModels.Racing
{
    public class SessionDriver
    {
        public long SeasonDriverId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Country Nationality { get; set; }
        public int Number { get; set; }

        public string TeamName { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Accent { get; set; } = string.Empty;

        public long ResultId { get; set; }

        public int Power { get; set; }

        public int[] Scores { get; set; }
        public int Position { get; set; }
        public int PenaltyPunish { get; set; }
        public string GapAbove { get; set; } = string.Empty;

        public string? CutoffText { get; set; }
        public string? CutoffColour { get; set; }
    }

    public static class ExtendSessionDriver
    {
        public static int MaxScore(this SessionDriver driver) => driver.Scores.Max();
    }
}
