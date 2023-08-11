using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MudBlazor;
using MudBlazor.Services;
using MudExtensions.Services;
using SimTECH.Data;
using SimTECH.Data.Services;
using SimTECH.Providers;

namespace SimTECH
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure database context services

            //if (builder.Environment.IsDevelopment())
            //{
            //    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //    builder.Services.AddDbContextFactory<SimTechDbContext>(options =>
            //    {
            //        options.UseSqlServer(connectionString);
            //        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //        options.ConfigureWarnings(e => e.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
            //    });
            //    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            //}
            //else
            //{
            //    var connectionString = builder.Configuration.GetConnectionString("LocalConnection")!.Replace("~", builder.Environment.ContentRootPath);
            //    var connection = new SqliteConnection(connectionString);
            //    builder.Services.AddDbContext<SimTechDbContext, LocalDbContext>(options =>
            //    {
            //        options.UseSqlite(connection);
            //        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //        options.ConfigureWarnings(e => e.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
            //    });
            //}

            builder.Services.AddDbContextFactory<SimTechDbContext>(options =>
            {
                options.UseSqlite($"Data Source={nameof(SimTechDbContext.SimTechDb)}.db");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.ConfigureWarnings(e => e.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
            });

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
            });
            builder.Services.AddMudExtensions();

            // Add configuration
            builder.Services.Configure<SimConfig>(builder.Configuration.GetSection(SimConfig.SectionKey));

            // Provider services
            builder.Services.AddScoped<BreadcrumbProvider>();
            builder.Services.AddScoped<HumanBeingProvider>();

            // Data services
            builder.Services.AddScoped<ContractService>();
            builder.Services.AddScoped<ClimateService>();
            builder.Services.AddScoped<DriverService>();
            builder.Services.AddScoped<EngineService>();
            builder.Services.AddScoped<IncidentService>();
            builder.Services.AddScoped<LeagueService>();
            builder.Services.AddScoped<ManufacturerService>();
            builder.Services.AddScoped<RaceService>();
            builder.Services.AddScoped<ResultService>();
            builder.Services.AddScoped<SeasonService>();
            builder.Services.AddScoped<SeasonEntrantService>();
            builder.Services.AddScoped<TeamService>();
            builder.Services.AddScoped<TrackService>();
            builder.Services.AddScoped<TraitService>();
            builder.Services.AddScoped<TyreService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();

                // We only need the thing underneath whenever we're going to share it as an .exe file
                using var scope = app.Services.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<SimTechDbContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}