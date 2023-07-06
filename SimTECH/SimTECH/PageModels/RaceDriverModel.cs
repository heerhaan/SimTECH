﻿using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.PageModels
{
    // Er is een betere manier om de sessies te implementeren maar ik weet even niet hoe
    public abstract class DriverBase
    {
        public long ResultId { get; set; }
        public long SeasonDriverId { get; set; }
        public long SeasonTeamId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public string GapAbove { get; set; }
    }

    public class RaceDriver : DriverBase
    {
        public RaceStatus Status { get; set; }
        public Incident? Incident { get; set; }

        public int Position { get; set; }
        public int Grid { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }

        public Tyre CurrentTyre { get; set; }

        public int DriverReliability { get; set; }
        public int CarReliability { get; set; }
        public int EngineReliability { get; set; }
        public int WearMaxMod { get; set; }
        public int WearMinMod { get; set; }
        public int RngMinMod { get; set; }
        public int RngMaxMod { get; set; }
        public int LifeBonus { get; set; }

        public bool HasFastestLap { get; set; }
        public bool InstantOvertaken { get; set; }
        public bool FrequentMistake { get; set; }

        public List<LapScore> LapScores { get; set; }

        public int LapSum => LapScores.Sum(e => e.Score);
        public int GridChange => Grid - Position;
        public int LastScore { get; set; }
        public int OvertakeCount { get; set; }
        public int DefensiveCount { get; set; }
        public string? SingleOccurrence { get; set; }

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
                FastestLap = HasFastestLap,
                Overtaken = OvertakeCount,
                Defended = DefensiveCount,

                SeasonDriverId = SeasonDriverId,
                SeasonTeamId = SeasonTeamId,
                RaceId = raceId,
                TyreId = CurrentTyre.Id,
                IncidentId = Incident?.Id,
            };
        }

        public ScoredPoints ToScoredPoints(Dictionary<int, int> allotments, int polePoints, int fastLapPoints)
        {
            var points = 0;
            var hiddenPoints = 0;

            if (allotments.TryGetValue(Position, out int allotedPoints))
                points += allotedPoints;

            if (Grid == 1)
                points += polePoints;

            if (HasFastestLap)
                points += fastLapPoints;

            if (Status == RaceStatus.Racing)
            {
                double positionCalc = 200000 / Position;
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

    public class QualifyingDriver : DriverBase
    {
        // We now repeat these properties three times, enforcing (for now) three sessions but it's not too hard to make a list of this
        public int[] RunValuesQ1 { get; set; }
        public int PositionQ1 { get; set; }
        public int MaxScoreQ1 => RunValuesQ1.Max();
        public double GapQ1 { get; set; }

        public int[] RunValuesQ2 { get; set; }
        public int PositionQ2 { get; set; }
        public int MaxScoreQ2 => RunValuesQ2.Max();
        public double GapQ2 { get; set; }

        public int[] RunValuesQ3 { get; set; }
        public int PositionQ3 { get; set; }
        public int MaxScoreQ3 => RunValuesQ3.Max();
        public double GapQ3 { get; set; }

        public int Position { get; set; }
        public double PenaltyPunishment { get; set; }

        public int GetQualifyingResult(int maxRng) => Power + NumberHelper.RandomInt(maxRng);
        public double PenaltyPosition() => Position + PenaltyPunishment;

        public List<QualifyingScore> ToScoreResults(long raceId)
        {
            var scoreResults = new List<QualifyingScore>
            {
                new QualifyingScore
                {
                    Index = 1,
                    Scores = RunValuesQ1,
                    Position = PositionQ1,
                    RaceId = raceId,
                    ResultId = ResultId,
                }
            };

            if (MaxScoreQ2 == 0)
                return scoreResults;

            scoreResults.Add(new QualifyingScore
            {
                Index = 2,
                Scores = RunValuesQ2,
                Position = PositionQ2,
                RaceId = raceId,
                ResultId = ResultId,
            });

            if (MaxScoreQ3 == 0)
                return scoreResults;

            scoreResults.Add(new QualifyingScore
            {
                Index = 3,
                Scores = RunValuesQ3,
                Position = PositionQ3,
                RaceId = raceId,
                ResultId = ResultId,
            });

            return scoreResults;
        }
    }

    public class PracticeDriver : DriverBase
    {
        public int[] RunValues { get; set; }

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunValues.Max();

        public PracticeScore ToScoreResult(long raceId, int num)
            => new() { Index = num, Scores = RunValues, Position = Position, RaceId = raceId, ResultId = ResultId, };
    }
}
