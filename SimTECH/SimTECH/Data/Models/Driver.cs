namespace SimTECH.Data.Models
{
    public class Driver : ModelState
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Abbreviation { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public Country Country { get; set; }
        public string? Biography { get; set; }
        public bool Mark { get; set; }

        public IList<SeasonDriver>? SeasonDrivers { get; set; }
        public IList<DriverTrait>? DriverTraits { get; set; }
        public IList<Contract>? Contracts { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
