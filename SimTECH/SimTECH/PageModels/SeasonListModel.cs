namespace SimTECH.PageModels
{
    public class SeasonListModel
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public State State { get; set; }

        public LeadingEntrant? LeadingDriver { get; set; }
        public LeadingEntrant? LeadingTeam { get; set; }
    }

    public class LeadingEntrant
    {
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }
    }
}
