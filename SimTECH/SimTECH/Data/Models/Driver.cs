namespace SimTECH.Data.Models
{
    public class Driver
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Abbreviation { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public Country Country { get; set; } = default!;
        public string Biography { get; set; } = default!;
        public State State { get; set; }

        public IList<SeasonDriver> SeasonDrivers { get; set; } = default!;
        public IList<DriverTrait> DriverTraits { get; set; } = default!;
        public IList<Contract> Contracts { get; set; } = default!;
    }
}
