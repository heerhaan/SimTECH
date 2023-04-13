using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.PageModels
{
    // Er is een betere manier om de sessies te implementeren maar ik weet even niet hoe
    public abstract class DriverBase
    {
        public long ResultId { get; set; }
        public long SeasonDriverId { get; set; }
        public long SeasonTeamId { get; set; }

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

        public int Position { get; set; }
        public int Grid { get; set; }
        public int Setup { get; set; }
        public int CurrentTyreOrder { get; set; }
        public int TyreLife { get; set; }

        public Tyre CurrentTyre { get; set; }
        public Strategy Strategy { get; set; }

        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMaxMod { get; set; }
        public int WearMinMod { get; set; }
        public int RngMinMod { get; set; }
        public int RngMaxMod { get; set; }

        public string DisplayGap { get; set; }
        public bool HasFastestLap { get; set; }

        public List<int> LapScores { get; set; } = new();
        public Dictionary<int, RacerEvent> RaceEventsPerLap { get; set; } = new();

        public int LapSum => LapScores?.Sum() ?? 0;
        public int GridChange => Grid - Position;

        public Result ToResult(long raceId)
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

                SeasonDriverId = SeasonDriverId,
                SeasonTeamId = SeasonTeamId,
                RaceId = raceId,
                StrategyId = Strategy.Id,
            };
        }

        public ScoredPoints ToScoredPoints(Dictionary<int, int> allotments, int polePoints, int fastLapPoints, bool hasFastestLap)
        {
            var points = 0;
            var hiddenPoints = 0;

            if (allotments.TryGetValue(Position, out int allotedPoints))
                points += allotedPoints;

            if (Grid == 1)
                points += polePoints;

            if (hasFastestLap)
                points += fastLapPoints;

            if (Status == RaceStatus.Racing)
            {
                double positionCalc = 20000000 / Position;

                hiddenPoints = positionCalc.RoundDouble();
            }

            return new ScoredPoints
            {
                SeasonDriverId = SeasonDriverId,
                SeasonTeamId = SeasonTeamId,
                Points = points,
                HiddenPoints = hiddenPoints,
            };
        }
    }

    public class ScoredPoints
    {
        public long SeasonDriverId { get; set; }
        public long SeasonTeamId { get; set; }

        public int Points { get; set; }
        public int HiddenPoints { get; set; }
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

        public int BaseValue { get; set; }
        public int EnginePower { get; set; }
        public List<long> DriverTraitIds { get; set; }
        public List<long> TeamTraitIds { get; set; }

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
