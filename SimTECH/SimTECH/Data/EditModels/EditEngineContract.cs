//using SimTECH.Data.Models;

//namespace SimTECH.Data.EditModels
//{
//    public class EditEngineContract
//    {
//        private readonly EngineContract _contract;

//        public long Id { get; set; }
//        public int Duration { get; set; }
//        public long TeamId { get; set; }
//        public long EngineId { get; set; }
//        public long LeagueId { get; set; }

//        // Supportive fields
//        public Team? Team { get; set; }
//        public Engine? Engine { get; set; }

//        public EditEngineContract(EngineContract? contract)
//        {
//            if (contract == null)
//            {
//                _contract = new();
//            }
//            else
//            {
//                Id = contract.Id;
//                Duration = contract.Duration;
//                TeamId = contract.TeamId;
//                EngineId = contract.EngineId;
//                LeagueId = contract.LeagueId;

//                _contract = contract;
//            }
//        }

//        public EngineContract Record =>
//            new()
//            {
//                Id = Id,
//                Duration = Duration,
//                TeamId = TeamId,
//                EngineId = EngineId,
//                LeagueId = LeagueId,
//            };

//        public bool IsDirty => _contract != Record;

//        public bool SetToZero => Duration == 0;
//    }
//}
