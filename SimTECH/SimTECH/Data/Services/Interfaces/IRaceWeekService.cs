using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Data.Services.Interfaces;

public interface IRaceWeekService
{
    public Task<List<Result>> GetResultsOfRace(long raceId);

    public Task<List<LapScore>> GetLapScores(long raceId);

    public Task<List<QualifyingScore>> GetQualifyingScores(long raceId);

    public Task<List<PracticeScore>> GetPracticeScores(long raceId);

    public Task<List<PracticeScore>> GetPracticeScores(long raceId, int index);

    public Task<List<Tyre>> GetValidTyresForRace(long leagueId);

    public Task PickTyre(long resultId, Tyre tyre);

    public Task PersistPracticeScores(List<PracticeScore> practiceScores);

    public Task PersistQualifyingScores(List<QualifyingScore> qualyScores);

    public Task FinishQualifying(List<QualifyingScore> qualyScores, Dictionary<long, (int, int)> driverPositions, long raceToAdvance, int maximumRace);

    public Task<List<RaceOccurrence>> GetRaceOccurrences(long raceId);

    public Task<List<GivenPenalty>> GetRacePenalties(long raceId);

    public Task ConsumePenalties(List<long> consumables, long raceId);

    public Task CheckPenalties(List<Result> raceResults);

    public Task PersistLapScores(List<LapScore> lapscores);

    public Task PersistOccurrences(List<RaceOccurrence> occurrences);

    public Task FinishRace(long raceId, List<Result> finishedResults, List<ScoredPoints> scoredPoints);
}
