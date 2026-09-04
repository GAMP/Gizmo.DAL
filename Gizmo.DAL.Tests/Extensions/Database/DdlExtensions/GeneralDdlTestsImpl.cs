using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL;
using Gizmo.DAL.Extensions;
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
        Assert.Contains(cnmd.DatabaseName, name);

        // Verify the name contains a timestamp
        Assert.Contains("_", name);

        // Verify the name format: {provider}_{DatabaseName}_{timestamp}.{extension}
        var expectedProvider = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => "mssql",
            DatabaseType.POSTGRE => "pgsql",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
        };

        Assert.Contains($"{expectedProvider}_", name);
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

    public static void GenerateTempBackupName_CreatesCorrectFormat(DefaultDbContext context, DatabaseType dbType)
    {
        var name = context.Database.GenerateTempBackupName();

        var extension = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => ".bak",
            DatabaseType.POSTGRE => ".dump",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported for backup.")
        };

        // Verify the name ends with the correct extension
        Assert.EndsWith(extension, name);

        // Verify the name format: {provider}_{randomPart}.{extension}
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);
        
        // Expected provider prefix
        var expectedPrefix = dbType switch
        {
            DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => "mssql_",
            DatabaseType.POSTGRE => "pgsql_",
            _ => throw new NotSupportedException($"Database type {dbType} is not supported.")
        };

        // Verify the name starts with the expected provider prefix
        Assert.StartsWith(expectedPrefix, nameWithoutExtension);

        // Extract the random part (should be after the provider prefix)
        var randomPart = nameWithoutExtension.Substring(expectedPrefix.Length);
        
        // Should be 8 characters (GUID substring)
        Assert.Equal(8, randomPart.Length);
        
        // Should be all hex characters (lowercase)
        Assert.Matches("^[0-9a-f]{8}$", randomPart);
    }

    public static void GenerateTempBackupName_CreatesUniqueNames(DefaultDbContext context)
    {
        var name1 = context.Database.GenerateTempBackupName();
        var name2 = context.Database.GenerateTempBackupName();
        var name3 = context.Database.GenerateTempBackupName();

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
            ("mssql_MyDatabase_2025_11_13_14_30", new DateTime(2025, 11, 13, 14, 30, 0, DateTimeKind.Utc)),
            ("mssql_MyDatabase_2025_11_13_14_30.bak", new DateTime(2025, 11, 13, 14, 30, 0, DateTimeKind.Utc)),
            ("pgsqlMyDatabase_2025_01_01_00_00.dump", new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)),
            ("mysql_MyDatabase_2024_12_31_23_59.sql", new DateTime(2024, 12, 31, 23, 59, 0, DateTimeKind.Utc))
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
            "2025_11_13.bak", // Incomplete timestamp
            "mssql_MyDatabase", // Missing timestamp
            "mssql_MyDatabase_25_11_13_14_30.bak",    // Two-digit year
            "mssql_MyDatabase_2025-11-13-14-30.bak",  // Wrong separator
            "mssql_MyDatabase_abcd_ef_gh_ij_kl.bak",   // Non-numeric characters
            "mssql_MyDatabase_2025_13_32_25_61.bak"  // Invalid date/time values
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

    public static async Task TruncateLogs(DefaultDbContext context)
    {
        var rows = await context.Database.TruncateLogs(CancellationToken.None);

        Assert.True(rows >= 0);
    }
}
