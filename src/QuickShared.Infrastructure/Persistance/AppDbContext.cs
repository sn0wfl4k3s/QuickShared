using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickShared.Domain.Entities;
using System.Reflection;

namespace QuickShared.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext() : base(BuildOptions().Options) { }
    public AppDbContext(DbContextOptions<AppDbContext> _) : base(BuildOptions().Options) { }

    public virtual DbSet<ManagerFile> ManagerFiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("quickshared");
        modelBuilder.HasPostgresExtension("pg_trgm");
        modelBuilder.HasPostgresExtension("unaccent");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(BuildOptions());
        }
    }

    internal static DbContextOptionsBuilder<AppDbContext> BuildOptions()
    {
        var configuration = new ConfigurationBuilder()
                .AddUserSecrets<AppDbContext>()
                .AddEnvironmentVariables()
                .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("Postgres"),
            npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "quickshared");
            });

        return optionsBuilder;
    }
}
