using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNet.Testcontainers.Containers;
using Gizmo.DAL;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Tests.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;
using Xunit;

[CollectionDefinition("DatabaseCollection", DisableParallelization = true)]
public class DatabaseCollection : ICollectionFixture<DatabaseTestFixture>
{
    
}

/// <summary>
/// Disposable current-schema database fixture shared by the DB-backed tests.
/// </summary>
/// <remarks>
/// Each container hosts disposable databases created from the current EF model
/// (<see cref="DefaultDbContext"/>.EnsureCreated) plus the optional <c>ticker</c> schema, which is
/// created through the <see cref="TickerQDbContext"/> relational database creator. No backup files
/// are restored and no host backup directory is bind-mounted, so the harness never touches
/// pre-existing host data.
/// </remarks>
public class DatabaseTestFixture : IAsyncLifetime
{
    private CreationResult<MsSqlContainer>? _sqlServer;
    private CreationResult<PostgreSqlContainer>? _postgreSql;

    public async Task InitializeAsync()
    {
        var sqlServerTask = Container.Start<MsSqlContainer>();
        var postgreSqlTask = Container.Start<PostgreSqlContainer>();

        await Task.WhenAll(sqlServerTask, postgreSqlTask);

        _sqlServer = sqlServerTask.Result;
        _postgreSql = postgreSqlTask.Result;
    }

    public Task DisposeAsync() => Task.WhenAll(
            Container.Stop(_sqlServer?.Container),
            Container.Stop(_postgreSql?.Container)
        );

    public async Task<DefaultDbContext> CreateDbContext(DatabaseType dbType, string dbName, bool withTickerSchema = false)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

        var containerConnectionString = GetConnectionString(dbType);
        await using var containerDbContext = CreateDefaultDbContext(dbType, connectionString: containerConnectionString);
        var nonSystemDbNames = await containerDbContext.Database.GetNonSystemDbNames(ct: cts.Token);

        if (nonSystemDbNames.Contains(dbName))
        {
            // Deterministic reruns: drop a database left behind by an earlier (possibly failed) run.
            var existingMetadata = containerDbContext.Database.GetConnectionMetadata().ChangeDatabaseTo(dbName);
            await using var existingDbContext = CreateDefaultDbContext(dbType, connectionString: existingMetadata.ToConnectionString());
            await existingDbContext.Database.EnsureDeletedAsync(cts.Token);
        }

        var newDbMetadata = containerDbContext.Database.GetConnectionMetadata().ChangeDatabaseTo(dbName);
        var newDbContext = CreateDefaultDbContext(dbType, connectionString: newDbMetadata.ToConnectionString());

        await newDbContext.Database.EnsureCreatedAsync(cts.Token);

        // Seed required default data for tests
        await SeedRequiredTestData(newDbContext, cts.Token);

        if (withTickerSchema)
            await EnsureTickerSchemaAsync(newDbContext, dbType, cts.Token);

        return newDbContext;
    }

    private static DefaultDbContext CreateDefaultDbContext(DatabaseType dbType, string connectionString)
    {
        var builder = new DbContextOptionsBuilder<DefaultDbContext>();

        switch (dbType)
        {
            case DatabaseType.LOCALDB:
            case DatabaseType.MSSQL:
            case DatabaseType.MSSQLEXPRESS:
                builder.UseSqlServer(connectionString);
                break;
            case DatabaseType.POSTGRE:
                builder.UseNpgsql(connectionString);
                break;
            default:
                throw new NotSupportedException($"Database type {dbType} is not supported.");
        }

        return new DefaultDbContext(builder.Options);
    }

    private string GetConnectionString(DatabaseType dbType) => dbType switch
    {
        DatabaseType.LOCALDB or DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS => _sqlServer?.Container.GetConnectionString() ?? throw new InvalidOperationException("SQL Server connection string is not initialized."),
        DatabaseType.POSTGRE => _postgreSql?.Container.GetConnectionString() ?? throw new InvalidOperationException("PostgreSQL connection string is not initialized."),
        _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
    };

    /// <summary>
    /// Creates the <c>ticker</c> schema and its tables in the already created test database.
    /// </summary>
    /// <remarks>
    /// <see cref="DefaultDbContext"/>.EnsureCreated is all-or-nothing per database: once the
    /// database contains any table it skips creation, so the second context cannot add its schema
    /// through it. Instead the relational database creator of <see cref="TickerQDbContext"/> diffs
    /// the current model from an empty schema and emits an <c>EnsureSchema("ticker")</c> operation
    /// followed by the <c>CREATE TABLE</c> commands on both providers, so schema and tables are
    /// created together (the same table set as the TickerQ Initial migration, which has no seed
    /// data).
    /// </remarks>
    private static async Task EnsureTickerSchemaAsync(DefaultDbContext db, DatabaseType dbType, CancellationToken ct)
    {
        var connectionString = db.Database.GetConnectionString()!;

        var tickerOptions = new DbContextOptionsBuilder<TickerQDbContext>();
        switch (dbType)
        {
            case DatabaseType.LOCALDB:
            case DatabaseType.MSSQL:
            case DatabaseType.MSSQLEXPRESS:
                tickerOptions.UseSqlServer(connectionString);
                break;
            case DatabaseType.POSTGRE:
                tickerOptions.UseNpgsql(connectionString);
                break;
            default:
                throw new NotSupportedException($"Database type {dbType} is not supported.");
        }

        await using var tickerContext = new TickerQDbContext(tickerOptions.Options);
        await tickerContext.Database.GetService<IRelationalDatabaseCreator>().CreateTablesAsync(ct);
    }

    private static async Task SeedRequiredTestData(DefaultDbContext context, CancellationToken ct)
    {
        // Create default branch (required for SetBranchAsync to work)
        var defaultBranch = new Branch
        {
            Name = "Test Branch",
            IsDeleted = false,
            IsDisabled = false
        };
        context.Branches.Add(defaultBranch);

        // Create default user group (required for UserMember foreign key)
        var defaultUserGroup = new UserGroup
        {
            Name = "Test Users",
            IsDefault = true
        };
        context.UserGroups.Add(defaultUserGroup);

        // Create default asset type (required for AssetTransaction)
        var defaultAssetType = new AssetType
        {
            Name = "Test Asset Type"
        };
        context.AssetTypes.Add(defaultAssetType);

        await context.SaveChangesAsync(ct);

        // Create default user member (required for AssetTransaction foreign key)
        defaultUserGroup = await context.UserGroups.FirstAsync(ct);
        var defaultUserMember = new UserMember
        {
            Username = "TestUser",
            UserGroupId = defaultUserGroup.Id,
            Email = "test@example.com"
        };
        context.UsersMember.Add(defaultUserMember);
        await context.SaveChangesAsync(ct);

        // Create default asset (required for AssetTransaction foreign key)
        defaultAssetType = await context.AssetTypes.FirstAsync(ct);
        var defaultAsset = new Asset
        {
            Number = 1,
            AssetTypeId = defaultAssetType.Id
        };
        context.Assets.Add(defaultAsset);
        await context.SaveChangesAsync(ct);
    }
}
