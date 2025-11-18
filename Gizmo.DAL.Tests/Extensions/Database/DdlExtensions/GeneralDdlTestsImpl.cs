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
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => ".BAK",
            DatabaseType.POSTGRE => ".DUMP",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported for backup.")
        };

        Assert.EndsWith(extension, name);
        Assert.Contains(cnmd.DatabaseName, name);

        // Verify the name contains a timestamp
        Assert.Contains("_", name);

        // Verify the name format: {DatabaseName}_{provider}_{timestamp}.{extension}
        var expectedProvider = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => "MSSQL",
            DatabaseType.POSTGRE => "PGSQL",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
        };

        Assert.Contains($"_{expectedProvider}_", name);
    }

    public static async Task GenerateBackupName_CreatesEqualNames(DefaultDbContext context)
    {
        var now = DateTime.UtcNow;

        var name1 = context.Database.GenerateBackupName(now);

        var name2 = context.Database.GenerateBackupName(now);

        var name3 = context.Database.GenerateBackupName();

        var name4 = context.Database.GenerateBackupName();

        // Names should be equal since the same timestamp is used
        Assert.Equal(name1, name2);
        Assert.Equal(name3, name4);
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

    public static void GenerateTemporaryBackupName_CreatesCorrectFormat(DefaultDbContext context, DatabaseType dbType)
    {
        var name = context.Database.GenerateTemporaryBackupName();

        var extension = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => ".BAK",
            DatabaseType.POSTGRE => ".DUMP",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported for backup.")
        };

        // Verify the name ends with the correct extension
        Assert.EndsWith(extension, name);

        // Verify the name format: {randomPart}.{extension}
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);
        
        // Should be 8 characters (GUID substring)
        Assert.Equal(8, nameWithoutExtension.Length);
        
        // Should be all hex characters
        Assert.Matches("^[0-9a-f]{8}$", nameWithoutExtension);
    }

    public static void GenerateTemporaryBackupName_CreatesUniqueNames(DefaultDbContext context)
    {
        var name1 = context.Database.GenerateTemporaryBackupName();
        var name2 = context.Database.GenerateTemporaryBackupName();
        var name3 = context.Database.GenerateTemporaryBackupName();

        // All generated names should be unique
        Assert.NotEqual(name1, name2);
        Assert.NotEqual(name1, name3);
        Assert.NotEqual(name2, name3);
    }
    
    public static void TryParseBackupTime_ReturnsTrue()
    {
        // Arrange
        var testCases = new[]
        {
            ("BACKUP_MSSQL_MyDatabase_2025_11_13_14_30", new DateTime(2025, 11, 13, 14, 30, 0, DateTimeKind.Utc)),
            ("BACKUP_MSSQL_MyDatabase_2025_11_13_14_30.BAK", new DateTime(2025, 11, 13, 14, 30, 0, DateTimeKind.Utc)),
            ("BACKUP_PGSQLMyDatabase_2025_01_01_00_00.DUMP", new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)),
            ("BACKUP_MYSQL_MyDatabase_2024_12_31_23_59.SQL", new DateTime(2024, 12, 31, 23, 59, 0, DateTimeKind.Utc))
        };

        foreach (var (backupName, expectedDateTime) in testCases)
        {
            // Act
            var result = DdlOperations.TryParseBackupTime(backupName, out var backupTime);

            // Assert
            Assert.True(result, $"Failed to parse: {backupName}");
            Assert.Equal(expectedDateTime, backupTime);
        }
    }

    public static void TryParseBackupTime_ReturnsFalse()
    {
        // Arrange
        var invalidBackupNames = new[]
        {
            null,
            "   ",
            string.Empty,
            "Short_2025", // Too short
            "InvalidBackupName",
            "SomeRandomFile.txt", // Not a backup file
            "backup_2025_11_13.bak", // Incomplete timestamp
            "BACKUP_MSSQL_MyDatabase", // Missing timestamp
            "BACKUP_MSSQL_MyDatabase_25_11_13_14_30.BAK",    // Two-digit year
            "BACKUP_MSSQL_MyDatabase_2025-11-13-14-30.BAK",  // Wrong separator
            "BACKUP_MSSQL_MyDatabase_abcd_ef_gh_ij_kl.BAK",   // Non-numeric characters
            "BACKUP_MSSQL_MyDatabase_2025_13_32_25_61.BAK"  // Invalid date/time values
        };

        foreach (var backupName in invalidBackupNames)
        {
            var result = DdlOperations.TryParseBackupTime(backupName, out var backupTime);

            Assert.False(result, $"Should not parse: {backupName}");
            Assert.Equal(default, backupTime);
        }
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
