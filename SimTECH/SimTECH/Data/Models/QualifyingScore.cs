using System.ComponentModel.DataAnnotations.Schema;

namespace SimTECH.Data.Models;

public sealed class QualifyingScore : ScoreBase
{
    [NotMapped]
    public int PenaltyPunish { get; set; }//Doesnt really belong here tbh
}

public static class ExtendQualifyingScore
{
    public static int PenaltyPosition(this QualifyingScore score)
    {
        return score.Position + score.PenaltyPunish;
    }
}
