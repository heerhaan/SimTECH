namespace SimTECH.PageModels.Racing
{
    // TODO: Eventually phase out the whole RaceModel and RaceDriverModel for the classes in this folder
    public abstract class StageBase
    {
        public long RaceId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Climate { get; set; }
        public string ClimateIcon { get; set; }
        public bool IsFinished { get; set; }
    }
}
