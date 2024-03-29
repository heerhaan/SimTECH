﻿using SimTECH.Common.Enums;
using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public sealed class Incident : ModelState
    {
        public string Name { get; set; }
        //public string Icon { get; set; }
        public IncidentCategory Category { get; set; }
        public int Limit { get; set; }
        public int Punishment { get; set; }
        public int Odds { get; set; }
        public bool Penalized { get; set; }
        public string? Colour { get; set; }

        public double DoublePunishment
        {
            get
            {
                return Convert.ToDouble(Punishment) + 0.2;
            }
        }
    }

    public static class ExtendIncident
    {
        public static Incident TakeRandomIncident(this List<Incident> incidents)
        {
            var weightedList = new List<long>();

            foreach (var incident in incidents)
            {
                for (int i = 0; i < incident.Odds; i++)
                {
                    weightedList.Add(incident.Id);
                }
            }

            var randomId = weightedList.TakeRandomItem();
            return incidents.First(e => e.Id == randomId);
        }
    }
}
