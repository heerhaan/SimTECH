using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.EditModels
{
    public class EditTyreModel
    {
        private readonly Tyre _tyre;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public State State { get; set; }

        public int LifeBonus { get; set; } = 100;
        public int PitWhenBelow { get; set; } = 10;
        public int WearMin { get; set; } = 2;
        public int WearMax { get; set; } = 5;
        public int DistanceMin { get; set; } = 50;
        public int DistanceMax { get; set; } = 200;
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
                LifeBonus = tyre.Pace;
                PitWhenBelow = tyre.PitWhenBelow;
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
                Pace = LifeBonus,
                PitWhenBelow = PitWhenBelow,
                WearMax = WearMax,
                WearMin = WearMin,
                DistanceMin = DistanceMin,
                DistanceMax = DistanceMax,
                ForWet = ForWet,
            };

        public bool IsDirty => _tyre != Record;

        public int ExpectedLength(int distance)
        {
            if (LifeBonus == 0 || (WearMin == 0 && WearMax == 0))
                return 0;

            var wearAvg = (double)(WearMin + WearMax) / 2;
            var averageLength = LifeBonus / wearAvg * distance;

            return averageLength.RoundDouble();
        }
    }
}
