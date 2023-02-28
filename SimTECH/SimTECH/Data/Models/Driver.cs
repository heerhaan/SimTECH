namespace SimTECH.Data.Models
{
    public class Driver
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Abbreviation { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; } = default!;
        public string Biography { get; set; } = default!;
        public State State { get; set; }
    }
}
