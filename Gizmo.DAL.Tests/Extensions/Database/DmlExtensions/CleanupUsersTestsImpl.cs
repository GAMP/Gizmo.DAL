using System;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

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
}
