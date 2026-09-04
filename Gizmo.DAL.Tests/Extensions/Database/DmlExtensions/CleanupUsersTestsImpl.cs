using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Entities;
using Gizmo.DAL.Extensions;
using Gizmo.DAL.Scripts;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Gizmo.DAL.Tests.Extensions.Database.DmlExtensions.CleanupTestsImpl;

namespace Gizmo.DAL.Tests.Extensions.Database.DmlExtensions;

public static class CleanupUsersTestsImpl
{
    public static async Task CleanupUsers_OnlyRemovesDeletedUsers(DefaultDbContext context)
    {
        // Get the default user group created during test setup
        var defaultUserGroup = await context.UserGroups.FirstAsync();
        
        // Create test users - one deleted, one not deleted
        var deletedUser = new Entities.UserMember
        {
            Username = "DeletedTestUser",
            IsDeleted = true,
            UserGroupId = defaultUserGroup.Id,
            CreatedTime = DateTime.UtcNow
        };
        
        var activeUser = new Entities.UserMember
        {
            Username = "ActiveTestUser", 
            IsDeleted = false,
            UserGroupId = defaultUserGroup.Id,
            CreatedTime = DateTime.UtcNow
        };

        context.UsersMember.AddRange(deletedUser, activeUser);
        await context.SaveChangesAsync(CancellationToken.None);

        var deletedUserId = deletedUser.Id;
        var activeUserId = activeUser.Id;

        await context.CleanupUsers(CancellationToken.None);

        var deletedUserExists = await context.Users.AnyAsync(u => u.Id == deletedUserId);
        var activeUserExists = await context.Users.AnyAsync(u => u.Id == activeUserId);

        Assert.False(deletedUserExists, "Deleted user should be removed");
        Assert.True(activeUserExists, "Active user should remain");
    }

    public static async Task CleanupUsers_Basic(DefaultDbContext context)
    {
        // Get initial counts
        var initialUserCount = await context.Users.CountAsync();
        var initialDeletedUserCount = await context.Users.CountAsync(u => u.IsDeleted);
        var initialTransactionCount = await context.AssetTransactions.CountAsync();

        await context.CleanupUsers(CancellationToken.None);

        var finalUserCount = await context.Users.CountAsync();
        var finalDeletedUserCount = await context.Users.CountAsync(u => u.IsDeleted);
        var finalTransactionCount = await context.AssetTransactions.CountAsync();

        // Verify deleted users are removed
        Assert.Equal(0, finalDeletedUserCount);
        Assert.Equal(initialUserCount - initialDeletedUserCount, finalUserCount);

        // Verify related data is cleaned up (transactions for deleted users should be removed)
        Assert.True(finalTransactionCount <= initialTransactionCount);
    }

    #region User-deletion cleanup contract

