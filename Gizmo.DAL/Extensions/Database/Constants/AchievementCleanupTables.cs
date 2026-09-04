namespace Gizmo.DAL.Extensions.DmlExtensions;

/// <summary>
/// Provider-neutral achievement table names and deletion order shared by the SQL Server and
/// PostgreSQL cleanup scripts. Order is significant: rows must be deleted child-before-parent so
/// restrictive foreign keys are satisfied. Identifier quoting and any dialect-specific SQL remain
/// in the provider-specific scripts.
/// </summary>
internal static class AchievementCleanupTables
{
    /// <summary>
    /// Achievement completion history cleared on every cleanup, before the always-run financial
    /// section, because completion rewards reference PointTransaction/Invoice via restrictive FKs
    /// that are reset unconditionally. Ordered child-before-parent.
    /// </summary>
    public static readonly string[] CompletionTables = new[]
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

    /// <summary>
    /// Achievement configuration and verification methods cleared only on full reset, ordered so
    /// restrictive references to Achievement/Challenge/Ladder are cleared first.
    /// </summary>
    public static readonly string[] ConfigTables = new[]
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
}
