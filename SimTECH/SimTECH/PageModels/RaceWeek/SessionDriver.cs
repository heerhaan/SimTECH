using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek;

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
    public int AbsolutePosition { get; set; }
    public int PenaltyPunish { get; set; }
    public string GapAbove { get; set; } = string.Empty;
    public int Setup { get; set; }

    public long ClassId { get; set; }
    public RaceClass? Class { get; set; }

    //public int MaximumScore
    //{
    //    get
    //    {
    //        return Scores.Max();
    //    }
    //}
}

public static class ExtendSessionDriver
{
    public static int MaxScore(this SessionDriver driver) => driver.Scores.Max();
}
