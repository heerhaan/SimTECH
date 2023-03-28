using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public class RaceModel
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public Weather Weather { get; set; }

        public int AmountRuns { get; set; }//Can be used for both runs in qualy and stints in race

        public List<RaceDriver> OldRaceDrivers { get; set; }//ew delet
        public List<DriverBase> RaceDrivers { get; set; }

        public Season Season { get; set; }//pick values intead of whole object?
    }
}
