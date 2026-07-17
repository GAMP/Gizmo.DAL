using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update3 : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_AchievementLadderUserState",
                table: "AchievementLadderUserState");

            migrationBuilder.DropIndex(
                name: "IX_AchievementLadderUserState_UserId_LadderId",
                table: "AchievementLadderUserState");

            migrationBuilder.DropIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId",
                table: "AchievementChallengeCompletion");

            migrationBuilder.DropColumn(
                name: "AchievementLadderUserStateId",
                table: "AchievementLadderUserState");

            migrationBuilder.AddColumn<bool>(
                name: "IsTierExempt",
                table: "UserMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AchievementLadderUserState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSettledPeriodStart",
                table: "AchievementLadderUserState",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "LadderId",
                table: "AchievementLadderUserState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedTime",
                table: "AchievementChallengeCompletion",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "GlobalOccurrence",
                table: "AchievementChallengeCompletion",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "Options",
                table: "AchievementChallenge",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "MaxCompletions",
                table: "AchievementChallenge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "AchievementChallenge",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AchievementChallenge",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AchievementChallenge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "GlobalMaxCompletions",
                table: "AchievementChallenge",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchievementLadderUserState",
                table: "AchievementLadderUserState",
                columns: new[] { "UserId", "LadderId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId_CreatedTime",
                table: "UserSession",
                columns: new[] { "UserId", "CreatedTime" });

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
                name: "IX_AchievementChallengeCompletion_ChallengeId_GlobalOccurrence",
                table: "AchievementChallengeCompletion",
                columns: new[] { "ChallengeId", "GlobalOccurrence" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSession_UserId_CreatedTime",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_UserId_CreatedTime",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_DepositTransaction_UserId_CreatedTime",
                table: "DepositTransaction");

            migrationBuilder.DropIndex(
                name: "IX_AppStat_UserId_StartTime",
                table: "AppStat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AchievementLadderUserState",
                table: "AchievementLadderUserState");

            migrationBuilder.DropIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId_GlobalOccurrence",
                table: "AchievementChallengeCompletion");

            migrationBuilder.DropColumn(
                name: "IsTierExempt",
                table: "UserMember");

            migrationBuilder.DropColumn(
                name: "GlobalOccurrence",
                table: "AchievementChallengeCompletion");

            migrationBuilder.DropColumn(
                name: "GlobalMaxCompletions",
                table: "AchievementChallenge");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSettledPeriodStart",
                table: "AchievementLadderUserState",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "LadderId",
                table: "AchievementLadderUserState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AchievementLadderUserState",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "AchievementLadderUserStateId",
                table: "AchievementLadderUserState",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedTime",
                table: "AchievementChallengeCompletion",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "Options",
                table: "AchievementChallenge",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "MaxCompletions",
                table: "AchievementChallenge",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "AchievementChallenge",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AchievementChallenge",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AchievementChallenge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AchievementLadderUserState",
                table: "AchievementLadderUserState",
                column: "AchievementLadderUserStateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId",
                table: "UserSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLadderUserState_UserId_LadderId",
                table: "AchievementLadderUserState",
                columns: new[] { "UserId", "LadderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId",
                table: "AchievementChallengeCompletion",
                column: "ChallengeId");
        }
    }
}
