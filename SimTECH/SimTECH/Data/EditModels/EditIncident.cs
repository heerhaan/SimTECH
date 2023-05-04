using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditIncident
    {
        private readonly Incident _incident;

        public long Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public Entrant ForEntrant { get; set; }
        public RaceStatus ForStatus { get; set; }
        public int Limit { get; set; }
        public int Punishment { get; set; }

        public EditIncident(Incident? incident)
        {
            if (incident == null)
            {
                _incident = new();
            }
            else
            {
                Id = incident.Id;
                Name = incident.Name;
                State = incident.State;
                ForEntrant = incident.ForEntrant;
                ForStatus = incident.ForStatus;
                Limit = incident.Limit;
                Punishment = incident.Punishment;

                _incident = incident;
            }
        }

        public Incident Record =>
            new()
            {
                Id = Id,
                Name = Name,
                State = State,
                ForEntrant = ForEntrant,
                ForStatus = ForStatus,
                Limit = Limit,
                Punishment = Punishment
            };

        public bool IsDirty => _incident != Record;
    }
}
