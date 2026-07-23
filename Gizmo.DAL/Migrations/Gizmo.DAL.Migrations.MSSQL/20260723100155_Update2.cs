using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSession_UserId",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "IsTierExempt",
                table: "UserMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DefaultOperatorId",
                table: "Register",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptPrinterNumber",
                table: "Register",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    AchievementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    SignalGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    MaxCompletionsPerRange = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievement_FileImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "FileImage",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Achievement_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Achievement_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallenge",
                columns: table => new
                {
                    AchievementChallengeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxCompletions = table.Column<int>(type: "int", nullable: true),
                    GlobalMaxCompletions = table.Column<int>(type: "int", nullable: true),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallenge", x => x.AchievementChallengeId);
                    table.ForeignKey(
                        name: "FK_AchievementChallenge_FileImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "FileImage",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AchievementChallenge_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementChallenge_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadder",
                columns: table => new
                {
                    AchievementLadderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadder", x => x.AchievementLadderId);
                    table.ForeignKey(
                        name: "FK_AchievementLadder_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementLadder_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VerificationMethod",
                columns: table => new
                {
                    VerificationMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Context = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<int>(type: "int", nullable: false),
                    CapabilityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationMethod", x => x.VerificationMethodId);
                    table.ForeignKey(
                        name: "FK_VerificationMethod_Integration_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integration",
                        principalColumn: "IntegrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VerificationMethod_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementCompletion",
                columns: table => new
                {
                    AchievementCompletionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    RangeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementCompletion", x => x.AchievementCompletionId);
                    table.ForeignKey(
                        name: "FK_AchievementCompletion_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementCompletion_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementFilter_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementFilter_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementParameter",
                columns: table => new
                {
                    AchievementParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementParameter", x => x.AchievementParameterId);
                    table.ForeignKey(
                        name: "FK_AchievementParameter_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementParameter_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementRequirementSnapshot",
                columns: table => new
                {
                    AchievementRequirementSnapshotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    RequiredCount = table.Column<int>(type: "int", nullable: false),
                    CompletedCount = table.Column<int>(type: "int", nullable: false),
                    TargetValue = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    ActualValue = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementRequirementSnapshot", x => x.AchievementRequirementSnapshotId);
                    table.ForeignKey(
                        name: "FK_AchievementRequirementSnapshot_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletion",
                columns: table => new
                {
                    AchievementChallengeCompletionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Occurrence = table.Column<int>(type: "int", nullable: false),
                    GlobalOccurrence = table.Column<int>(type: "int", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletion", x => x.AchievementChallengeCompletionId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletion_AchievementChallenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "AchievementChallenge",
                        principalColumn: "AchievementChallengeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletion_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeRequirement",
                columns: table => new
                {
                    AchievementChallengeRequirementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    RequiredCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeRequirement", x => x.AchievementChallengeRequirementId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeRequirement_AchievementChallenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "AchievementChallenge",
                        principalColumn: "AchievementChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeRequirement_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeRequirement_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementChallengeRequirement_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeReward",
                columns: table => new
                {
                    AchievementChallengeRewardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeReward", x => x.AchievementChallengeRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeReward_AchievementChallenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "AchievementChallenge",
                        principalColumn: "AchievementChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeReward_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementChallengeReward_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderEntry",
                columns: table => new
                {
                    AchievementLadderEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadderId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderEntry", x => x.AchievementLadderEntryId);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEntry_AchievementLadder_LadderId",
                        column: x => x.LadderId,
                        principalTable: "AchievementLadder",
                        principalColumn: "AchievementLadderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEntry_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementLadderEntry_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderEvent",
                columns: table => new
                {
                    AchievementLadderEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LadderId = table.Column<int>(type: "int", nullable: false),
                    FromUserGroupId = table.Column<int>(type: "int", nullable: false),
                    ToUserGroupId = table.Column<int>(type: "int", nullable: false),
                    FromRank = table.Column<int>(type: "int", nullable: false),
                    ToRank = table.Column<int>(type: "int", nullable: false),
                    Trigger = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderEvent", x => x.AchievementLadderEventId);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEvent_AchievementLadder_LadderId",
                        column: x => x.LadderId,
                        principalTable: "AchievementLadder",
                        principalColumn: "AchievementLadderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEvent_UserGroup_FromUserGroupId",
                        column: x => x.FromUserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEvent_UserGroup_ToUserGroupId",
                        column: x => x.ToUserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEvent_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderLevel",
                columns: table => new
                {
                    AchievementLadderLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadderId = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderLevel", x => x.AchievementLadderLevelId);
                    table.ForeignKey(
                        name: "FK_AchievementLadderLevel_AchievementLadder_LadderId",
                        column: x => x.LadderId,
                        principalTable: "AchievementLadder",
                        principalColumn: "AchievementLadderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLadderLevel_FileImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "FileImage",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AchievementLadderLevel_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderLevel_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementLadderLevel_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderUserState",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LadderId = table.Column<int>(type: "int", nullable: false),
                    LastSettledPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderUserState", x => new { x.UserId, x.LadderId });
                    table.ForeignKey(
                        name: "FK_AchievementLadderUserState_AchievementLadder_LadderId",
                        column: x => x.LadderId,
                        principalTable: "AchievementLadder",
                        principalColumn: "AchievementLadderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLadderUserState_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementAppCategoryFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    AppCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementAppCategoryFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementAppCategoryFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementAppCategoryFilter_AppCategory_AppCategoryId",
                        column: x => x.AppCategoryId,
                        principalTable: "AppCategory",
                        principalColumn: "AppCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementAppExeFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    AppExeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementAppExeFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementAppExeFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementAppExeFilter_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementAppFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementAppFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementAppFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementAppFilter_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementAppGroupFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    AppGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementAppGroupFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementAppGroupFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementAppGroupFilter_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementBillProfileFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    BillProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementBillProfileFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementBillProfileFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementBillProfileFilter_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementBranchFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementBranchFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementBranchFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementBranchFilter_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementDayOfWeekFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    DayTimeFrom = table.Column<TimeOnly>(type: "time", nullable: true),
                    DayTimeTo = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementDayOfWeekFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementDayOfWeekFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementHostFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementHostFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementHostFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementHostFilter_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementHostGroupFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    HostGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementHostGroupFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementHostGroupFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementHostGroupFilter_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementPaymentMethodFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementPaymentMethodFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementPaymentMethodFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementPaymentMethodFilter_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementProductFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementProductFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementProductFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementProductFilter_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementProductGroupFilter",
                columns: table => new
                {
                    AchievementFilterId = table.Column<int>(type: "int", nullable: false),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementProductGroupFilter", x => x.AchievementFilterId);
                    table.ForeignKey(
                        name: "FK_AchievementProductGroupFilter_AchievementFilter_AchievementFilterId",
                        column: x => x.AchievementFilterId,
                        principalTable: "AchievementFilter",
                        principalColumn: "AchievementFilterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementProductGroupFilter_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "ProductGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletionRequirement",
                columns: table => new
                {
                    AchievementRequirementSnapshotId = table.Column<int>(type: "int", nullable: false),
                    CompletionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletionRequirement", x => x.AchievementRequirementSnapshotId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionRequirement_AchievementChallengeCompletion_CompletionId",
                        column: x => x.CompletionId,
                        principalTable: "AchievementChallengeCompletion",
                        principalColumn: "AchievementChallengeCompletionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionRequirement_AchievementRequirementSnapshot_AchievementRequirementSnapshotId",
                        column: x => x.AchievementRequirementSnapshotId,
                        principalTable: "AchievementRequirementSnapshot",
                        principalColumn: "AchievementRequirementSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletionReward",
                columns: table => new
                {
                    AchievementChallengeCompletionRewardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProcessedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletionReward", x => x.AchievementChallengeCompletionRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionReward_AchievementChallengeCompletion_CompletionId",
                        column: x => x.CompletionId,
                        principalTable: "AchievementChallengeCompletion",
                        principalColumn: "AchievementChallengeCompletionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionReward_UserOperator_ProcessedById",
                        column: x => x.ProcessedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengePointsReward",
                columns: table => new
                {
                    AchievementChallengeRewardId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengePointsReward", x => x.AchievementChallengeRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengePointsReward_AchievementChallengeReward_AchievementChallengeRewardId",
                        column: x => x.AchievementChallengeRewardId,
                        principalTable: "AchievementChallengeReward",
                        principalColumn: "AchievementChallengeRewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeProductReward",
                columns: table => new
                {
                    AchievementChallengeRewardId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeProductReward", x => x.AchievementChallengeRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeProductReward_AchievementChallengeReward_AchievementChallengeRewardId",
                        column: x => x.AchievementChallengeRewardId,
                        principalTable: "AchievementChallengeReward",
                        principalColumn: "AchievementChallengeRewardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeProductReward_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeTimeReward",
                columns: table => new
                {
                    AchievementChallengeRewardId = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeTimeReward", x => x.AchievementChallengeRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeTimeReward_AchievementChallengeReward_AchievementChallengeRewardId",
                        column: x => x.AchievementChallengeRewardId,
                        principalTable: "AchievementChallengeReward",
                        principalColumn: "AchievementChallengeRewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderEventRequirement",
                columns: table => new
                {
                    AchievementRequirementSnapshotId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    PointsAwarded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderEventRequirement", x => x.AchievementRequirementSnapshotId);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEventRequirement_AchievementLadderEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "AchievementLadderEvent",
                        principalColumn: "AchievementLadderEventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLadderEventRequirement_AchievementRequirementSnapshot_AchievementRequirementSnapshotId",
                        column: x => x.AchievementRequirementSnapshotId,
                        principalTable: "AchievementRequirementSnapshot",
                        principalColumn: "AchievementRequirementSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementLadderRequirement",
                columns: table => new
                {
                    AchievementLadderRequirementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    AchievementId = table.Column<int>(type: "int", nullable: false),
                    RequiredCount = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLadderRequirement", x => x.AchievementLadderRequirementId);
                    table.ForeignKey(
                        name: "FK_AchievementLadderRequirement_AchievementLadderLevel_LevelId",
                        column: x => x.LevelId,
                        principalTable: "AchievementLadderLevel",
                        principalColumn: "AchievementLadderLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLadderRequirement_Achievement_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievement",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementLadderRequirement_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_AchievementLadderRequirement_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletionPointsReward",
                columns: table => new
                {
                    AchievementChallengeCompletionRewardId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    PointTransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletionPointsReward", x => x.AchievementChallengeCompletionRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionPointsReward_AchievementChallengeCompletionReward_AchievementChallengeCompletionRewardId",
                        column: x => x.AchievementChallengeCompletionRewardId,
                        principalTable: "AchievementChallengeCompletionReward",
                        principalColumn: "AchievementChallengeCompletionRewardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionPointsReward_PointTransaction_PointTransactionId",
                        column: x => x.PointTransactionId,
                        principalTable: "PointTransaction",
                        principalColumn: "PointTransactionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletionProductReward",
                columns: table => new
                {
                    AchievementChallengeCompletionRewardId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletionProductReward", x => x.AchievementChallengeCompletionRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionProductReward_AchievementChallengeCompletionReward_AchievementChallengeCompletionRewardId",
                        column: x => x.AchievementChallengeCompletionRewardId,
                        principalTable: "AchievementChallengeCompletionReward",
                        principalColumn: "AchievementChallengeCompletionRewardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionProductReward_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionProductReward_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AchievementChallengeCompletionTimeReward",
                columns: table => new
                {
                    AchievementChallengeCompletionRewardId = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementChallengeCompletionTimeReward", x => x.AchievementChallengeCompletionRewardId);
                    table.ForeignKey(
                        name: "FK_AchievementChallengeCompletionTimeReward_AchievementChallengeCompletionReward_AchievementChallengeCompletionRewardId",
                        column: x => x.AchievementChallengeCompletionRewardId,
                        principalTable: "AchievementChallengeCompletionReward",
                        principalColumn: "AchievementChallengeCompletionRewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId_CreatedTime",
                table: "UserSession",
                columns: new[] { "UserId", "CreatedTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Register_DefaultOperatorId",
                table: "Register",
                column: "DefaultOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId_CreatedTime",
                table: "Invoice",
                columns: new[] { "UserId", "CreatedTime" });

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_UserId_CreatedTime",
                table: "DepositTransaction",
                columns: new[] { "UserId", "CreatedTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_UserId_StartTime",
                table: "AppStat",
                columns: new[] { "UserId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_CreatedById",
                table: "Achievement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_ImageId",
                table: "Achievement",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_ModifiedById",
                table: "Achievement",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementAppCategoryFilter_AppCategoryId",
                table: "AchievementAppCategoryFilter",
                column: "AppCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementAppExeFilter_AppExeId",
                table: "AchievementAppExeFilter",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementAppFilter_AppId",
                table: "AchievementAppFilter",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementAppGroupFilter_AppGroupId",
                table: "AchievementAppGroupFilter",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementBillProfileFilter_BillProfileId",
                table: "AchievementBillProfileFilter",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementBranchFilter_BranchId",
                table: "AchievementBranchFilter",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallenge_CreatedById",
                table: "AchievementChallenge",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallenge_ImageId",
                table: "AchievementChallenge",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallenge_ModifiedById",
                table: "AchievementChallenge",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId_GlobalOccurrence",
                table: "AchievementChallengeCompletion",
                columns: new[] { "ChallengeId", "GlobalOccurrence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletion_UserId_ChallengeId_Occurrence",
                table: "AchievementChallengeCompletion",
                columns: new[] { "UserId", "ChallengeId", "Occurrence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionPointsReward_Transaction",
                table: "AchievementChallengeCompletionPointsReward",
                column: "PointTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionProductReward_InvoiceId",
                table: "AchievementChallengeCompletionProductReward",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionProductReward_ProductId",
                table: "AchievementChallengeCompletionProductReward",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionRequirement_CompletionId",
                table: "AchievementChallengeCompletionRequirement",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionReward_CompletionId",
                table: "AchievementChallengeCompletionReward",
                column: "CompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionReward_NotGranted",
                table: "AchievementChallengeCompletionReward",
                column: "Status",
                filter: "[Status] IN (0, 1)")
                .Annotation("SqlServer:Include", new[] { "CompletionId" });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletionReward_ProcessedById",
                table: "AchievementChallengeCompletionReward",
                column: "ProcessedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeProductReward_ProductId",
                table: "AchievementChallengeProductReward",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeRequirement_AchievementId",
                table: "AchievementChallengeRequirement",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeRequirement_ChallengeId_AchievementId",
                table: "AchievementChallengeRequirement",
                columns: new[] { "ChallengeId", "AchievementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeRequirement_CreatedById",
                table: "AchievementChallengeRequirement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeRequirement_ModifiedById",
                table: "AchievementChallengeRequirement",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeReward_ChallengeId",
                table: "AchievementChallengeReward",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeReward_CreatedById",
                table: "AchievementChallengeReward",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeReward_ModifiedById",
                table: "AchievementChallengeReward",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementCompletion_AchievementId",
                table: "AchievementCompletion",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementCompletion_UserId_AchievementId_RangeStart",
                table: "AchievementCompletion",
                columns: new[] { "UserId", "AchievementId", "RangeStart" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementFilter_AchievementId",
                table: "AchievementFilter",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementFilter_CreatedById",
                table: "AchievementFilter",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementHostFilter_HostId",
                table: "AchievementHostFilter",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementHostGroupFilter_HostGroupId",
                table: "AchievementHostGroupFilter",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadder_CreatedById",
                table: "AchievementLadder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadder_Enabled",
                table: "AchievementLadder",
                column: "IsEnabled",
                unique: true,
                filter: "[IsEnabled] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadder_ModifiedById",
                table: "AchievementLadder",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEntry_AchievementId",
                table: "AchievementLadderEntry",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEntry_CreatedById",
                table: "AchievementLadderEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEntry_LadderId_AchievementId",
                table: "AchievementLadderEntry",
                columns: new[] { "LadderId", "AchievementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEntry_ModifiedById",
                table: "AchievementLadderEntry",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEvent_FromUserGroupId",
                table: "AchievementLadderEvent",
                column: "FromUserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEvent_LadderId",
                table: "AchievementLadderEvent",
                column: "LadderId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEvent_ToUserGroupId",
                table: "AchievementLadderEvent",
                column: "ToUserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEvent_UserId_CreatedTime",
                table: "AchievementLadderEvent",
                columns: new[] { "UserId", "CreatedTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderEventRequirement_EventId",
                table: "AchievementLadderEventRequirement",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_CreatedById",
                table: "AchievementLadderLevel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_ImageId",
                table: "AchievementLadderLevel",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_LadderId_Rank",
                table: "AchievementLadderLevel",
                columns: new[] { "LadderId", "Rank" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_LadderId_UserGroupId",
                table: "AchievementLadderLevel",
                columns: new[] { "LadderId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_ModifiedById",
                table: "AchievementLadderLevel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderLevel_UserGroupId",
                table: "AchievementLadderLevel",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderRequirement_AchievementId",
                table: "AchievementLadderRequirement",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderRequirement_CreatedById",
                table: "AchievementLadderRequirement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderRequirement_LevelId_AchievementId",
                table: "AchievementLadderRequirement",
                columns: new[] { "LevelId", "AchievementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderRequirement_ModifiedById",
                table: "AchievementLadderRequirement",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderUserState_LadderId",
                table: "AchievementLadderUserState",
                column: "LadderId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementParameter_AchievementId_Key",
                table: "AchievementParameter",
                columns: new[] { "AchievementId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementParameter_CreatedById",
                table: "AchievementParameter",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementPaymentMethodFilter_PaymentMethodId",
                table: "AchievementPaymentMethodFilter",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementProductFilter_ProductId",
                table: "AchievementProductFilter",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementProductGroupFilter_ProductGroupId",
                table: "AchievementProductGroupFilter",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementRequirementSnapshot_AchievementId",
                table: "AchievementRequirementSnapshot",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationMethod_Context_IntegrationId_CapabilityGuid",
                table: "VerificationMethod",
                columns: new[] { "Context", "IntegrationId", "CapabilityGuid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationMethod_CreatedById",
                table: "VerificationMethod",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationMethod_IntegrationId",
                table: "VerificationMethod",
                column: "IntegrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_UserOperator_DefaultOperatorId",
                table: "Register",
                column: "DefaultOperatorId",
                principalTable: "UserOperator",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_UserOperator_DefaultOperatorId",
                table: "Register");

            migrationBuilder.DropTable(
                name: "AchievementAppCategoryFilter");

            migrationBuilder.DropTable(
                name: "AchievementAppExeFilter");

            migrationBuilder.DropTable(
                name: "AchievementAppFilter");

            migrationBuilder.DropTable(
                name: "AchievementAppGroupFilter");

            migrationBuilder.DropTable(
                name: "AchievementBillProfileFilter");

            migrationBuilder.DropTable(
                name: "AchievementBranchFilter");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletionPointsReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletionProductReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletionRequirement");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletionTimeReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengePointsReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengeProductReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengeRequirement");

            migrationBuilder.DropTable(
                name: "AchievementChallengeTimeReward");

            migrationBuilder.DropTable(
                name: "AchievementCompletion");

            migrationBuilder.DropTable(
                name: "AchievementDayOfWeekFilter");

            migrationBuilder.DropTable(
                name: "AchievementHostFilter");

            migrationBuilder.DropTable(
                name: "AchievementHostGroupFilter");

            migrationBuilder.DropTable(
                name: "AchievementLadderEntry");

            migrationBuilder.DropTable(
                name: "AchievementLadderEventRequirement");

            migrationBuilder.DropTable(
                name: "AchievementLadderRequirement");

            migrationBuilder.DropTable(
                name: "AchievementLadderUserState");

            migrationBuilder.DropTable(
                name: "AchievementParameter");

            migrationBuilder.DropTable(
                name: "AchievementPaymentMethodFilter");

            migrationBuilder.DropTable(
                name: "AchievementProductFilter");

            migrationBuilder.DropTable(
                name: "AchievementProductGroupFilter");

            migrationBuilder.DropTable(
                name: "VerificationMethod");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletionReward");

            migrationBuilder.DropTable(
                name: "AchievementChallengeReward");

            migrationBuilder.DropTable(
                name: "AchievementLadderEvent");

            migrationBuilder.DropTable(
                name: "AchievementRequirementSnapshot");

            migrationBuilder.DropTable(
                name: "AchievementLadderLevel");

            migrationBuilder.DropTable(
                name: "AchievementFilter");

            migrationBuilder.DropTable(
                name: "AchievementChallengeCompletion");

            migrationBuilder.DropTable(
                name: "AchievementLadder");

            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "AchievementChallenge");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_UserId_CreatedTime",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_Register_DefaultOperatorId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_UserId_CreatedTime",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_DepositTransaction_UserId_CreatedTime",
                table: "DepositTransaction");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_UserId_StartTime",
                table: "AppStat");

            migrationBuilder.DropColumn(
                name: "IsTierExempt",
                table: "UserMember");

            migrationBuilder.DropColumn(
                name: "DefaultOperatorId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "ReceiptPrinterNumber",
                table: "Register");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId",
                table: "UserSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice",
                column: "UserId");
        }
    }
}
