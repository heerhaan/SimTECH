using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public sealed class SeasonTeam : ModelBase
    {
        public string Name { get; set; }
        public string Principal { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Points { get; set; }
        public double HiddenPoints { get; set; }

        public int BaseValue { get; set; } = 100;
        public int Aero { get; set; } = 10;
        public int Chassis { get; set; } = 10;
        public int Powertrain { get; set; } = 10;//More like a "drivetrain" in this instance
        public int Reliability { get; set; } = 1000;

        //public int StandingsGoal { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long SeasonId { get; set; }
        public Season Season { get; set; }
        public long SeasonEngineId { get; set; }
        public SeasonEngine SeasonEngine { get; set; }
        public long ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public long? ClassId { get; set; }
        public RaceClass? Class { get; set; }

        public IList<SeasonDriver>? SeasonDrivers { get; set; }
        public IList<Result>? Results { get; set; }
    }

    public static class ExtendSeasonTeam
    {
        public static int RetrieveAspectValue(this SeasonTeam team, Aspect aspect) => aspect switch
        {
            Aspect.BaseCar => team.BaseValue,
            Aspect.Reliability => team.Reliability,
            Aspect.Aero => team.Aero,
            Aspect.Chassis => team.Chassis,
            Aspect.Powertrain => team.Powertrain,
            _ => throw new InvalidOperationException("Invalid aspect for this entrant")
        };

        public static int ModifierApplication(this SeasonTeam team, Track? track)
        {
            var modifiers = (team.Aero * track?.AeroMod ?? 1)
                + (team.Chassis * track?.ChassisMod ?? 1)
                + (team.Powertrain * track?.PowerMod ?? 1);

            return modifiers.RoundDouble();
        }
    }
}
