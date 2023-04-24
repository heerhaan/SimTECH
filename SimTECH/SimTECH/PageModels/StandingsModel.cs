namespace SimTECH.PageModels
{
    public abstract class StandingEntrantBase
    {
        public long Id { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public Country Nationality { get; set; }
        public int Points { get; set; }
        public int HiddenPoints { get; set; }

        public string Colour { get; set; }
        public string Accent { get; set; }

        public List<StandingResultCell> ResultCells { get; set; }
    }

    public class StandingDriverModel : StandingEntrantBase
    {
        public int Number { get; set; }
        public TeamRole TeamRole { get; set; }
        public string Team { get; set; }
        public long? SeasonTeamId { get; set; }
    }

    public class StandingTeamModel : StandingEntrantBase
    {
        public string Principal { get; set; }
    }

    public class StandingResultCell
    {
        public int Position { get; set; }
        public RaceStatus Status { get; set; }
        public int Round { get; set; }

        public bool Pole { get; set; }
        public bool FL { get; set; }

        public string GetResultText()
        {
            return Status switch
            {
                RaceStatus.Dnq => "DNQ",
                RaceStatus.Dsq => "DSQ",
                RaceStatus.Dnf => "DNF",
                _ => Position.ToString(),
            };
        }
    }
}
