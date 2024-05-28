using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimTECH.Data;
using SimTECH.Data.Models;
using SimTECH.UnitTests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace SimTECH.UnitTests.Infrastructure;

// Check the following sources
// https://testcontainers.com/guides/getting-started-with-testcontainers-for-dotnet/
// https://blog.jetbrains.com/dotnet/2023/10/24/how-to-use-testcontainers-with-dotnet-unit-tests

public class DataFixture : WebApplicationFactory<ISimMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("MSSQL_SA_PASSWORD", "Password1")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPassword("Password1")
            .Build();

    private DbContextOptions<SimTechDbContext>? _dbContextOptions;
    private SimTechDbContext? _dbContext;

    public virtual async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var options = new DbContextOptionsBuilder<SimTechDbContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;

        _dbContextOptions = options;

        await using var context = new SimTechDbContext(options);
        await context.Database.MigrateAsync();

        var testDriver = new Driver()
        {
            FirstName = "Max",
            LastName = "Verstappen",
            Abbreviation = "VER",
            DateOfBirth = DateTime.Now,
            Country = Common.Enums.Country.NL,
            Biography = "why do we need one",
            Mark = true,
        };

        await context.Driver.AddAsync(testDriver);
        await context.SaveChangesAsync();
    }

    public new virtual async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        builder.ConfigureTestServices(services =>
        {
            // Yoinked this, may need some adaptions to make it work
            services
                .Remove<DbContextOptions<SimTechDbContext>>()
                .AddDbContext<SimTechDbContext>((sp, options) =>
                    options.UseSqlServer(_dbContainer.GetConnectionString(),
                    o => o.MigrationsAssembly(typeof(SimTechDbContext).Assembly.FullName)));

            services.AddSimTechServices();
        });
    }

    public SimTechDbContext GetDbContext()
    {
        if (_dbContext == null)
            return new SimTechDbContext(_dbContextOptions!);

        return _dbContext;
    }
}
