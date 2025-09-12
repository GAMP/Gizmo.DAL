using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SharedLib.Configuration;
using TickerQ.EntityFrameworkCore.Configurations;

namespace Gizmo.DAL.Contexts;

/// <summary>
/// <see cref="DbContext"/> for accessing the TickerQ database.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TickerQDbContext"/> class.
/// </remarks>
/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
public sealed class TickerQDbContext(IOptions<ServiceDatabaseConfig> options) : DbContext
{
    const string DEFAULT_SCHEMA = "ticker";
    const string MIGRATIONS_TABLE = "__EFMigrationsHistory";
    const string MIGRATIONS_ASSEMBLY = "Gizmo.DAL.Migrations.TickerQ";

    private readonly ServiceDatabaseConfig _dbConfig = options.Value;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TimeTickerConfigurations(schema: DEFAULT_SCHEMA));
        builder.ApplyConfiguration(new CronTickerConfigurations(schema: DEFAULT_SCHEMA));
        builder.ApplyConfiguration(new CronTickerOccurrenceConfigurations(schema: DEFAULT_SCHEMA));
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        switch (_dbConfig.DbType)
        {
            case SharedLib.DatabaseType.LOCALDB:
            case SharedLib.DatabaseType.MSSQLEXPRESS:
            case SharedLib.DatabaseType.MSSQL:
                builder.UseSqlServer(_dbConfig.DbConnectionString, options =>
                {
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY);
                });
                break;
            case SharedLib.DatabaseType.POSTGRE:
                builder.UseNpgsql(_dbConfig.DbConnectionString, options =>
                {
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY);
                });
                break;
            default:
                throw new NotSupportedException("Database type not supported.");
        }
    }
}

/// <inheritdoc/>
public sealed class TickerQDbContextFactory : IDesignTimeDbContextFactory<TickerQDbContext>
{
    /// <inheritdoc/>
    public TickerQDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("service.json", optional: true, reloadOnChange: false)
            .Build();

        var options = Options.Create(config.GetRequiredSection("Service:Database").Get<ServiceDatabaseConfig>());

        return new TickerQDbContext(options);
    }
}