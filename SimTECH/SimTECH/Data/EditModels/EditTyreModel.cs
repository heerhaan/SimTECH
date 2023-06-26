﻿using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditTyreModel
    {
        private readonly Tyre _tyre;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public State State { get; set; }

        public int Pace { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
        public int DistanceMin { get; set; }
        public int DistanceMax { get; set; }
        public bool ForWet { get; set; }

        public EditTyreModel(Tyre? tyre)
        {
            if (tyre == null)
            {
                _tyre = new();
            }
            else
            {
                Id = tyre.Id;
                Name = tyre.Name;
                Colour = tyre.Colour;
                State = tyre.State;
                Pace = tyre.Pace;
                WearMin = tyre.WearMin;
                WearMax = tyre.WearMax;
                DistanceMin = tyre.DistanceMin;
                DistanceMax = tyre.DistanceMax;
                ForWet = tyre.ForWet;

                _tyre = tyre;
            }
        }

        public Tyre Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Colour = Colour,
                State = State,
                Pace = Pace,
                WearMax = WearMax,
                WearMin = WearMin,
                DistanceMin = DistanceMin,
                DistanceMax = DistanceMax,
                ForWet = ForWet,
            };

        public bool IsDirty => _tyre != Record;
    }
}
