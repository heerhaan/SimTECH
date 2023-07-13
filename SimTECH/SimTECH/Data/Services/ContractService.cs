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

        public async Task<List<Contract>> GetContracts() => await GetContracts(false);
        public async Task<List<Contract>> GetContracts(bool expired)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Contract
                .Where(e => (e.Duration == 0) == expired)
                .ToListAsync();
        }

        public async Task<List<Contract>> GetExtendedContracts() => await GetExtendedContracts(false);
        public async Task<List<Contract>> GetExtendedContracts(bool expired)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Contract
                .Where(e => (e.Duration == 0) == expired)
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
    }
}
