namespace SimTECH.PageModels
{
    public abstract class WinnerModel
    {
        public string Name { get; set; }
        public Country Country { get; set; }
    }

    public class DriverWinner : WinnerModel
    {
        public int Number { get; set; }
    }

    public class TeamWinner : WinnerModel
    {
        public string Colour { get; set; }
        public string Accent { get; set; }
    }
}
