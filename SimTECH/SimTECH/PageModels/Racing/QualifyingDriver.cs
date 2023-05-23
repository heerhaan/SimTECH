using SimTECH.Extensions;

namespace SimTECH.PageModels.Racing
{
    public class QualifyingDriver : DriverBase
    {
        public int FinalPosition { get; set; }
        public double PenaltyPunishment { get; set; }

        public List<QualifyingSession> Sessions { get; set; }
    }

    public static class ExtendQualifyingDriver
    {
        public static int GetQualyResult(this QualifyingDriver driver, int rng) => 
            driver.Power + NumberHelper.RandomInt(rng);

        public static double GetPositionAfterPenalty(this QualifyingDriver driver) =>
            driver.FinalPosition + driver.PenaltyPunishment;
    }
}
