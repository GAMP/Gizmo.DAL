using System;
using System.Threading;
using System.Threading.Tasks;
using Gizmo.DAL.Contexts;
using Gizmo.DAL.Extensions;
using Xunit;

namespace Gizmo.DAL.Tests.Extensions.Database.DdlExtensions;

public static class LoginManagementTestsImpl
{
    public static async Task EnsureAdminExists_CreatesNewLogin_WhenNotExists(DefaultDbContext context)
    {
        var adminName = $"gizmo_test_admin_{Guid.NewGuid():N}";

        // Verify admin doesn't exist initially
        var existsBefore = await context.Database.LoginExists(adminName, CancellationToken.None);
        Assert.False(existsBefore);

        // Create the admin
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);

        // Verify admin now exists
        var existsAfter = await context.Database.LoginExists(adminName, CancellationToken.None);
        Assert.True(existsAfter);

        // Verify admin has proper permissions
        var hasAdminPermissions = await context.Database.HasAdminPermissions(adminName, CancellationToken.None);
        Assert.True(hasAdminPermissions);

        // Cleanup
        await context.Database.DropLogin(adminName, CancellationToken.None);
    }

    public static async Task EnsureAdminExists_IsIdempotent_WhenLoginAlreadyExists(DefaultDbContext context)
    {
        var adminName = $"gizmo_test_admin_{Guid.NewGuid():N}";

        // First call - create the admin
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);
        var existsAfterFirst = await context.Database.LoginExists(adminName, CancellationToken.None);
        Assert.True(existsAfterFirst);

        // Second call - should be idempotent
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);
        var existsAfterSecond = await context.Database.LoginExists(adminName, CancellationToken.None);
        Assert.True(existsAfterSecond);

        // Verify still has admin permissions
        var hasAdminPermissions = await context.Database.HasAdminPermissions(adminName, CancellationToken.None);
        Assert.True(hasAdminPermissions);

        // Cleanup
        await context.Database.DropLogin(adminName, CancellationToken.None);
    }

    public static async Task EnsurAdminExists_EnablesDisabledLogin(DefaultDbContext context)
    {
        var adminName = $"gizmo_test_admin_{Guid.NewGuid():N}";

        // Create and then disable the login
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);
        await context.Database.DisableLogin(adminName, CancellationToken.None);

        // Verify login is disabled
        var isDisabled = await context.Database.IsLoginDisabled(adminName, CancellationToken.None);
        Assert.True(isDisabled);

        // Call EnsureAdminExists again - should enable the login
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);

        // Verify login is now enabled
        var isEnabledAfter = !await context.Database.IsLoginDisabled(adminName, CancellationToken.None);
        Assert.True(isEnabledAfter);

        // Cleanup
        await context.Database.DropLogin(adminName, CancellationToken.None);
    }

    public static async Task LoginExists_ReturnsCorrectStatus(DefaultDbContext context)
    {
        var loginName = $"gizmo_test_login_{Guid.NewGuid():N}";

        // Verify login doesn't exist initially
        var existsBefore = await context.Database.LoginExists(loginName, CancellationToken.None);
        Assert.False(existsBefore);

        // Create the login
        await context.Database.EnsureAdminExists(loginName, CancellationToken.None);

        // Verify login now exists
        var existsAfter = await context.Database.LoginExists(loginName, CancellationToken.None);
        Assert.True(existsAfter);

        // Cleanup
        await context.Database.DropLogin(loginName, CancellationToken.None);

        // Verify login no longer exists
        var existsAfterDrop = await context.Database.LoginExists(loginName, CancellationToken.None);
        Assert.False(existsAfterDrop);
    }

    public static async Task HasAdminPermissions_ReturnsCorrectStatus(DefaultDbContext context)
    {
        var adminName = $"gizmo_test_admin_{Guid.NewGuid():N}";

        // Create admin login
        await context.Database.EnsureAdminExists(adminName, CancellationToken.None);

        // Verify has admin permissions
        var hasPermissions = await context.Database.HasAdminPermissions(adminName, CancellationToken.None);
        Assert.True(hasPermissions);

        // Cleanup
        await context.Database.DropLogin(adminName, CancellationToken.None);
    }

    public static async Task DisableLogin_DisablesLoginCorrectly(DefaultDbContext context)
    {
        var loginName = $"gizmo_test_login_{Guid.NewGuid():N}";

        // Create the login
        await context.Database.EnsureAdminExists(loginName, CancellationToken.None);

        // Verify login is initially enabled
        var isDisabledBefore = await context.Database.IsLoginDisabled(loginName, CancellationToken.None);
        Assert.False(isDisabledBefore);

        // Disable the login
        await context.Database.DisableLogin(loginName, CancellationToken.None);

        // Verify login is now disabled
        var isDisabledAfter = await context.Database.IsLoginDisabled(loginName, CancellationToken.None);
        Assert.True(isDisabledAfter);

        // Cleanup
        await context.Database.DropLogin(loginName, CancellationToken.None);
    }

    public static async Task DropLogin_RemovesLoginCorrectly(DefaultDbContext context)
    {
        var loginName = $"gizmo_test_login_{Guid.NewGuid():N}";

        // Create the login
        await context.Database.EnsureAdminExists(loginName, CancellationToken.None);

        // Verify login exists
        var existsBefore = await context.Database.LoginExists(loginName, CancellationToken.None);
        Assert.True(existsBefore);

        // Drop the login
        await context.Database.DropLogin(loginName, CancellationToken.None);

        // Verify login no longer exists
        var existsAfter = await context.Database.LoginExists(loginName, CancellationToken.None);
        Assert.False(existsAfter);
    }

    public static async Task HasAdminPermissions_ReturnsFalseForNonExistentLogin(DefaultDbContext context)
    {
        var nonExistentLogin = $"gizmo_nonexistent_{Guid.NewGuid():N}";

        // Check permissions for non-existent login - should return false, not throw
        var hasPermissions = await context.Database.HasAdminPermissions(nonExistentLogin, CancellationToken.None);
        Assert.False(hasPermissions);
    }

    public static async Task DisableLogin_HandlesNonExistentLogin(DefaultDbContext context)
    {
        var nonExistentLogin = $"gizmo_nonexistent_{Guid.NewGuid():N}";

        // Disabling non-existent login should throw an exception
        await Assert.ThrowsAnyAsync<Exception>(
            () => context.Database.DisableLogin(nonExistentLogin, CancellationToken.None));
    }
}
