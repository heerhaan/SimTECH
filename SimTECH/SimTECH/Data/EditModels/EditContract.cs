using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditContract
    {
        private readonly Contract _contract;

        public long Id { get; set; }
        public int Duration { get; set; } = 1;
        public long TeamId { get; set; }
        public long DriverId { get; set; }
        public long LeagueId { get; set; }

        // Supportive fields
        public Team? Team { get; set; }
        public Driver? Driver { get; set; }

        public EditContract(Contract? contract)
        {
            if (contract == null)
            {
                _contract = new();
            }
            else
            {
                Id = contract.Id;
                Duration = contract.Duration;
                TeamId = contract.TeamId;
                DriverId = contract.DriverId;
                LeagueId = contract.LeagueId;

                _contract = contract;
            }
        }

        public Contract Record =>
            new()
            {
                Id = Id,
                Duration = Duration,
                TeamId = TeamId,
                DriverId = DriverId,
                LeagueId = LeagueId,
            };

        public bool IsDirty => _contract != Record;

        public bool SetToZero => Duration == 0;
    }
}
