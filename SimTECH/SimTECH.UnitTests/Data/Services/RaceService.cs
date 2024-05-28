using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;
using SimTECH.PageModels;

namespace SimTECH.UnitTests.Data.Services;

public class RaceService : IRaceService
{
    public Task ActivateRace(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRace(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<long?> GetNextRaceIdOfSeason(long seasonId)
    {
        throw new NotImplementedException();
    }

    public Task<Race?> GetNextRaceOfSeason(long seasonId)
    {
        throw new NotImplementedException();
    }

    public Task<Race> GetRaceById(long raceId)
    {
        throw new NotImplementedException();
    }

    public Task<long?> GetRaceIdByRound(long seasonId, int round)
    {
        throw new NotImplementedException();
    }

    public Task<List<Race>> GetRacesBySeason(long seasonId)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinishedRaceModel>> GetRecentlyFinishedCalendarRaces(int amount)
    {
        throw new NotImplementedException();
    }

    public Task InsertRaces(List<Race> races)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRace(Race race)
    {
        throw new NotImplementedException();
    }
}
