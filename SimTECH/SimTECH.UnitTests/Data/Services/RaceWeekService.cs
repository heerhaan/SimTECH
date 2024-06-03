using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;
using SimTECH.PageModels.RaceWeek;
using SimTECH.UnitTests.Data.Factories;

namespace SimTECH.UnitTests.Data.Services;

public class RaceWeekService : IRaceWeekService
{
    public Task CheckPenalties(List<Result> raceResults)
    {
        throw new NotImplementedException();
    }

    public Task ConsumePenalties(List<long> consumables, long raceId)
    {
        throw new NotImplementedException();
    }

    public Task FinishQualifying(List<QualifyingScore> qualyScores, Dictionary<long, (int, int)> driverPositions, long raceToAdvance, int maximumRace)
    {
        throw new NotImplementedException();
    }

    public Task FinishRace(long raceId, List<Result> finishedResults, List<ScoredPoints> scoredPoints)
    {
        throw new NotImplementedException();
    }

    public Task<List<LapScore>> GetLapScores(long raceId)
    {
        var listScores = new List<LapScore>();

        return Task.FromResult(listScores);
    }

    public Task<List<PracticeScore>> GetPracticeScores(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<PracticeScore>> GetPracticeScores(long raceId, int index)
    {
        throw new NotImplementedException();
    }

    public Task<List<QualifyingScore>> GetQualifyingScores(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RaceOccurrence>> GetRaceOccurrences(long raceId)
    {
        var items = new List<RaceOccurrence>();

        return Task.FromResult(items);
    }

    public Task<List<GivenPenalty>> GetRacePenalties(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Result>> GetResultsOfRace(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Tyre>> GetValidTyresForRace(long leagueId)
    {
        var tyres = TyreFactory.GenerateDefaultListTyres;

        return Task.FromResult(tyres);
    }

    public Task PersistLapScores(List<LapScore> lapscores)
    {
        throw new NotImplementedException();
    }

    public Task PersistOccurrences(List<RaceOccurrence> occurrences)
    {
        throw new NotImplementedException();
    }

    public Task PersistPracticeScores(List<PracticeScore> practiceScores)
    {
        throw new NotImplementedException();
    }

    public Task PersistQualifyingScores(List<QualifyingScore> qualyScores)
    {
        throw new NotImplementedException();
    }

    public Task PickTyre(long resultId, Tyre tyre)
    {
        throw new NotImplementedException();
    }
}
