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
    }

    public class PracticeModel : SessionBase
    {
        public List<PracticeDriver> PracticeDrivers { get; set; }
    }
}
