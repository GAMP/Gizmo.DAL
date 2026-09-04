using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Gizmo.DAL;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Scripts;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database;

/// <summary>
/// DB-free regression validation for the database cleanup and user hard-delete scripts.
/// </summary>
/// <remarks>
/// Static, database-free validation that complements the disposable current-schema
/// database-backed collection: it asserts script content, provider parity, operation
/// ordering, and schema conformance against the EF model without starting a database.
/// </remarks>
public class ScriptValidationTests
{
    private const string UsersHardDelete = "USERS_HARD_DELETE";

    // Script-local table variables (MSSQL) / temp tables (Npgsql) declared inside the scripts,
    // not part of the migrated schema.
    private static readonly string[] DeclaredTempTables =
    {
        "UserIdList",
        "UserMemberIdList",
        "AchievementChallengeCompletionIdList",
        "AchievementLadderEventIdList",
        "AchievementRequirementSnapshotIdList",
    };

    // Cleanup scripts query provider catalog views; those references are implementation detail,
    // not cleanup data targets, so they are excluded from parity and schema checks.
    private static readonly string[] SystemCatalogPrefixes = { "sys.", "information_schema." };

    private static readonly string[] AchievementHistoryTables =
    {
        "AchievementChallengeCompletionPointsReward",
        "AchievementChallengeCompletionProductReward",
        "AchievementChallengeCompletionTimeReward",
        "AchievementChallengeCompletionReward",
        "AchievementChallengeCompletionRequirement",
        "AchievementLadderEventRequirement",
        "AchievementChallengeCompletion",
        "AchievementCompletion",
        "AchievementRequirementSnapshot",
        "AchievementLadderEvent",
        "AchievementLadderUserState",
    };

    private static readonly string[] AchievementConfigTables =
    {
        "AchievementChallengePointsReward",
        "AchievementChallengeProductReward",
        "AchievementChallengeTimeReward",
        "AchievementChallengeReward",
        "AchievementChallengeRequirement",
        "AchievementLadderRequirement",
        "AchievementLadderEntry",
        "AchievementLadderLevel",
        "AchievementAppCategoryFilter",
        "AchievementAppExeFilter",
        "AchievementAppFilter",
        "AchievementAppGroupFilter",
        "AchievementBillProfileFilter",
        "AchievementBranchFilter",
        "AchievementDayOfWeekFilter",
        "AchievementHostFilter",
        "AchievementHostGroupFilter",
        "AchievementPaymentMethodFilter",
        "AchievementProductFilter",
        "AchievementProductGroupFilter",
        "AchievementFilter",
        "AchievementParameter",
        "VerificationMethod",
        "Achievement",
        "AchievementChallenge",
        "AchievementLadder",
    };

    private static readonly string[] FinancialParentTables = { "InvoicePayment", "Invoice", "PointTransaction" };

    // CleanupUsersScript deletes the achievement completion history through direct DELETE targets
    // for the reward leaves/base, the requirement-snapshot base, and the completion/user-state
    // parents. The requirement leaf tables (AchievementChallengeCompletionRequirement and
    // AchievementLadderEventRequirement) are removed by the AchievementRequirementSnapshot base
    // delete's TPT cascade, so they are deliberately absent from this DELETE-target list.
    private static readonly string[] CleanupUsersAchievementHistoryTargets =
    {
        "AchievementChallengeCompletionPointsReward",
        "AchievementChallengeCompletionProductReward",
        "AchievementChallengeCompletionTimeReward",
        "AchievementChallengeCompletionReward",
        "AchievementRequirementSnapshot",
        "AchievementChallengeCompletion",
        "AchievementCompletion",
        "AchievementLadderEvent",
        "AchievementLadderUserState",
    };

