using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TickerQ.EntityFrameworkCore.Configurations;
using TickerQ.Utilities.Entities;
using Microsoft.Extensions.Configuration;

namespace Gizmo.DAL.Contexts;

/// <summary>
/// <see cref="DbContext"/> for accessing the TickerQ database.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TickerQDbContext"/> class.
/// </remarks>
public sealed class TickerQDbContext(DbContextOptions<TickerQDbContext> options) : DbContext(options)
{
    internal const string DEFAULT_SCHEMA = "ticker";

    /// <summary>
    /// Gets or sets the cron tickers.
    /// </summary>
    public DbSet<CronTickerEntity> CronTickers { get; set; }

    /// <summary>
    /// Gets or sets the time tickers.
    /// </summary>
    public DbSet<TimeTickerEntity> TimeTickers { get; set; }
    
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TimeTickerConfigurations<TimeTickerEntity>(schema: DEFAULT_SCHEMA));
        builder.ApplyConfiguration(new CronTickerConfigurations<CronTickerEntity>(schema: DEFAULT_SCHEMA));
        builder.ApplyConfiguration(new CronTickerOccurrenceConfigurations<CronTickerEntity>(schema: DEFAULT_SCHEMA));
    }
}

/// <summary>
/// Design-time factory for creating <see cref="TickerQDbContext"/> instances.
/// </summary>
public sealed class TickerQDbContextFactory : IDesignTimeDbContextFactory<TickerQDbContext>
{
    /// <summary>
    /// Creates a <see cref="TickerQDbContext"/> for design-time operations.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the factory.</param>
    /// <returns>A configured <see cref="TickerQDbContext"/> instance.</returns>
    public TickerQDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("service.json", optional: true, reloadOnChange: false)
           .Build();

        var dbConfig = config.GetRequiredSection("Service:Database").Get<ServiceDatabaseConfig>();

        // Since we are using IDesignTimeDbContextFactory, we need to create options manually.
        // Another option would be using the Startup project to provide the configuration but since the migrations wont be generated often we can stick to this dirty approach for now.

        //var dbConfig = new ServiceDatabaseConfig() { DbType = DatabaseType.MSSQL, DbConnectionString = @"Server=LOCALHOST\SQLEXPRESS;Initial Catalog=_gizmo_db;Integrated Security=true;TrustServerCertificate=true" };
        //var dbConfig = new ServiceDatabaseConfig() {  DbType = SharedLib.DatabaseType.POSTGRE, DbConnectionString = "Server=localhost;Database=_gizmo_db;User Id=postgres;Password=password" };

        var optionsBuilder = new DbContextOptionsBuilder<TickerQDbContext>();
        optionsBuilder.Configure(dbConfig);

        return new TickerQDbContext(optionsBuilder.Options);
    }
}

/// <summary>
/// Extension methods for configuring <see cref="TickerQDbContext"/> options.
/// </summary>
public static class TickerQDbContextExtensions
{
    const string MIGRATIONS_TABLE = "__EFMigrationsHistory";
    const string MIGRATIONS_ASSEMBLY_MSSQL = "Gizmo.DAL.Migrations.TickerQ.MSSQL";
    const string MIGRATIONS_ASSEMBLY_POSTGRE = "Gizmo.DAL.Migrations.TickerQ.Npgsql";

    /// <summary>
    /// Configures the <see cref="DbContextOptionsBuilder"/> for <see cref="TickerQDbContext"/> based on the provided <see cref="ServiceDatabaseConfig"/>.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to configure.</param>
    /// <param name="dbConfig">The database configuration.</param>
    /// <exception cref="NotSupportedException">Thrown when the database type is not supported.</exception>
    public static void Configure(this DbContextOptionsBuilder optionsBuilder, ServiceDatabaseConfig dbConfig)
    {
        var dbType = dbConfig.DbType;
        var connectionString = dbConfig.DbConnectionString;

        switch (dbType)
        {
            case DatabaseType.LOCALDB:
            case DatabaseType.MSSQLEXPRESS:
            case DatabaseType.MSSQL:
                optionsBuilder.UseSqlServer(connectionString, options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, TickerQDbContext.DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY_MSSQL);
                });
                break;
            case DatabaseType.POSTGRE:
                optionsBuilder.UseNpgsql(connectionString, options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, TickerQDbContext.DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY_POSTGRE);
                });
                break;
            default:
                throw new NotSupportedException("Database type not supported.");
        }
    }
}
