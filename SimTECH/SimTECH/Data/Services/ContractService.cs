using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class ContractService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public ContractService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Contract>> GetExtendedLeagueContracts(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Contract
                .Where(e => e.LeagueId == leagueId)
                .Include(e => e.Driver)
                .Include(e => e.Team)
                .Include(e => e.League)
                .ToListAsync();
        }

        public async Task<List<Contract>> GetLeagueContracts(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Contract
                .Where(e => e.Duration != 0 && e.LeagueId == leagueId)
                .ToListAsync();
        }

        public async Task UpdateContract(Contract contract)
        {
            using var context = _dbFactory.CreateDbContext();

            if (contract.Id == 0)
                context.Add(contract);
            else
                context.Update(contract);

            await context.SaveChangesAsync();
        }

        public async Task AddContracts(List<Contract> contracts)
        {
            using var context = _dbFactory.CreateDbContext();

            // Validations to check if there are no running contracts?

            context.AddRange(contracts);

            await context.SaveChangesAsync();
        }

        public async Task SubtractDurations(long leagueId, long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var league = await context.League.FirstAsync(e => e.Id == leagueId);

            if (league.Options.HasFlag(LeagueOptions.AllowContracting))
            {
                var driversInSeason = await context.SeasonDriver.Where(e => e.SeasonId == seasonId).Select(e => e.DriverId).ToListAsync();

                var leagueContracts = await context.Contract
                    .Where(e => e.LeagueId == leagueId && e.Duration > 0 && driversInSeason.Contains(e.DriverId))
                    .ExecuteUpdateAsync(e =>
                        e.SetProperty(prop => prop.Duration, prop => prop.Duration - 1));
            }
        }
    }
}
