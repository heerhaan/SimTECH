using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public abstract class SessionBase
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }

        public int AmountRuns { get; set; }//Can be used for both runs in qualy and stints in race
    }

    public class RaceModel : SessionBase
    {
        public List<RaceDriver> RaceDrivers { get; set; }

        public Season Season { get; set; }//pick values intead of whole object?
    }

    public class QualifyingModel : SessionBase
    {
        public List<QualifyingDriver> QualifyingDrivers { get; set; }

        public Season Season { get; set; }//pick values intead of whole object?

        public int HighestScoreQ1() => QualifyingDrivers.Max(e => e.MaxScoreQ1);
        public int HighestScoreQ2() => QualifyingDrivers.Max(e => e.MaxScoreQ2);
        public int HighestScoreQ3() => QualifyingDrivers.Max(e => e.MaxScoreQ3);
    }

    public class PracticeModel : SessionBase
    {
        public List<PracticeDriver> PracticeDrivers { get; set; }
    }
}
