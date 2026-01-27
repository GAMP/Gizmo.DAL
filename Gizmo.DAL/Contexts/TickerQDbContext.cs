using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SharedLib.Configuration;
using TickerQ.EntityFrameworkCore.Configurations;
using TickerQ.EntityFrameworkCore.Entities;

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
    const string MIGRATIONS_ASSEMBLY_MSSQL = "Gizmo.DAL.Migrations.TickerQ.MSSQL";
    const string MIGRATIONS_ASSEMBLY_POSTGRE = "Gizmo.DAL.Migrations.TickerQ.Npgsql";

    /// <summary>
    /// Gets or sets the cron tickers.
    /// </summary>
    public DbSet<CronTickerEntity> CronTickers { get; set; }

    /// <summary>
    /// Gets or sets the time tickers.
    /// </summary>
    public DbSet<TimeTickerEntity> TimeTickers { get; set; }
    
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
            case DatabaseType.LOCALDB:
            case DatabaseType.MSSQLEXPRESS:
            case DatabaseType.MSSQL:
                builder.UseSqlServer(_dbConfig.DbConnectionString, options =>
                {
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY_MSSQL);
                });
                break;
            case DatabaseType.POSTGRE:
                builder.UseNpgsql(_dbConfig.DbConnectionString, options =>
                {
                    options.MigrationsHistoryTable(MIGRATIONS_TABLE, DEFAULT_SCHEMA);
                    options.MigrationsAssembly(MIGRATIONS_ASSEMBLY_POSTGRE);
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
        //var config = new ConfigurationBuilder()
        //    .SetBasePath(AppContext.BaseDirectory)
        //    .AddJsonFile("service.json", optional: true, reloadOnChange: false)
        //    .Build();

        //var options = Options.Create(config.GetRequiredSection("Service:Database").Get<ServiceDatabaseConfig>());

        // Since we are using IDesignTimeDbContextFactory, we need to create options manually.
        // Another option would be using the Startup project to provide the configuration but since the migrations wont be generated often we can stick to this dirty approach for now.

        var options = Options.Create(new ServiceDatabaseConfig() { DbType = DatabaseType.MSSQL, DbConnectionString = @"Server=LOCALHOST\SQLEXPRESS;Initial Catalog=_gizmo_db;Integrated Security=true;TrustServerCertificate=true" });
        //var options = Options.Create(new ServiceDatabaseConfig() {  DbType = SharedLib.DatabaseType.POSTGRE, DbConnectionString = "Server=localhost;Database=_gizmo_db;User Id=postgres;Password=password" });

        return new TickerQDbContext(options);
    }
}
