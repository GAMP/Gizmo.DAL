using System.Threading.Tasks;
using Gizmo.DAL.Tests.Extensions.Database.DdlExtensions;
using SharedLib;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database;

[Collection("DatabaseCollection")]
public class DdlExtensionsTests(DatabaseTestFixture fixture)
{
    private readonly DatabaseTestFixture _fixture = fixture;

    [Fact]
    public void TryParseBackupTime()
    {
        GeneralDdlTestsImpl.TryParseBackupTime_ReturnsTrue();
        GeneralDdlTestsImpl.TryParseBackupTime_ReturnsFalse();
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task GenerateBackupName(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(GenerateBackupName));
        GeneralDdlTestsImpl.GenerateBackupName_CreatesCorrectFormat(context, dbType);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task GenerateBackupName_CreatesEqualNames(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(GenerateBackupName_CreatesEqualNames));
        await GeneralDdlTestsImpl.GenerateBackupName_CreatesEqualNames(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task GenerateBackupName_HandlesSpecialCharacters(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(GenerateBackupName_HandlesSpecialCharacters));
        GeneralDdlTestsImpl.GenerateBackupName_HandlesSpecialCharacters(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task GetNonSystemDbName(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(GetNonSystemDbName));
        await GeneralDdlTestsImpl.GetNonSystemDbName(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task ReinitializeDatabase(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(ReinitializeDatabase));
        await GeneralDdlTestsImpl.ReinitializeDatabase(context, _fixture, dbType);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task TruncateLogs(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(TruncateLogs));
        await GeneralDdlTestsImpl.TruncateLogs(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task EnsureAdminExists_CreatesNewLogin_WhenNotExists(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(EnsureAdminExists_CreatesNewLogin_WhenNotExists));
        await LoginManagementTestsImpl.EnsureAdminExists_CreatesNewLogin_WhenNotExists(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task EnsureAdminExists_IsIdempotent_WhenLoginAlreadyExists(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(EnsureAdminExists_IsIdempotent_WhenLoginAlreadyExists));
        await LoginManagementTestsImpl.EnsureAdminExists_IsIdempotent_WhenLoginAlreadyExists(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task EnsureAdminExists_EnablesDisabledLogin(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(EnsureAdminExists_EnablesDisabledLogin));
        await LoginManagementTestsImpl.EnsurAdminExists_EnablesDisabledLogin(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task LoginExists_ReturnsCorrectStatus(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(LoginExists_ReturnsCorrectStatus));
        await LoginManagementTestsImpl.LoginExists_ReturnsCorrectStatus(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task HasAdminPermissions_ReturnsCorrectStatus(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(HasAdminPermissions_ReturnsCorrectStatus));
        await LoginManagementTestsImpl.HasAdminPermissions_ReturnsCorrectStatus(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task DisableLogin_DisablesLoginCorrectly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(DisableLogin_DisablesLoginCorrectly));
        await LoginManagementTestsImpl.DisableLogin_DisablesLoginCorrectly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task DropLogin_RemovesLoginCorrectly(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(DropLogin_RemovesLoginCorrectly));
        await LoginManagementTestsImpl.DropLogin_RemovesLoginCorrectly(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task HasAdminPermissions_ReturnsFalseForNonExistentLogin(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(HasAdminPermissions_ReturnsFalseForNonExistentLogin));
        await LoginManagementTestsImpl.HasAdminPermissions_ReturnsFalseForNonExistentLogin(context);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public async Task DisableLogin_HandlesNonExistentLogin(DatabaseType dbType)
    {
        await using var context = await _fixture.CreateDbContext(dbType, nameof(DisableLogin_HandlesNonExistentLogin));
        await LoginManagementTestsImpl.DisableLogin_HandlesNonExistentLogin(context);
    }
}