    /// <summary>
    /// The public batch USERS_HARD_DELETE script removes the user's achievement completion history
    /// (including reward leaves referencing Invoice/PointTransaction) and then the user row itself.
    /// </summary>
    public static async Task UsersHardDelete_RemovesAchievementHistoryAndUser(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var graph = await SeedFinancialAndAchievementGraphAsync(db, ct);

        var deletedIds = await db.FromSqlScriptToIdsAsync(
            SQLScripts.USERS_HARD_DELETE,
            new Dictionary<string, object> { ["UserIds"] = graph.UserId.ToString() },
            ct);

        db.ChangeTracker.Clear();

        Assert.Contains(graph.UserId, deletedIds);
        Assert.False(await db.UsersMember.AnyAsync(u => u.Id == graph.UserId, ct), "Hard-deleted user should be removed.");
        Assert.Equal(0, await db.AchievementCompletions.CountAsync(ct));
        Assert.Equal(0, await db.AchievementChallengeCompletions.CountAsync(ct));
        Assert.Equal(0, await db.Set<AchievementChallengeCompletionReward>().CountAsync(ct));
        Assert.Equal(0, await db.Invoices.CountAsync(ct));
        Assert.Equal(0, await db.PointsTransaction.CountAsync(ct));
        Assert.Equal(0, await db.Orders.CountAsync(ct));
        // Batch hard delete removes completion history but not achievement configuration.
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == graph.AchievementId, ct));
        Assert.True(await db.AchievementChallenges.AnyAsync(c => c.Id == graph.ChallengeId, ct));
    }

    /// <summary>
    /// D1 regression: <see cref="DmlOperations.CleanupUsers"/> must succeed for a soft-deleted
    /// member whose processed challenge-completion rewards reference Invoice/PointTransaction
    /// through restrictive FKs. The script removes the reward leaves/base, requirement snapshots
    /// and user completion state (achievement/challenge completions, ladder events, ladder user
    /// states) before the financial parents, so the purge no longer hits restrict-FK failures;
    /// configuration and non-deleted users are retained.
    /// </summary>
    public static async Task CleanupUsers_RemovesSoftDeletedUserAchievementDependencies(DefaultDbContext db)
    {
        var ct = CancellationToken.None;
        var now = DateTime.UtcNow;

        var userGroup = await db.UserGroups.FirstAsync(ct);
        var deletedUser = new UserMember
        {
            Username = $"Deleted_{Guid.NewGuid():N}"[..24],
            UserGroupId = userGroup.Id,
            Email = "deleted@example.com",
            IsDeleted = true,
            CreatedTime = now
        };
        db.UsersMember.Add(deletedUser);
        await db.SaveChangesAsync(ct);

        // Processed completion-reward graph (reward leaves reference the invoice/point transaction).
        var graph = await SeedFinancialAndAchievementGraphAsync(db, ct, deletedUser);

        // Challenge-completion requirement snapshot (TPT leaf of AchievementRequirementSnapshot).
        var completion = await db.AchievementChallengeCompletions.FirstAsync(c => c.Id == graph.CompletionId, ct);
        completion.Requirements.Add(new AchievementChallengeCompletionRequirement
        {
            AchievementId = graph.AchievementId,
            RequiredCount = 1,
            CompletedCount = 1,
            TargetValue = 1,
            ActualValue = 1
        });

        // Ladder configuration plus per-user ladder state/event (with its requirement snapshot leaf).
        var ladder = new AchievementLadder
        {
            Period = CalendarPeriod.Week,
            Mode = AchievementLadderMode.Points,
            IsEnabled = false,
            IsDeleted = false,
            CreatedTime = now
        };
        db.AchievementLadders.Add(ladder);
        await db.SaveChangesAsync(ct);

        db.AchievementLadderUserStates.Add(new AchievementLadderUserState
        {
            UserId = deletedUser.Id,
            LadderId = ladder.Id,
            LastSettledPeriodStart = now.Date
        });

        var ladderEvent = new AchievementLadderEvent
        {
            UserId = deletedUser.Id,
            LadderId = ladder.Id,
            FromUserGroupId = userGroup.Id,
            ToUserGroupId = userGroup.Id,
            FromRank = 0,
            ToRank = 1,
            Trigger = AchievementLadderEventTrigger.Settle,
            PeriodStart = now.Date.AddDays(-7),
            CreatedTime = now
        };
        ladderEvent.Requirements.Add(new AchievementLadderEventRequirement
        {
            AchievementId = graph.AchievementId,
            RequiredCount = 1,
            CompletedCount = 1,
            TargetValue = 1,
            ActualValue = 1,
            PointsAwarded = 10
        });
        db.AchievementLadderEvents.Add(ladderEvent);
        await db.SaveChangesAsync(ct);

        // Pre-state guards: the seeded graph really references the financial parents and completion state.
        Assert.Equal(1, await db.AchievementLadderEvents.CountAsync(ct));
        Assert.Equal(1, await db.AchievementLadderUserStates.CountAsync(ct));
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementChallengeCompletionReward", ct));
        Assert.Equal(2, await CountTableRowsAsync(db, "AchievementRequirementSnapshot", ct));
        Assert.Equal(1, await db.Invoices.CountAsync(ct));
        Assert.Equal(1, await db.PointsTransaction.CountAsync(ct));

        await db.CleanupUsers(ct);

        db.ChangeTracker.Clear();

        // Soft-deleted member removed; active seeded member retained.
        Assert.False(await db.Users.AnyAsync(u => u.Id == graph.UserId, ct), "Soft-deleted user base row should be removed.");
        Assert.False(await db.UsersMember.AnyAsync(u => u.Id == graph.UserId, ct), "Soft-deleted member should be removed.");
        Assert.True(await db.UsersMember.AnyAsync(u => u.Username == "TestUser", ct), "Active seeded member should be retained.");

        // User completion state removed.
        Assert.Equal(0, await db.AchievementCompletions.CountAsync(ct));
        Assert.Equal(0, await db.AchievementChallengeCompletions.CountAsync(ct));
        Assert.Equal(0, await db.AchievementLadderEvents.CountAsync(ct));
        Assert.Equal(0, await db.AchievementLadderUserStates.CountAsync(ct));
        Assert.Equal(0, await CountTableRowsAsync(db, "AchievementChallengeCompletionReward", ct));
        Assert.Equal(0, await CountTableRowsAsync(db, "AchievementRequirementSnapshot", ct));

        // Financial parents of the soft-deleted user removed.
        Assert.Equal(0, await db.Invoices.CountAsync(ct));
        Assert.Equal(0, await db.PointsTransaction.CountAsync(ct));
        Assert.Equal(0, await db.Orders.CountAsync(ct));

        // Achievement/challenge/ladder/product configuration is retained (not user-owned).
        Assert.True(await db.Achievements.AnyAsync(a => a.Id == graph.AchievementId, ct));
        Assert.True(await db.AchievementChallenges.AnyAsync(c => c.Id == graph.ChallengeId, ct));
        Assert.True(await db.AchievementLadders.AnyAsync(l => l.Id == ladder.Id, ct));
        Assert.True(await db.Products.AnyAsync(p => p.Id == graph.ProductId, ct));
    }

    #endregion
}
