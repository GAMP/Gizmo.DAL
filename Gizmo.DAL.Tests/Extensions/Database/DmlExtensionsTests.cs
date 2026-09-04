using System.Threading.Tasks;
using Gizmo.DAL;
using Gizmo.DAL.Tests.Extensions.Database.DmlExtensions;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database;

[Collection("DatabaseCollection")]
public class DmlExtensionsTests(DatabaseTestFixture fixture)
{
    private readonly DatabaseTestFixture _fixture = fixture;

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task CleanupUsers(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(CleanupUsers));
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
    public async Task Cleanup_HandlesEmptyDatabase(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_HandlesEmptyDatabase));
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
    public async Task Cleanup_HandlesAssetTransactionCheckedInBy(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(Cleanup_HandlesAssetTransactionCheckedInBy));
        await CleanupTestsImpl.Cleanup_HandlesAssetTransactionCheckedInBy(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task FullReset_RemovesAchievementCompletionRewardsBeforeFinancialParents(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(FullReset_RemovesAchievementCompletionRewardsBeforeFinancialParents)}_{dbType}");
        await CleanupTestsImpl.FullReset_RemovesAchievementCompletionRewardsBeforeFinancialParents(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task FullReset_ResetsTickerQIncludingSelfReference(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(FullReset_ResetsTickerQIncludingSelfReference)}_{dbType}", withTickerSchema: true);
        await CleanupTestsImpl.FullReset_ResetsTickerQIncludingSelfReference(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task FullReset_SucceedsWhenTickerSchemaAbsent(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(FullReset_SucceedsWhenTickerSchemaAbsent)}_{dbType}");
        await CleanupTestsImpl.FullReset_SucceedsWhenTickerSchemaAbsent(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task ProductsOnlyCleanup_RemovesAchievementProductDependencies(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(ProductsOnlyCleanup_RemovesAchievementProductDependencies)}_{dbType}");
        await CleanupTestsImpl.ProductsOnlyCleanup_RemovesAchievementProductDependencies(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task HostsOnlyCleanup_RemovesAchievementHostDependencies(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(HostsOnlyCleanup_RemovesAchievementHostDependencies)}_{dbType}");
        await CleanupTestsImpl.HostsOnlyCleanup_RemovesAchievementHostDependencies(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task UsersHardDelete_RemovesAchievementHistoryAndUser(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(UsersHardDelete_RemovesAchievementHistoryAndUser)}_{dbType}");
        await CleanupUsersTestsImpl.UsersHardDelete_RemovesAchievementHistoryAndUser(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task CleanupUsers_RemovesSoftDeletedUserAchievementDependencies(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(CleanupUsers_RemovesSoftDeletedUserAchievementDependencies)}_{dbType}");
        await CleanupUsersTestsImpl.CleanupUsers_RemovesSoftDeletedUserAchievementDependencies(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task ProductsOnlyCleanup_RemovesTptBaseRowsForProductsAndPreservesUnrelatedConfiguration(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(ProductsOnlyCleanup_RemovesTptBaseRowsForProductsAndPreservesUnrelatedConfiguration)}_{dbType}");
        await CleanupTestsImpl.ProductsOnlyCleanup_RemovesTptBaseRowsForProductsAndPreservesUnrelatedConfiguration(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task HostsOnlyCleanup_RemovesTptBaseRowsForHostsAndPreservesUnrelatedConfiguration(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, $"{nameof(HostsOnlyCleanup_RemovesTptBaseRowsForHostsAndPreservesUnrelatedConfiguration)}_{dbType}");
        await CleanupTestsImpl.HostsOnlyCleanup_RemovesTptBaseRowsForHostsAndPreservesUnrelatedConfiguration(context);
    }
}
