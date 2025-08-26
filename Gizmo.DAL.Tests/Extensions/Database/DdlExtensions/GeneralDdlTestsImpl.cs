using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions;
using SharedLib;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database.DdlExtensions;

public static class GeneralDdlTestsImpl
{
    public static void GenerateBackupName_CreatesCorrectFormat(DefaultDbContext context, DatabaseType dbType)
    {
        var cnmd = context.Database.GetConnectionMetadata();
        var name = context.Database.GenerateBackupName();
        
        var extension = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => ".bak",
            DatabaseType.POSTGRE => ".dump",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported for backup.")
        };
        
        Assert.EndsWith(extension, name);
        Assert.StartsWith(cnmd.DatabaseName, name);
        
        // Verify the name contains a timestamp
        Assert.Contains("_", name);
        
        // Verify the name format: {DatabaseName}_{provider}_{timestamp}.{extension}
        var expectedProvider = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => "mssql",
            DatabaseType.POSTGRE => "pgsql",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
        };
        
        Assert.Contains($"_{expectedProvider}_", name);
    }

    public static async Task GenerateBackupName_CreatesUniqueNames(DefaultDbContext context)
    {
        var name1 = context.Database.GenerateBackupName();

        await Task.Delay(1000); // Simulate waiting for a second

        var name2 = context.Database.GenerateBackupName();
        
        // Names should be different due to timestamp
        Assert.NotEqual(name1, name2);
    }

    public static void GenerateBackupName_HandlesSpecialCharacters(DefaultDbContext context)
    {
        var name = context.Database.GenerateBackupName();
        
        // Verify the name doesn't contain problematic characters for file systems
        Assert.DoesNotContain("/", name);
        Assert.DoesNotContain("\\", name);
        Assert.DoesNotContain(":", name);
        Assert.DoesNotContain("*", name);
        Assert.DoesNotContain("?", name);
        Assert.DoesNotContain("\"", name);
        Assert.DoesNotContain("<", name);
        Assert.DoesNotContain(">", name);
        Assert.DoesNotContain("|", name);
    }

    public static async Task GetNonSystemDbName(DefaultDbContext context)
    {
        var cnmd = context.Database.GetConnectionMetadata();
        var dbNames = await context.Database.GetNonSystemDbNames(CancellationToken.None);

        var count = dbNames.Count();
        var dbName = dbNames.FirstOrDefault(x => x == cnmd.DatabaseName);

        Assert.True(count > 0);
        Assert.Equal(cnmd.DatabaseName, dbName);
    }

    public static async Task ReinitializeDatabase(DefaultDbContext context, DatabaseTestFixture fixture, DatabaseType dbType)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromMinutes(3));
        var config = fixture.GetConfiguration(dbType);

        var exists = await context.Database.Exists(cts.Token);

        Assert.True(exists);

        var backupName = context.Database.GenerateBackupName();
        var backupPath = Path.Combine(config.Backup.Dst, backupName);

        await context.Database.Backup(backupPath, cts.Token);

        await context.Database.Drop(cts.Token);

        exists = await context.Database.Exists(cts.Token);

        Assert.False(exists);

        await context.Database.Restore(backupPath, cts.Token);

        exists = await context.Database.Exists(cts.Token);

        Assert.True(exists);

        var backupVolumeDirectory = Path.GetDirectoryName(config.Backup.Src);

        var backupToRemove = Path.Combine(backupVolumeDirectory!, backupName);

        if (File.Exists(backupToRemove))
        {
            File.Delete(backupToRemove);
        }
    }

    public static async Task TruncateLogs(DefaultDbContext context)
    {
        var rows = await context.Database.TruncateLogs(CancellationToken.None);

        Assert.True(rows >= 0);
    }
}
