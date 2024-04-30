using SimTECH.Data.Models;
using SimTECH.PageModels;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Data.Services.Interfaces;

public interface IRaceService
{
    public Task<List<Race>> GetRacesBySeason(long seasonId);

    public Task<Race> GetRaceById(long raceId);

    public Task<long?> GetRaceIdByRound(long seasonId, int round);

    public Task<long?> GetNextRaceIdOfSeason(long seasonId);

    public Task<Race?> GetNextRaceOfSeason(long seasonId);

    public Task<List<Result>> GetResultsOfRace(long raceId);

    public Task<List<LapScore>> GetLapScores(long raceId);

    public Task<List<QualifyingScore>> GetQualifyingScores(long raceId);

    public Task<List<PracticeScore>> GetPracticeScores(long raceId);

    public Task<List<PracticeScore>> GetPracticeScores(long raceId, int index);

    public Task UpdateRace(Race race);

    public Task InsertRaces(List<Race> races);

    public Task DeleteRace(long raceId);

    public Task ActivateRace(long raceId);

    public Task PickTyre(long resultId, Tyre tyre);

    public Task PersistPracticeScores(List<PracticeScore> practiceScores);

    public Task PersistQualifyingScores(List<QualifyingScore> qualyScores);

    public Task FinishQualifying(List<QualifyingScore> qualyScores, Dictionary<long, (int, int)> driverPositions, long raceToAdvance, int maximumRace);

    public Task<List<RaceOccurrence>> GetRaceOccurrences(long raceId);

    public Task<List<GivenPenalty>> GetRacePenalties(long raceId);

    public Task ConsumePenalties(List<long> consumables, long raceId);

    public Task PersistLapScores(List<LapScore> lapscores);

    public Task PersistOccurrences(List<RaceOccurrence> occurrences);

    public Task FinishRace(long raceId, List<Result> finishedResults, List<ScoredPoints> scoredPoints);

    public Task<List<FinishedRaceModel>> GetRecentlyFinishedCalendarRaces(int amount);
}
