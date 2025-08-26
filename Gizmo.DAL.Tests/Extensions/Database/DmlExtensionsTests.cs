using System.Threading.Tasks;
using Gizmo.DAL.Tests.Extensions.Database.DmlExtensions;
using SharedLib;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database;

public class DmlExtensionsTests(DatabaseTestFixture fixture) : IClassFixture<DatabaseTestFixture>
{
    private readonly DatabaseTestFixture _fixture = fixture;

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_All(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_All), useBackup: true);
        await CleanupTestsImpl.Cleanup_All(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task CleanupUsers(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(CleanupUsers), useBackup: true);
        await CleanupUsersTestsImpl.CleanupUsers_Basic(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task CleanupUsers_OnlyRemovesDeletedUsers(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(CleanupUsers_OnlyRemovesDeletedUsers));
        await CleanupUsersTestsImpl.CleanupUsers_OnlyRemovesDeletedUsers(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_DeleteProductsOnly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_DeleteProductsOnly), useBackup: true);
        await CleanupTestsImpl.Cleanup_DeleteProductsOnly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_DeleteHostsOnly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_DeleteHostsOnly), useBackup: true);
        await CleanupTestsImpl.Cleanup_DeleteHostsOnly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_DeleteOperatorsOnly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_DeleteOperatorsOnly), useBackup: true);
        await CleanupTestsImpl.Cleanup_DeleteOperatorsOnly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_DeleteUsersOnly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_DeleteUsersOnly), useBackup: true);
        await CleanupTestsImpl.Cleanup_DeleteUsersOnly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_HandlesEmptyDatabase(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_HandlesEmptyDatabase), useBackup: true);
        await CleanupTestsImpl.Cleanup_HandlesEmptyDatabase(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_HandlesReservedHostReferences(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_HandlesReservedHostReferences));
        await CleanupTestsImpl.Cleanup_HandlesReservedHostReferences(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_RespectsCancellationToken(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_RespectsCancellationToken));
        await CleanupTestsImpl.Cleanup_RespectsCancellationToken(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_CreatesDefaultAdminOperator(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_CreatesDefaultAdminOperator));
        await CleanupTestsImpl.Cleanup_CreatesDefaultAdminOperator(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_DeleteAllFalse(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_DeleteAllFalse), useBackup: true);
        await CleanupTestsImpl.Cleanup_DeleteAllFalse(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task Cleanup_HandlesAssetTransactionCheckedInBy(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_HandlesAssetTransactionCheckedInBy));
        await CleanupTestsImpl.Cleanup_HandlesAssetTransactionCheckedInBy(context);
    }
}
