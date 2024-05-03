using SimTECH.Data;
using SimTECH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTECH.UnitTests.Extensions;

public static class DataSeedExtensions
{
    /// var platformOrganization = await _dbContext.SeedSomeEntity(
    //      EntityFactory.SomeEntity.Generate());

    public static async Task<Driver> SeedDriver(this SimTechDbContext dbContext, Driver driverSeed)
    {
        dbContext.Driver.Add(driverSeed);
        await dbContext.SaveChangesAsync();

        return driverSeed;
    }

    //public static async Task<List<Tyre>> SeedTyres(this SimTechDbContext dbContext)
    //{
    //    // could use to prefill with predefned tyres?
    //}
}
