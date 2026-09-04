using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database.DmlExtensions;

public static class CleanupTestsImpl
{
    public static async Task Cleanup_CreatesDefaultAdminOperator(DefaultDbContext context)
    {
        var testOperator = new Entities.UserOperator
        {
            Username = "TestOperator",
            CreatedTime = DateTime.UtcNow,
            UserCredential = new Entities.UserCredential()
        };
        context.UsersOperator.Add(testOperator);
        await context.SaveChangesAsync(CancellationToken.None);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: true,
            deleteProducts: false,
            CancellationToken.None
        );

        var adminOperator = await context.UsersOperator
            .Include(u => u.UserCredential)
            .Include(u => u.Permissions)
            .FirstOrDefaultAsync(u => u.Username == "Admin");

        Assert.NotNull(adminOperator);
        Assert.Equal("Admin", adminOperator.Username);
        Assert.NotNull(adminOperator.UserCredential);
        Assert.NotNull(adminOperator.UserCredential.Salt);
        Assert.NotNull(adminOperator.UserCredential.Password);
        Assert.True(adminOperator.Permissions.Any(), "Admin should have permissions");
    }

    public static async Task Cleanup_HandlesReservedHostReferences(DefaultDbContext context)
    {
        // Get the default user group from seeded data
        var defaultUserGroup = await context.UserGroups.FirstAsync();
        
        // Create a host
        var testHost = new Entities.Host
        {
            Name = "TestHost",
            CreatedTime = DateTime.UtcNow
        };
        context.Hosts.Add(testHost);
        await context.SaveChangesAsync(CancellationToken.None);

        // Create a guest user with ReservedHostId pointing to the host
        var guestUser = new Entities.UserGuest
        {
            Username = "TestGuest",
            UserGroupId = defaultUserGroup.Id,
            ReservedHostId = testHost.Id,
            CreatedTime = DateTime.UtcNow
        };
        context.UsersGuest.Add(guestUser);
        await context.SaveChangesAsync(CancellationToken.None);

        // This should not throw a foreign key constraint error
        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: true,
            deleteOperators: false,
            deleteProducts: false,
            CancellationToken.None
        );

        var hostExists = await context.Hosts.AnyAsync(h => h.Id == testHost.Id);
        var guestExists = await context.UsersGuest.AnyAsync(u => u.Id == guestUser.Id);
        
        Assert.False(hostExists, "Host should be deleted");
        
        if (guestExists)
        {
            var updatedGuest = await context.UsersGuest.FindAsync(guestUser.Id);
            Assert.Null(updatedGuest?.ReservedHostId);
        }
    }

    public static async Task Cleanup_HandlesAssetTransactionCheckedInBy(DefaultDbContext context)
    {
        // Get the default entities from seeded data
        var defaultAssetType = await context.AssetTypes.FirstAsync();
        var defaultAsset = await context.Assets.FirstAsync();
        var defaultUserMember = await context.UsersMember.FirstAsync();
        
        // Create a separate branch for this test to avoid conflicts with seeded branch during cleanup
        var testBranch = new Entities.Branch
        {
            Name = "Test Branch for AssetTransaction",
            IsDeleted = false,
            IsDisabled = false
        };
        context.Branches.Add(testBranch);
        await context.SaveChangesAsync(CancellationToken.None);
        
        // Create an operator
        var testOperator = new Entities.UserOperator
        {
            Username = "TestOperator",
            CreatedTime = DateTime.UtcNow,
            UserCredential = new Entities.UserCredential()
        };
        context.UsersOperator.Add(testOperator);
        await context.SaveChangesAsync(CancellationToken.None);

        // Create an asset transaction with CheckedInById reference
        var assetTransaction = new Entities.AssetTransaction
        {
            AssetTypeId = defaultAssetType.Id,
            AssetTypeName = defaultAssetType.Name,
            AssetId = defaultAsset.Id,
            UserId = defaultUserMember.Id,
            BranchId = testBranch.Id,
            CheckedInById = testOperator.Id,
            CreatedTime = DateTime.UtcNow
        };
        context.AssetTransactions.Add(assetTransaction);
        await context.SaveChangesAsync(CancellationToken.None);

        await context.Cleanup(
            deleteUsers: false,
            deleteHosts: false,
            deleteOperators: true,
            deleteProducts: false,
            CancellationToken.None
        );

        var operatorExists = await context.UsersOperator.AnyAsync(o => o.Id == testOperator.Id && o.Username != "Admin");
        var transactionExists = await context.AssetTransactions.AnyAsync(t => t.Id == assetTransaction.Id);
        
        Assert.False(operatorExists, "Test operator should be deleted");
        
        if (transactionExists)
        {
            var updatedTransaction = await context.AssetTransactions.FindAsync(assetTransaction.Id);
            Assert.Null(updatedTransaction?.CheckedInById);
        }
    }

    public static async Task Cleanup_HandlesEmptyDatabase(DefaultDbContext context)
    {
        // Get initial counts
        var initialUserCount = await context.Users.CountAsync();
        var initialHostCount = await context.Hosts.CountAsync();
        var initialProductCount = await context.Products.CountAsync();

        await context.Cleanup(
            deleteUsers: true,
            deleteHosts: true,
            deleteOperators: true,
            deleteProducts: true,
            CancellationToken.None
        );

        var finalUserCount = await context.Users.CountAsync();
        var finalHostCount = await context.Hosts.CountAsync();
        var finalProductCount = await context.Products.CountAsync();
        var adminOperator = await context.UsersOperator.FirstOrDefaultAsync(u => u.Username == "Admin");

        Assert.True(finalUserCount <= initialUserCount);
        Assert.True(finalHostCount <= initialHostCount);
        Assert.True(finalProductCount <= initialProductCount);
        Assert.NotNull(adminOperator); // Admin should be created even in empty database
    }

    public static async Task Cleanup_RespectsCancellationToken(DefaultDbContext context)
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel(); // Cancel immediately

        await Assert.ThrowsAnyAsync<OperationCanceledException>(async () =>
        {
            await context.Cleanup(
                deleteUsers: true,
                deleteHosts: true,
                deleteOperators: true,
                deleteProducts: true,
                cts.Token
            );
        });
    }

    #region Achievement/TickerQ cleanup contract

    internal sealed record FinancialAchievementGraph(
        int UserId,
        int ProductId,
        int OrderId,
        int InvoiceId,
        int PointTransactionId,
        int AchievementId,
        int ChallengeId,
        int CompletionId);

    private static bool IsSqlServer(DefaultDbContext db) =>
        db.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer";

    /// <summary>
    /// Seeds a user financial graph (product, order, invoice, point transaction) plus an
    /// achievement challenge completion whose reward leaves reference the invoice and the point
    /// transaction through restrictive FKs (product reward has NOT NULL ProductId, nullable
    /// InvoiceId; points reward has nullable PointTransactionId). When <paramref name="user"/> is
    /// supplied the graph is attached to that member; otherwise it is attached to the first
    /// seeded member.
    /// </summary>
    internal static async Task<FinancialAchievementGraph> SeedFinancialAndAchievementGraphAsync(DefaultDbContext db, CancellationToken ct, UserMember? user = null)
    {
        var now = DateTime.UtcNow;
        var targetUser = user ?? await db.UsersMember.FirstAsync(ct);

        var productGroup = new ProductGroup { Name = "Test Products", CreatedTime = now };
        var product = new Product { Name = "Test Product", ProductGroup = productGroup, CreatedTime = now };
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);

        var order = new ProductOrder { UserId = targetUser.Id, CreatedTime = now };
        db.Orders.Add(order);
        await db.SaveChangesAsync(ct);

        var invoice = new Invoice { UserId = targetUser.Id, ProductOrderId = order.Id, CreatedTime = now };
        var pointTransaction = new PointTransaction { UserId = targetUser.Id, CreatedTime = now };
        db.Invoices.Add(invoice);
        db.PointsTransaction.Add(pointTransaction);
        await db.SaveChangesAsync(ct);

        var achievement = new Achievement { Name = "Test Achievement", CreatedTime = now };
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        db.AchievementCompletions.Add(new AchievementCompletion
        {
            UserId = targetUser.Id,
            AchievementId = achievement.Id,
            RangeStart = DateTime.UtcNow.Date,
            CompletedTime = now,
            Quantity = 1
        });
        await db.SaveChangesAsync(ct);

        var challenge = new AchievementChallenge { Name = "Test Challenge", CreatedTime = now };
        db.AchievementChallenges.Add(challenge);
        await db.SaveChangesAsync(ct);

        var completion = new AchievementChallengeCompletion
        {
            UserId = targetUser.Id,
            ChallengeId = challenge.Id,
            Occurrence = 1,
            GlobalOccurrence = 1,
            CompletedTime = now
        };
        completion.Rewards.Add(new AchievementChallengeCompletionPointsReward
        {
            Amount = 25,
            PointTransactionId = pointTransaction.Id,
            Status = AchievementChallengeRewardStatus.Granted
        });
        completion.Rewards.Add(new AchievementChallengeCompletionProductReward
        {
            ProductId = product.Id,
            InvoiceId = invoice.Id,
            Quantity = 1,
            Status = AchievementChallengeRewardStatus.Granted
        });
        db.AchievementChallengeCompletions.Add(completion);
        await db.SaveChangesAsync(ct);

        return new FinancialAchievementGraph(
            targetUser.Id, product.Id, order.Id, invoice.Id, pointTransaction.Id,
            achievement.Id, challenge.Id, completion.Id);
    }

    private static async Task<int> CountScalarAsync(DefaultDbContext db, string sql, CancellationToken ct)
    {
        var values = await db.Database.SqlQueryRaw<int>(sql).ToArrayAsync(ct);
        return values.Length == 0 ? 0 : values[0];
    }

    private static string TickerCountSql(string table, DefaultDbContext db) => IsSqlServer(db)
        ? $"SELECT COUNT(*) FROM [ticker].[{table}]"
        : $"SELECT COUNT(*)::int FROM \"ticker\".\"{table}\"";

    /// <summary>
    /// Provider-qualified whole-table row count used to assert TPT base-table state (e.g.
    /// AchievementFilter / AchievementChallengeReward base rows) that has no EF entity set.
    /// </summary>
    private static string TableCountSql(string table, DefaultDbContext db) => IsSqlServer(db)
        ? $"SELECT COUNT(*) FROM [{table}]"
        : $"SELECT COUNT(*)::int FROM \"{table}\"";

    internal static Task<int> CountTableRowsAsync(DefaultDbContext db, string table, CancellationToken ct) =>
        CountScalarAsync(db, TableCountSql(table, db), ct);

    private static async Task SeedTickerRowsAsync(DefaultDbContext db, CancellationToken ct)
    {
        var cronId = Guid.NewGuid();
        var occurrenceId = Guid.NewGuid();
        var parentId = Guid.NewGuid();
        var childId = Guid.NewGuid();
        var executionTime = DateTime.UtcNow.AddMinutes(5);
        var sql = IsSqlServer(db)
            ? $"""
                INSERT INTO [ticker].[CronTickers] ([Id], [Retries], [CreatedAt], [UpdatedAt])
                VALUES ('{cronId}', 0, SYSUTCDATETIME(), SYSUTCDATETIME());
                INSERT INTO [ticker].[CronTickerOccurrences] ([Id], [Status], [ExecutionTime], [CronTickerId], [ElapsedTime], [RetryCount], [CreatedAt], [UpdatedAt])
                VALUES ('{occurrenceId}', 0, '{executionTime:yyyy-MM-ddTHH:mm:ss.fffffff}', '{cronId}', 0, 0, SYSUTCDATETIME(), SYSUTCDATETIME());
                INSERT INTO [ticker].[TimeTickers] ([Id], [CreatedAt], [UpdatedAt], [Status], [ElapsedTime], [Retries], [RetryCount])
                VALUES ('{parentId}', SYSUTCDATETIME(), SYSUTCDATETIME(), 0, 0, 0, 0);
                INSERT INTO [ticker].[TimeTickers] ([Id], [CreatedAt], [UpdatedAt], [Status], [ElapsedTime], [Retries], [RetryCount], [ParentId])
                VALUES ('{childId}', SYSUTCDATETIME(), SYSUTCDATETIME(), 0, 0, 0, 0, '{parentId}');
                """
            : $"""
                INSERT INTO "ticker"."CronTickers" ("Id", "Retries", "CreatedAt", "UpdatedAt")
                VALUES ('{cronId}'::uuid, 0, now(), now());
                INSERT INTO "ticker"."CronTickerOccurrences" ("Id", "Status", "ExecutionTime", "CronTickerId", "ElapsedTime", "RetryCount", "CreatedAt", "UpdatedAt")
                VALUES ('{occurrenceId}'::uuid, 0, '{executionTime:yyyy-MM-ddTHH:mm:ss.fffffff}', '{cronId}'::uuid, 0, 0, now(), now());
                INSERT INTO "ticker"."TimeTickers" ("Id", "CreatedAt", "UpdatedAt", "Status", "ElapsedTime", "Retries", "RetryCount")
                VALUES ('{parentId}'::uuid, now(), now(), 0, 0, 0, 0);
                INSERT INTO "ticker"."TimeTickers" ("Id", "CreatedAt", "UpdatedAt", "Status", "ElapsedTime", "Retries", "RetryCount", "ParentId")
                VALUES ('{childId}'::uuid, now(), now(), 0, 0, 0, 0, '{parentId}'::uuid);
                """;
        await db.Database.ExecuteSqlRawAsync(sql, ct);
    }

    /// <summary>
    /// Full reset must delete achievement completion rewards before the always-reset Invoice and
    /// PointTransaction rows they reference through restrictive FKs, then remove all configuration.
    /// </summary>
    public static async Task FullReset_RemovesAchievementCompletionRewardsBeforeFinancialParents(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var graph = await SeedFinancialAndAchievementGraphAsync(db, ct);

        await db.Cleanup(deleteUsers: true, deleteHosts: true, deleteOperators: true, deleteProducts: true, ct);

        db.ChangeTracker.Clear();

        Assert.Equal(0, await db.Achievements.CountAsync(ct));
        Assert.Equal(0, await db.AchievementCompletions.CountAsync(ct));
        Assert.Equal(0, await db.AchievementChallenges.CountAsync(ct));
        Assert.Equal(0, await db.AchievementChallengeCompletions.CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementChallengeCompletionReward>().CountAsync(ct));
        Assert.Equal(0, await db.Invoices.CountAsync(ct));
        Assert.Equal(0, await db.PointsTransaction.CountAsync(ct));
        Assert.Equal(0, await db.Orders.CountAsync(ct));
        Assert.Equal(0, await db.Products.CountAsync(ct));
        Assert.Equal(0, await db.UsersMember.CountAsync(ct));
        Assert.NotNull(await db.UsersOperator.FirstOrDefaultAsync(o => o.Username == "Admin", ct));
        Assert.True(graph.CompletionId > 0);
    }

    /// <summary>
    /// Full reset resets TickerQ rows in the shared database: cron occurrences, cron tickers and
    /// self-referencing time tickers (child-before-parent ordering, parent FK released first).
    /// </summary>
    public static async Task FullReset_ResetsTickerQIncludingSelfReference(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        await SeedTickerRowsAsync(db, ct);

        Assert.Equal(1, await CountScalarAsync(db, TickerCountSql("CronTickers", db), ct));
        Assert.Equal(1, await CountScalarAsync(db, TickerCountSql("CronTickerOccurrences", db), ct));
        Assert.Equal(2, await CountScalarAsync(db, TickerCountSql("TimeTickers", db), ct));

        await db.Cleanup(deleteUsers: true, deleteHosts: true, deleteOperators: true, deleteProducts: true, ct);

        Assert.Equal(0, await CountScalarAsync(db, TickerCountSql("CronTickers", db), ct));
        Assert.Equal(0, await CountScalarAsync(db, TickerCountSql("CronTickerOccurrences", db), ct));
        Assert.Equal(0, await CountScalarAsync(db, TickerCountSql("TimeTickers", db), ct));
    }

    /// <summary>
    /// Full reset succeeds even when the ticker schema was never initialized (guards must skip the
    /// ticker statements instead of failing on missing tables).
    /// </summary>
    public static async Task FullReset_SucceedsWhenTickerSchemaAbsent(DefaultDbContext db)
    {
        var ct = CancellationToken.None;

        await db.Cleanup(deleteUsers: true, deleteHosts: true, deleteOperators: true, deleteProducts: true, ct);

        db.ChangeTracker.Clear();
        Assert.NotNull(await db.UsersOperator.FirstOrDefaultAsync(o => o.Username == "Admin", ct));
    }

    /// <summary>
    /// Products-only cleanup removes achievement rows referencing products (product filters and
    /// challenge product reward configuration) so product deletion cannot hit restrictive FKs;
    /// achievement/challenge configuration that does not reference products is retained.
    /// </summary>
    public static async Task ProductsOnlyCleanup_RemovesAchievementProductDependencies(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var now = DateTime.UtcNow;

        var productGroup = new ProductGroup { Name = "Test Products", CreatedTime = now };
        var product = new Product { Name = "Test Product", ProductGroup = productGroup, CreatedTime = now };
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);

        var achievement = new Achievement { Name = "Product Achievement", CreatedTime = now };
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        var challenge = new AchievementChallenge { Name = "Product Challenge", CreatedTime = now };
        db.AchievementChallenges.Add(challenge);
        await db.SaveChangesAsync(ct);

        db.Set<AchievementProductFilter>().Add(new AchievementProductFilter
        {
            AchievementId = achievement.Id,
            ProductId = product.Id,
            CreatedTime = now
        });
        db.Set<AchievementChallengeProductReward>().Add(new AchievementChallengeProductReward
        {
            ChallengeId = challenge.Id,
            ProductId = product.Id,
            Quantity = 1,
            CreatedTime = now
        });
        await db.SaveChangesAsync(ct);

        await db.Cleanup(deleteUsers: false, deleteHosts: false, deleteOperators: false, deleteProducts: true, ct);

        db.ChangeTracker.Clear();

        Assert.Equal(0, await db.Products.CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementProductFilter>().CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementChallengeProductReward>().CountAsync(ct));
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == achievement.Id, ct), "Achievement configuration should be retained in products-only cleanup.");
        Assert.True(await db.AchievementChallenges.AnyAsync(c => c.Id == challenge.Id, ct), "Challenge configuration should be retained in products-only cleanup.");
    }

    /// <summary>
    /// Hosts-only cleanup removes achievement rows referencing hosts (host filters) so host
    /// deletion cannot hit restrictive FKs; achievement configuration is retained.
    /// </summary>
    public static async Task HostsOnlyCleanup_RemovesAchievementHostDependencies(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var now = DateTime.UtcNow;

        var achievement = new Achievement { Name = "Host Achievement", CreatedTime = now };
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        var host = new Host { Name = "Test Host", CreatedTime = now };
        db.Hosts.Add(host);
        await db.SaveChangesAsync(ct);

        db.Set<AchievementHostFilter>().Add(new AchievementHostFilter
        {
            AchievementId = achievement.Id,
            HostId = host.Id,
            CreatedTime = now
        });
        await db.SaveChangesAsync(ct);

        await db.Cleanup(deleteUsers: false, deleteHosts: true, deleteOperators: false, deleteProducts: false, ct);

        db.ChangeTracker.Clear();

        Assert.Equal(0, await db.Hosts.CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementHostFilter>().CountAsync(ct));
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == achievement.Id, ct), "Achievement configuration should be retained in hosts-only cleanup.");
    }

    /// <summary>
    /// D2 regression (products-only): cleanup must delete the TPT base rows
    /// (AchievementFilter/AchievementChallengeReward) keyed by product-derived rows so the
    /// base-&gt;derived cascade removes the product filter/reward leaves, leaving no orphaned base
    /// configuration. Unrelated host filter and points reward configuration (leaf and base) is
    /// preserved.
    /// </summary>
    public static async Task ProductsOnlyCleanup_RemovesTptBaseRowsForProductsAndPreservesUnrelatedConfiguration(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var now = DateTime.UtcNow;

        var productGroup = new ProductGroup { Name = "D2 Products", CreatedTime = now };
        var product = new Product { Name = "D2 Target Product", ProductGroup = productGroup, CreatedTime = now };
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);

        var host = new Host { Name = "D2 Preserved Host", CreatedTime = now };
        db.Hosts.Add(host);
        await db.SaveChangesAsync(ct);

        var productAchievement = new Achievement { Name = "D2 Product Achievement", CreatedTime = now };
        var hostAchievement = new Achievement { Name = "D2 Host Achievement", CreatedTime = now };
        db.Achievements.AddRange(productAchievement, hostAchievement);
        await db.SaveChangesAsync(ct);

        var challenge = new AchievementChallenge { Name = "D2 Reward Challenge", CreatedTime = now };
        db.AchievementChallenges.Add(challenge);
        await db.SaveChangesAsync(ct);

        db.Set<AchievementProductFilter>().Add(new AchievementProductFilter
        {
            AchievementId = productAchievement.Id,
            ProductId = product.Id,
            CreatedTime = now
        });
        db.Set<AchievementHostFilter>().Add(new AchievementHostFilter
        {
            AchievementId = hostAchievement.Id,
            HostId = host.Id,
            CreatedTime = now
        });
        db.Set<AchievementChallengeProductReward>().Add(new AchievementChallengeProductReward
        {
            ChallengeId = challenge.Id,
            ProductId = product.Id,
            Quantity = 1,
            CreatedTime = now
        });
        db.Set<AchievementChallengePointsReward>().Add(new AchievementChallengePointsReward
        {
            ChallengeId = challenge.Id,
            Amount = 25,
            CreatedTime = now
        });
        await db.SaveChangesAsync(ct);

        // Pre-state: TPT gives one base row per filter/reward leaf.
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementFilter", ct));
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementChallengeReward", ct));

        await db.Cleanup(deleteUsers: false, deleteHosts: false, deleteOperators: false, deleteProducts: true, ct);

        db.ChangeTracker.Clear();

        Assert.Equal(0, await db.Products.CountAsync(ct));
        Assert.True(await db.Hosts.AnyAsync(h => h.Id == host.Id, ct), "Hosts are not deleted by products-only cleanup.");

        // Product-derived leaves removed.
        Assert.Equal(0, await db.Set<AchievementProductFilter>().CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementChallengeProductReward>().CountAsync(ct));

        // No orphaned base rows: only the preserved host filter and points reward bases remain.
        Assert.Equal(1, await CountTableRowsAsync(db, "AchievementFilter", ct));
        Assert.Equal(1, await CountTableRowsAsync(db, "AchievementChallengeReward", ct));
        Assert.Equal(1, await db.Set<AchievementHostFilter>().CountAsync(ct));
        Assert.Equal(1, await db.Set<AchievementChallengePointsReward>().CountAsync(ct));

        // Configuration retained.
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == productAchievement.Id || a.Id == hostAchievement.Id, ct));
        Assert.True(await db.AchievementChallenges.AnyAsync(c => c.Id == challenge.Id, ct));
    }

    /// <summary>
    /// D2 regression (hosts-only): cleanup must delete the TPT base AchievementFilter rows keyed by
    /// host-derived rows so the cascade removes the host filter leaves and host deletion cannot hit
    /// the restrictive host FK. Product-derived filter/reward configuration (leaf and base) is
    /// preserved because no reward type is host-dependent.
    /// </summary>
    public static async Task HostsOnlyCleanup_RemovesTptBaseRowsForHostsAndPreservesUnrelatedConfiguration(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var now = DateTime.UtcNow;

        var host = new Host { Name = "D2 Target Host", CreatedTime = now };
        db.Hosts.Add(host);
        await db.SaveChangesAsync(ct);

        var productGroup = new ProductGroup { Name = "D2 Products", CreatedTime = now };
        var product = new Product { Name = "D2 Preserved Product", ProductGroup = productGroup, CreatedTime = now };
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);

        var hostAchievement = new Achievement { Name = "D2 Host Achievement", CreatedTime = now };
        var productAchievement = new Achievement { Name = "D2 Product Achievement", CreatedTime = now };
        db.Achievements.AddRange(hostAchievement, productAchievement);
        await db.SaveChangesAsync(ct);

        var challenge = new AchievementChallenge { Name = "D2 Reward Challenge", CreatedTime = now };
        db.AchievementChallenges.Add(challenge);
        await db.SaveChangesAsync(ct);

        db.Set<AchievementHostFilter>().Add(new AchievementHostFilter
        {
            AchievementId = hostAchievement.Id,
            HostId = host.Id,
            CreatedTime = now
        });
        db.Set<AchievementProductFilter>().Add(new AchievementProductFilter
        {
            AchievementId = productAchievement.Id,
            ProductId = product.Id,
            CreatedTime = now
        });
        db.Set<AchievementChallengeProductReward>().Add(new AchievementChallengeProductReward
        {
            ChallengeId = challenge.Id,
            ProductId = product.Id,
            Quantity = 1,
            CreatedTime = now
        });
        db.Set<AchievementChallengePointsReward>().Add(new AchievementChallengePointsReward
        {
            ChallengeId = challenge.Id,
            Amount = 25,
            CreatedTime = now
        });
        await db.SaveChangesAsync(ct);

        // Pre-state: one base row per filter leaf.
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementFilter", ct));

        await db.Cleanup(deleteUsers: false, deleteHosts: true, deleteOperators: false, deleteProducts: false, ct);

        db.ChangeTracker.Clear();

        Assert.Equal(0, await db.Hosts.CountAsync(ct));
        Assert.True(await db.Products.AnyAsync(p => p.Id == product.Id, ct), "Products are not deleted by hosts-only cleanup.");

        // Host-derived filter leaves removed; only the preserved product filter leaf/base remains.
        Assert.Equal(0, await db.Set<AchievementHostFilter>().CountAsync(ct));
        Assert.Equal(1, await db.Set<AchievementProductFilter>().CountAsync(ct));
        Assert.Equal(1, await CountTableRowsAsync(db, "AchievementFilter", ct));

        // No reward configuration depends on hosts, so product reward and points reward leaves and
        // bases are fully preserved.
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementChallengeReward", ct));
        Assert.Equal(1, await db.Set<AchievementChallengeProductReward>().CountAsync(ct));
        Assert.Equal(1, await db.Set<AchievementChallengePointsReward>().CountAsync(ct));

        // Configuration retained.
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == hostAchievement.Id || a.Id == productAchievement.Id, ct));
        Assert.True(await db.AchievementChallenges.AnyAsync(c => c.Id == challenge.Id, ct));
    }

    #endregion

}
