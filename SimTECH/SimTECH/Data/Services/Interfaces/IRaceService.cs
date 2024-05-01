using SimTECH.Data.Models;
using SimTECH.PageModels;

namespace SimTECH.Data.Services.Interfaces;

public interface IRaceService
{
    public Task<List<Race>> GetRacesBySeason(long seasonId);

    public Task<Race> GetRaceById(long raceId);

    public Task<long?> GetRaceIdByRound(long seasonId, int round);

    public Task<long?> GetNextRaceIdOfSeason(long seasonId);

    public Task<Race?> GetNextRaceOfSeason(long seasonId);

    public Task UpdateRace(Race race);

    public Task InsertRaces(List<Race> races);

    public Task DeleteRace(long raceId);

    public Task ActivateRace(long raceId);

    public Task<List<FinishedRaceModel>> GetRecentlyFinishedCalendarRaces(int amount);
}
