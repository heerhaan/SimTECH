using System.ComponentModel.DataAnnotations.Schema;

namespace SimTECH.Data.Models
{
    public sealed class Driver : ModelState
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

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }

    public static class ExtendDriver
    {
    }
}
