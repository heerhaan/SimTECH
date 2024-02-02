using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels
{
    public class EditIncident
    {
        private readonly Incident _incident;

        public long Id { get; set; }
        public string Name { get; set; }
        public int Odds { get; set; } = 1;
        public string? Colour { get; set; }
        public State State { get; set; }

        public IncidentCategory Category { get; set; }
        public int Limit { get; set; }
        public int Punishment { get; set; }
        public bool Penalized { get; set; }

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
                Odds = incident.Odds;
                Colour = incident.Colour;
                State = incident.State;
                Category = incident.Category;
                Limit = incident.Limit;
                Punishment = incident.Punishment;
                Penalized = incident.Penalized;

                _incident = incident;
            }
        }

        public Incident Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Odds = Odds,
                Colour = Colour,
                State = State,
                Category = Category,
                Limit = Limit,
                Punishment = Punishment,
                Penalized = Penalized,
            };

        public bool IsDirty => _incident != Record;
    }
}
