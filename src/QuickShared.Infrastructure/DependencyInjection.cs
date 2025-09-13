using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QuickShared.Domain.Repositories;
using QuickShared.Domain.Services;
using QuickShared.Infrastructure.Persistance;
using QuickShared.Infrastructure.Persistance.Repositories;
using QuickShared.Infrastructure.Services.Storage;

namespace QuickShared.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();

        services.TryAddTransient<IManagerFileRepository, ManagerFileRepository>();
        services.TryAddTransient<IStorageService, StorageTelegramService>();

        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<AppDbContext>()
            .AddEnvironmentVariables()
            .Build();

        services.Configure<StorageTelegramOptions>(configuration.GetSection("StorageTelegram"));

        services.AddHttpClient();

        return services;
    }
}