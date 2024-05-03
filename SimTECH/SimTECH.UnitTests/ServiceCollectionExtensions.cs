using Microsoft.Extensions.DependencyInjection;

namespace SimTECH.UnitTests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(e =>
            e.ServiceType == typeof(TService));

        if (descriptor != null)
            services.Remove(descriptor);

        return services;
    }
}