    private static readonly Regex TableReferenceRegex = new(
        @"(?<keyword>DELETE\s+FROM|UPDATE|FROM|JOIN|INTO|USING)\s+(?<name>"
        + @"(?:\[[^\]]+\]|""[^""]+""|[A-Za-z_][A-Za-z0-9_]*)"
        + @"(?:\s*\.\s*(?:\[[^\]]+\]|""[^""]+""|[A-Za-z_][A-Za-z0-9_]*))?"
        + @")(?![A-Za-z0-9_])(?!\s*\()",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    // Handles both "DELETE FROM <table>" and the alias form "DELETE <alias> FROM <table>"
    // (MSSQL hard-delete uses the latter); groups 1-3 and 7-9 carry the table name.
    private static readonly Regex DeleteTargetRegex = new(
        @"DELETE\s+FROM\s+(?:\[([^\]]+)\]|""([^""]+)""|([A-Za-z_][A-Za-z0-9_]*))"
        + @"|DELETE\s+(?:\[([^\]]+)\]|""([^""]+)""|([A-Za-z_][A-Za-z0-9_]*))\s+FROM\s+(?:\[([^\]]+)\]|""([^""]+)""|([A-Za-z_][A-Za-z0-9_]*))",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex AliasRegex = new(
        @"\bAS\s+(?:\[([^\]]+)\]|""([^""]+)""|([A-Za-z_][A-Za-z0-9_]*))",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ValidTableNameRegex = new(
        @"^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)?$",
        RegexOptions.Compiled);

    private static HashSet<string>? _schemaTableNames;

    private static string GetScript(DatabaseType dbType, string scriptName)
    {
        var assembly = typeof(DmlOperations).Assembly;
        var typeName = IsMsSqlDialect(dbType) ? "Gizmo.DAL.Scripts.MsSqlScripts" : "Gizmo.DAL.Scripts.NpgSqlScripts";
        var type = assembly.GetType(typeName, throwOnError: true)
            ?? throw new InvalidOperationException($"Script provider type {typeName} not found.");
        var method = type.GetMethod("GetScript", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
            ?? throw new InvalidOperationException($"GetScript not found on {typeName}.");
        return (string)method.Invoke(null, new object[] { scriptName })!;
    }

    private static string GetCleanupScript(DatabaseType dbType, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        var assembly = typeof(DmlOperations).Assembly;
        var typeName = IsMsSqlDialect(dbType)
            ? "Gizmo.DAL.Extensions.DmlExtensions.SqlServer"
            : "Gizmo.DAL.Extensions.DmlExtensions.PostgreSql";
        var type = assembly.GetType(typeName, throwOnError: true)
            ?? throw new InvalidOperationException($"Cleanup script provider type {typeName} not found.");
        var method = type.GetMethod("CleanupScript", BindingFlags.Static | BindingFlags.Public)
            ?? throw new InvalidOperationException($"CleanupScript not found on {typeName}.");
        return (string)method.Invoke(null, new object[] { deleteUsers, deleteHosts, deleteOperators, deleteProducts })!;
    }

    private static string GetCleanupUsersScript(DatabaseType dbType)
    {
        var assembly = typeof(DmlOperations).Assembly;
        var typeName = IsMsSqlDialect(dbType)
            ? "Gizmo.DAL.Extensions.DmlExtensions.SqlServer"
            : "Gizmo.DAL.Extensions.DmlExtensions.PostgreSql";
        var type = assembly.GetType(typeName, throwOnError: true)
            ?? throw new InvalidOperationException($"Cleanup users script provider type {typeName} not found.");
        var method = type.GetMethod("CleanupUsersScript", BindingFlags.Static | BindingFlags.Public)
            ?? throw new InvalidOperationException($"CleanupUsersScript not found on {typeName}.");
        return (string)method.Invoke(null, null)!;
    }

    private static string NormalizeTableName(string raw)
    {
        var parts = Regex.Split(raw.Trim(), @"\s*\.\s*")
            .Select(part => part.Trim().Trim('[', ']', '"'))
            .Where(part => part.Length > 0)
            .ToArray();
        return string.Join(".", parts);
    }

    private static HashSet<string> ExtractAliases(string script)
    {
        var aliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match match in AliasRegex.Matches(script))
        {
            var alias = match.Groups[1].Success ? match.Groups[1].Value
                : match.Groups[2].Success ? match.Groups[2].Value
                : match.Groups[3].Value;
            aliases.Add(alias);
        }
        return aliases;
    }

    private static HashSet<string> ExtractTableReferences(string script)
    {
        var aliases = ExtractAliases(script);
        var references = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (Match match in TableReferenceRegex.Matches(script))
        {
            var name = NormalizeTableName(match.Groups["name"].Value);
            if (!ValidTableNameRegex.IsMatch(name))
                continue;
            if (aliases.Contains(name))
                continue;
            if (DeclaredTempTables.Contains(name, StringComparer.OrdinalIgnoreCase))
                continue;
            if (SystemCatalogPrefixes.Any(prefix => name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
                continue;
            references.Add(name);
        }

        return references;
    }

    private static List<string> ExtractDeleteTargets(string script)
    {
        var targets = new List<string>();
        foreach (Match match in DeleteTargetRegex.Matches(script))
        {
            var name = match.Groups[1].Success ? match.Groups[1].Value
                : match.Groups[2].Success ? match.Groups[2].Value
                : match.Groups[3].Success ? match.Groups[3].Value
                : match.Groups[7].Success ? match.Groups[7].Value
                : match.Groups[8].Success ? match.Groups[8].Value
                : match.Groups[9].Value;
            targets.Add(name);
        }
        return targets;
    }

    private static string DeleteLine(DatabaseType dbType, string tableName) =>
        IsMsSqlDialect(dbType) ? $"DELETE FROM [{tableName}]" : $"DELETE FROM \"{tableName}\"";

    // Central dialect mapping for the script producers. An unregistered DatabaseType fails loudly
    // here instead of silently taking the Npgsql branch in every test body.
    private static bool IsMsSqlDialect(DatabaseType dbType) => dbType switch
    {
        DatabaseType.MSSQL or DatabaseType.MSSQLEXPRESS or DatabaseType.LOCALDB => true,
        DatabaseType.POSTGRE => false,
        _ => throw new NotSupportedException($"No script producer registered for database type '{dbType}'.")
    };

    private static void AssertDeletedBefore(List<string> deleteTargets, string before, params string[] after)
    {
        var beforeIndex = deleteTargets.IndexOf(before);
        Assert.True(beforeIndex >= 0, $"'{before}' not found among DELETE targets.");

        foreach (var table in after)
        {
            var afterIndex = deleteTargets.IndexOf(table);
            Assert.True(afterIndex >= 0, $"'{table}' not found among DELETE targets.");
            Assert.True(
                beforeIndex < afterIndex,
                $"'{before}' (index {beforeIndex}) must be deleted before '{table}' (index {afterIndex}).");
        }
    }

    private static HashSet<string> SchemaTableNames()
    {
        if (_schemaTableNames is not null)
            return _schemaTableNames;

        var tables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // EF model table names = the migrated schema the cleanup scripts run against.
        // Provider-independent: mappings define table names via ToTable/entity names.
        var defaultOptions = new DbContextOptionsBuilder<DefaultDbContext>()
            .UseNpgsql("Host=localhost;Database=unused;Username=unused;Password=unused")
            .Options;
        using (var context = new DefaultDbContext(defaultOptions))
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                var table = entityType.GetTableName();
                if (!string.IsNullOrWhiteSpace(table))
                    tables.Add(table);
            }
        }

        // TickerQ tables live in the shared database under the "ticker" schema.
        var tickerOptions = new DbContextOptionsBuilder<TickerQDbContext>()
            .UseNpgsql("Host=localhost;Database=unused;Username=unused;Password=unused")
            .Options;
        using (var context = new TickerQDbContext(tickerOptions))
        {
            foreach (var entityType in context.Model.GetEntityTypes())
            {
                var table = entityType.GetTableName();
                if (!string.IsNullOrWhiteSpace(table))
                    tables.Add($"ticker.{table}");
            }
        }

        _schemaTableNames = tables;
        return tables;
    }

    public static IEnumerable<object[]> ProviderAndCleanupModeData()
    {
        foreach (var dbType in new[] { DatabaseType.MSSQL, DatabaseType.POSTGRE })
        {
            yield return new object[] { dbType, true, true, true, true };    // full reset
            yield return new object[] { dbType, true, false, false, false }; // users only
            yield return new object[] { dbType, false, true, false, false }; // hosts only
            yield return new object[] { dbType, false, false, false, true }; // products only
            yield return new object[] { dbType, false, false, true, false }; // operators only
            yield return new object[] { dbType, false, false, false, false }; // nothing
        }
    }

    #region USERS_HARD_DELETE

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public void UsersHardDelete_AchievementHistoryDeletedBeforeFinancialParents(DatabaseType dbType)
    {
        var script = GetScript(dbType, UsersHardDelete);
        var targets = ExtractDeleteTargets(script);

        // Achievement completion history must be removed before the financial parent tables
        // (restrict FKs from completion rewards to Invoice/PointTransaction).
        foreach (var historyTable in AchievementHistoryTables)
            AssertDeletedBefore(targets, historyTable, FinancialParentTables);

        // Financial parents keep their existing relative order (InvoicePayment before Invoice).
        AssertDeletedBefore(targets, "InvoicePayment", "Invoice");
    }

    [Fact]
    public void UsersHardDelete_ProviderScriptsHaveParity()
    {
        var msSql = GetScript(DatabaseType.MSSQL, UsersHardDelete);
        var npgsql = GetScript(DatabaseType.POSTGRE, UsersHardDelete);

        Assert.Equal(
            ExtractTableReferences(msSql),
            ExtractTableReferences(npgsql));
        Assert.Equal(
            ExtractDeleteTargets(msSql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase),
            ExtractDeleteTargets(npgsql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase));
    }

    #endregion

    #region Cleanup script (provider DML extensions)

    [Theory]
    [MemberData(nameof(ProviderAndCleanupModeData))]
    public void CleanupScript_AchievementHistoryDeletedBeforeFinancialParents(DatabaseType dbType, bool deleteUsers, bool deleteHosts, bool deleteOperators, bool deleteProducts)
    {
        var script = GetCleanupScript(dbType, deleteUsers, deleteHosts, deleteOperators, deleteProducts);
        var targets = ExtractDeleteTargets(script);

        // Completion history is cleared on every cleanup mode because the financial section always runs.
        foreach (var historyTable in AchievementHistoryTables)
            AssertDeletedBefore(targets, historyTable, FinancialParentTables);
    }

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public void CleanupScript_TickerQ_GuardedAndOrderedOnlyOnFullReset(DatabaseType dbType)
    {
        var full = GetCleanupScript(dbType, deleteUsers: true, deleteHosts: true, deleteOperators: true, deleteProducts: true);
        var partial = GetCleanupScript(dbType, deleteUsers: true, deleteHosts: false, deleteOperators: false, deleteProducts: false);

        if (IsMsSqlDialect(dbType))
        {
            // Guard: each statement is conditional on the ticker table existing.
            Assert.Contains("OBJECT_ID(N'ticker.CronTickerOccurrences'", full);
            Assert.Contains("OBJECT_ID(N'ticker.TimeTickers'", full);
            Assert.Contains("OBJECT_ID(N'ticker.CronTickers'", full);

            // Ordered: occurrences -> ParentId NULL -> TimeTickers delete -> CronTickers delete.
            AssertBefore(full, "DELETE FROM [ticker].[CronTickerOccurrences]", "UPDATE [ticker].[TimeTickers] SET [ParentId] = NULL");
            AssertBefore(full, "UPDATE [ticker].[TimeTickers] SET [ParentId] = NULL", "DELETE FROM [ticker].[TimeTickers]");
            AssertBefore(full, "DELETE FROM [ticker].[TimeTickers]", "DELETE FROM [ticker].[CronTickers]");
        }
        else
        {
            // Guards must quote the mixed-case identifiers; PostgreSQL folds unquoted names to
            // lowercase, which resolved to NULL and silently skipped the TickerQ reset.
            Assert.Contains("to_regclass('ticker.\"CronTickerOccurrences\"')", full);
            Assert.Contains("to_regclass('ticker.\"TimeTickers\"')", full);
            Assert.Contains("to_regclass('ticker.\"CronTickers\"')", full);

            AssertBefore(full, "DELETE FROM \"ticker\".\"CronTickerOccurrences\"", "UPDATE \"ticker\".\"TimeTickers\" SET \"ParentId\" = NULL");
            AssertBefore(full, "UPDATE \"ticker\".\"TimeTickers\" SET \"ParentId\" = NULL", "DELETE FROM \"ticker\".\"TimeTickers\"");
            AssertBefore(full, "DELETE FROM \"ticker\".\"TimeTickers\"", "DELETE FROM \"ticker\".\"CronTickers\"");
        }

        // The ticker schema may not exist (TickerQ not initialized); partial cleanups must not touch it.
        Assert.DoesNotContain("ticker.", partial);
        Assert.DoesNotContain("[ticker]", partial);
        Assert.DoesNotContain("\"ticker\"", partial);
    }

    [Fact]
    public void CleanupScript_AchievementScopeMatchesMode()
    {
        foreach (var dbType in new[] { DatabaseType.MSSQL, DatabaseType.POSTGRE })
        {
            var full = GetCleanupScript(dbType, deleteUsers: true, deleteHosts: true, deleteOperators: true, deleteProducts: true);
            var usersOnly = GetCleanupScript(dbType, deleteUsers: true, deleteHosts: false, deleteOperators: false, deleteProducts: false);
            var productsOnly = GetCleanupScript(dbType, deleteUsers: false, deleteHosts: false, deleteOperators: false, deleteProducts: true);
            var hostsOnly = GetCleanupScript(dbType, deleteUsers: false, deleteHosts: true, deleteOperators: false, deleteProducts: false);

            // Full reset removes achievement configuration, filters, and verification methods.
            foreach (var table in AchievementConfigTables)
                Assert.Contains(            DeleteLine(dbType, table), full);

            // Partial cleanups keep configuration but always clear completion history.
            foreach (var table in AchievementConfigTables)
                Assert.DoesNotContain(            DeleteLine(dbType, table), usersOnly);
            foreach (var table in AchievementHistoryTables)
                Assert.Contains(            DeleteLine(dbType, table), usersOnly);

            // Products-only removes achievement rows referencing products by deleting the TPT base
            // rows (AchievementFilter/AchievementChallengeReward) keyed by the product-derived
            // tables; the base->derived cascade removes the leaf rows, so no direct leaf DELETE is
            // expected and unrelated host-derived filters are untouched.
            Assert.Contains(            DeleteLine(dbType, "AchievementFilter"), productsOnly);
            Assert.Contains(            DeleteLine(dbType, "AchievementChallengeReward"), productsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementProductFilter"), productsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementChallengeProductReward"), productsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementHostFilter"), productsOnly);

            // Hosts-only removes host-derived filters the same way (base-row delete keyed by
            // AchievementHostFilter) while leaving product-derived filter/reward rows untouched.
            Assert.Contains(            DeleteLine(dbType, "AchievementFilter"), hostsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementHostFilter"), hostsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementProductFilter"), hostsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementChallengeReward"), hostsOnly);
            Assert.DoesNotContain(            DeleteLine(dbType, "AchievementChallengeProductReward"), hostsOnly);
        }
    }

    [Fact]
    public void CleanupScript_ProviderTableSetsHaveParityForEveryFlagCombination()
    {
        for (var flags = 0; flags < 16; flags++)
        {
            var deleteUsers = (flags & 1) != 0;
            var deleteHosts = (flags & 2) != 0;
            var deleteOperators = (flags & 4) != 0;
            var deleteProducts = (flags & 8) != 0;

            var msSql = GetCleanupScript(DatabaseType.MSSQL, deleteUsers, deleteHosts, deleteOperators, deleteProducts);
            var npgsql = GetCleanupScript(DatabaseType.POSTGRE, deleteUsers, deleteHosts, deleteOperators, deleteProducts);

            var msSqlReferences = ExtractTableReferences(msSql);
            var npgsqlReferences = ExtractTableReferences(npgsql);

            Assert.True(
                msSqlReferences.SetEquals(npgsqlReferences),
                $"Table reference parity failed for flags (users={deleteUsers}, hosts={deleteHosts}, operators={deleteOperators}, products={deleteProducts}). "
                + $"MSSQL-only: [{string.Join(", ", msSqlReferences.Except(npgsqlReferences))}], "
                + $"Npgsql-only: [{string.Join(", ", npgsqlReferences.Except(msSqlReferences))}].");

            var msSqlTargets = ExtractDeleteTargets(msSql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase);
            var npgsqlTargets = ExtractDeleteTargets(npgsql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase);

            Assert.True(
                msSqlTargets.SequenceEqual(npgsqlTargets),
                $"DELETE target parity failed for flags (users={deleteUsers}, hosts={deleteHosts}, operators={deleteOperators}, products={deleteProducts}). "
                + $"MSSQL-only: [{string.Join(", ", msSqlTargets.Except(npgsqlTargets))}], "
                + $"Npgsql-only: [{string.Join(", ", npgsqlTargets.Except(msSqlTargets))}].");
        }
    }

    #endregion

    #region CleanupUsers script (soft-deleted user purge)

    [Theory]
    [InlineData(DatabaseType.MSSQL)]
    [InlineData(DatabaseType.POSTGRE)]
    public void CleanupUsersScript_AchievementHistoryDeletedBeforeFinancialParents(DatabaseType dbType)
    {
        var script = GetCleanupUsersScript(dbType);
        var targets = ExtractDeleteTargets(script);

        // Soft-deleted user completion history (reward leaves/base, requirement-snapshot base and
        // completion/user-state parents) must be deleted before the always-run financial parents,
        // because completion reward leaves reference PointTransaction/Invoice through restrictive
        // FKs and the financial tables are deleted per soft-deleted user below.
        foreach (var historyTable in CleanupUsersAchievementHistoryTargets)
            AssertDeletedBefore(targets, historyTable, FinancialParentTables);

        // Financial parents keep their existing relative order (InvoicePayment before Invoice).
        AssertDeletedBefore(targets, "InvoicePayment", "Invoice");
    }

    [Fact]
    public void CleanupUsersScript_ProviderScriptsHaveParity()
    {
        var msSql = GetCleanupUsersScript(DatabaseType.MSSQL);
        var npgsql = GetCleanupUsersScript(DatabaseType.POSTGRE);

        Assert.Equal(
            ExtractTableReferences(msSql),
            ExtractTableReferences(npgsql));
        Assert.Equal(
            ExtractDeleteTargets(msSql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase),
            ExtractDeleteTargets(npgsql).OrderBy(name => name, StringComparer.OrdinalIgnoreCase));
    }

    #endregion

    #region Schema conformance (migrated schema)

    [Fact]
    public void Scripts_ReferencedTablesExistInMigratedSchema()
    {
        var schemaTables = SchemaTableNames();
        var referenced = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var dbType in new[] { DatabaseType.MSSQL, DatabaseType.POSTGRE })
        {
            referenced.UnionWith(ExtractTableReferences(GetScript(dbType, UsersHardDelete)));

            for (var flags = 0; flags < 16; flags++)
            {
                var script = GetCleanupScript(
                    dbType,
                    deleteUsers: (flags & 1) != 0,
                    deleteHosts: (flags & 2) != 0,
                    deleteOperators: (flags & 4) != 0,
                    deleteProducts: (flags & 8) != 0);
                referenced.UnionWith(ExtractTableReferences(script));
            }
        }

        var missing = referenced.Where(name => !schemaTables.Contains(name)).OrderBy(name => name).ToArray();
        Assert.True(
            missing.Length == 0,
            $"Scripts reference tables missing from the migrated schema (EF model): [{string.Join(", ", missing)}].");
    }

    [Fact]
    public void Update2AndTickerQ_MigrationTablesPresentInModel()
    {
        var schemaTables = SchemaTableNames();

        // Update2 (MSSQL 20260723100155 / Npgsql 20260723101222) created achievement/verification tables;
        // Update3 (20260825145608 / 20260825145544) only added columns, so no new tables are expected.
        var update2Tables = AchievementHistoryTables
            .Concat(AchievementConfigTables)
            .Distinct()
            .ToArray();

        var missing = update2Tables.Where(name => !schemaTables.Contains(name)).OrderBy(name => name).ToArray();
        Assert.True(
            missing.Length == 0,
            $"Update2 tables missing from the EF model: [{string.Join(", ", missing)}].");

        // TickerQ migrations (20260202195024 / 20260202195108) create the ticker schema tables.
        foreach (var tickerTable in new[] { "ticker.CronTickers", "ticker.TimeTickers", "ticker.CronTickerOccurrences" })
            Assert.Contains(tickerTable, schemaTables);
    }

    #endregion

    private static void AssertBefore(string script, string earlier, string later)
    {
        var earlierIndex = script.IndexOf(earlier, StringComparison.Ordinal);
        var laterIndex = script.IndexOf(later, StringComparison.Ordinal);
        Assert.True(earlierIndex >= 0, $"'{earlier}' not found in script.");
        Assert.True(laterIndex >= 0, $"'{later}' not found in script.");
        Assert.True(earlierIndex < laterIndex, $"'{earlier}' (index {earlierIndex}) must precede '{later}' (index {laterIndex}).");
    }
}