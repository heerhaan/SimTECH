using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using SimTECH.Data.Services.Interfaces;
using SimTECH.Tests.Data.Services;

namespace SimTECH.Tests.Extensions;

public static class TestContextExtensions
{
    public static void AddTestServices(this TestContext context)
    {
        context.JSInterop.Mode = JSRuntimeMode.Loose;

        context.Services.AddMudServices(options =>
        {
            options.SnackbarConfiguration.ShowTransitionDuration = 0;
            options.SnackbarConfiguration.HideTransitionDuration = 0;
        });

        // Probably need interface to make this properly work
        context.Services.AddScoped<IIncidentService, IncidentService>();
        context.Services.AddScoped<IRaceService, RaceService>();
        context.Services.AddScoped<IRaceWeekService, RaceWeekService>();
    }

    // NOTE: following is unused, I assume that this is an "other" way to inject services
    //public static void AddSimTechServices(this IServiceCollection services)
    //{
    //    services.AddMudServices(options =>
    //    {
    //        options.SnackbarConfiguration.ShowTransitionDuration = 0;
    //        options.SnackbarConfiguration.HideTransitionDuration = 0;
    //    });

    //    // NOTE: Doesn't seem like these services can be found this way?
    //    services.AddScoped<IIncidentService, IncidentService>();
    //    services.AddScoped<IRaceService, RaceService>();
    //    services.AddScoped<IRaceWeekService, RaceWeekService>();
    //}
}
