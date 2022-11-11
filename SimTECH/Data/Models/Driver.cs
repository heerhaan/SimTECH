namespace SimTECH.Data.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Abbreviation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Biography { get; set; }
        public bool Archived { get; set; }
    }
}
