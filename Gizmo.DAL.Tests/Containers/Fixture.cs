using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNet.Testcontainers.Containers;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Tests.Containers;
using Microsoft.EntityFrameworkCore;
using SharedLib;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;
using Xunit;

[CollectionDefinition("DatabaseCollection", DisableParallelization = true)]
public class DatabaseCollection : ICollectionFixture<DatabaseTestFixture>
{
    
}

public class DatabaseTestFixture : IAsyncLifetime
{
    private CreationResult<MsSqlContainer>? _sqlServer;
    private CreationResult<PostgreSqlContainer>? _postgreSql;
    private readonly string _restoredDbName = Guid.NewGuid().ToString("N");

    public async Task InitializeAsync()
    {
        var sqlServerTask = Container.Start<MsSqlContainer>();
        var postgreSqlTask = Container.Start<PostgreSqlContainer>();

        await Task.WhenAll(sqlServerTask, postgreSqlTask);

        _sqlServer = await TryRestoreDatabase(sqlServerTask.Result);
        _postgreSql = await TryRestoreDatabase(postgreSqlTask.Result);
    }

    public Task DisposeAsync() => Task.WhenAll(
            Container.Stop(_sqlServer?.Container),
            Container.Stop(_postgreSql?.Container)
        );

    public Configuration GetConfiguration(DatabaseType dbType)
    {
        return dbType switch
        {
            DatabaseType.LOCALDB or DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS => _sqlServer?.Config ?? throw new InvalidOperationException("SQL Server configuration is not initialized."),
            DatabaseType.POSTGRE => _postgreSql?.Config ?? throw new InvalidOperationException("PostgreSQL configuration is not initialized."),
            _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
        };
    }

    public async Task<DefaultDbContext> CreateDbContext(DatabaseType dbType, string dbName, bool useBackup = false)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(3));

        var containerConnectionString = GetConnectionString(dbType);
        await using var containerDbContext = CreateDefaultDbContext(dbType, connectionString: containerConnectionString);
        var nonSystemDbNames = await containerDbContext.Database.GetNonSystemDbNames(ct: cts.Token);

        if (nonSystemDbNames.Contains(dbName))
        {
            var existingMetadata = containerDbContext.Database.GetConnectionMetadata().ChangeDatabaseTo(dbName);
            var existingDbContext = CreateDefaultDbContext(dbType, connectionString: existingMetadata.ToConnectionString());
            return existingDbContext;
        }

        var newDbMetadata = containerDbContext.Database.GetConnectionMetadata().ChangeDatabaseTo(dbName);
        var newDbContext = CreateDefaultDbContext(dbType, connectionString: newDbMetadata.ToConnectionString());

        if (!useBackup)
        {
            await newDbContext.Database.EnsureCreatedAsync(cts.Token);

            // Seed required default data for tests
            await SeedRequiredTestData(newDbContext, cts.Token);

            return newDbContext;
        }

        var restoredMetadata = containerDbContext.Database.GetConnectionMetadata().ChangeDatabaseTo(_restoredDbName);
        await using var restoredDbContext = CreateDefaultDbContext(dbType, connectionString: restoredMetadata.ToConnectionString());

        if (!await restoredDbContext.Database.Exists(ct: cts.Token))
            throw new InvalidOperationException($"Database '{dbName}' does not exist and no backup is available to restore.");

        var config = GetConfiguration(dbType);
        var backupName = newDbContext.Database.GenerateBackupName();
        var backupPath = Path.Combine(config.Backup.Dst, backupName);

        await restoredDbContext.Database.Backup(backupPath, cts.Token);
        await newDbContext.Database.Restore(backupPath, cts.Token);

        var backupVolumeDirectory = Path.GetDirectoryName(config.Backup.Src);

        if (backupVolumeDirectory is not null)
        {
            var backupToRemove = Path.Combine(backupVolumeDirectory, backupName);

            if (System.IO.File.Exists(backupToRemove))
            {
                System.IO.File.Delete(backupToRemove);
            }
        }

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

    private async Task<CreationResult<T>> TryRestoreDatabase<T>(CreationResult<T> result) where T : DockerContainer, IDatabaseContainer
    {
        if (!string.IsNullOrWhiteSpace(result.Config.Backup.Src) && !string.IsNullOrWhiteSpace(result.Config.Backup.Dst))
        {
            var cs = result.Container.GetConnectionString();

            var dbtype = typeof(T) switch
            {
                Type t when t == typeof(MsSqlContainer) => DatabaseType.MSSQL,
                Type t when t == typeof(PostgreSqlContainer) => DatabaseType.POSTGRE,
                _ => throw new NotSupportedException($"Database type {typeof(T).Name} is not supported for restore.")
            };

            var backupMetadata = DclOperations.CreateConnectionMetadata(dbtype, cs).ChangeDatabaseTo(_restoredDbName);
            await using var dbContext = CreateDefaultDbContext(dbtype, backupMetadata.ToConnectionString());

            var backupFileName = Path.GetFileName(result.Config.Backup.Src);
            var backupFilePath = Path.Combine(result.Config.Backup.Dst, backupFileName);
            await dbContext.Database.Restore(backupFilePath);
        }

        return result;
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
