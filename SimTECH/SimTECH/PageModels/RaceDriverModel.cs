using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.PageModels
{
    // Er is een betere manier om de sessies te implementeren maar ik weet even niet hoe
    public abstract class DriverBase
    {
        public long ResultId { get; set; }

        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }
    }

    public class RaceDriver : DriverBase
    {
        public RaceStatus Status { get; set; }
        public Incident Incident { get; set; }

        public int Grid { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }

        public Tyre CurrentTyre { get; set; }
        public Strategy Strategy { get; set; }

        public int NormalizedPower { get; set; }
        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMaxMod { get; set; }
        public int WearMinMod { get; set; }
        public int RngMinMod { get; set; }
        public int RngMaxMod { get; set; }

        public int Position { get; set; }
        public string DisplayGap { get; set; }

        public List<int> LapScores { get; set; } = new();

        public int LapSum => LapScores.Sum();

        public Result ToResult()
        {
            return new Result
            {
                Id = ResultId,
                Grid = Grid,
                Position = Position,
                Score = LapSum,
                Status = Status,
                Incident = Incident,
                Setup = Setup,
                TyreLife = TyreLife,
            };
        }
    }

    public class QualifyingDriver : DriverBase
    {
        // We now repeat these properties three times, enforcing (for now) three sessions but it's not too hard to make a list of this
        public int[] RunValuesQ1 { get; set; }
        public int PositionQ1 { get; set; }
        public int MaxScoreQ1 => RunValuesQ1.Max();
        public string DisplayGapQ1 { get; set; }

        public int[] RunValuesQ2 { get; set; }
        public int PositionQ2 { get; set; }
        public int MaxScoreQ2 => RunValuesQ2.Max();
        public string DisplayGapQ2 { get; set; }

        public int[] RunValuesQ3 { get; set; }
        public int PositionQ3 { get; set; }
        public int MaxScoreQ3 => RunValuesQ3.Max();
        public string DisplayGapQ3 { get; set; }

        public int Position { get; set; }

        public TraitEffect TraitEffect { get; set; }

        public int GetQualifyingResult(int maxRng) => Power + TraitEffect.QualifyingPace + NumberHelper.RandomInt(maxRng);
    }

    public class PracticeDriver : DriverBase
    {
        public int[] RunValues { get; set; }

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunValues.Max();
    }
}
