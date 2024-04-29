using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using SimTECH.Data.Services.Interfaces;
using SimTECH.UnitTests.Data.Services;

namespace SimTECH.UnitTests.Extensions;

public static class TestContextExtensions
{
    public static void AddTestServices(this Bunit.TestContext context)
    {
        context.Services.AddMudServices(options =>
        {
            options.SnackbarConfiguration.ShowTransitionDuration = 0;
            options.SnackbarConfiguration.HideTransitionDuration = 0;
        });

        // Probably need interface to make this properly work
        context.Services.AddScoped<IIncidentService, IncidentService>();
    }
}
