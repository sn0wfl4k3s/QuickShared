using Microsoft.Extensions.DependencyInjection;

namespace QuickShared.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossCutting(this IServiceCollection services)
    {
        // Add cross-cutting services here (e.g., logging, caching, etc.)
        return services;
    }
}
