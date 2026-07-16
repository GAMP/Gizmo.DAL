using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
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

            migrationBuilder.DropIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId",
                table: "AchievementChallengeCompletion");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedTime",
                table: "AchievementChallengeCompletion",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "GlobalOccurrence",
                table: "AchievementChallengeCompletion",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<int>(
                name: "Options",
                table: "AchievementChallenge",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "MaxCompletions",
                table: "AchievementChallenge",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "AchievementChallenge",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AchievementChallenge",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AchievementChallenge",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "GlobalMaxCompletions",
                table: "AchievementChallenge",
                type: "integer",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

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

            migrationBuilder.DropIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId_GlobalOccurrence",
                table: "AchievementChallengeCompletion");

            migrationBuilder.DropColumn(
                name: "GlobalOccurrence",
                table: "AchievementChallengeCompletion");

            migrationBuilder.DropColumn(
                name: "GlobalMaxCompletions",
                table: "AchievementChallenge");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedTime",
                table: "AchievementChallengeCompletion",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "Options",
                table: "AchievementChallenge",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "MaxCompletions",
                table: "AchievementChallenge",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisabled",
                table: "AchievementChallenge",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AchievementChallenge",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "AchievementChallenge",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId",
                table: "UserSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementChallengeCompletion_ChallengeId",
                table: "AchievementChallengeCompletion",
                column: "ChallengeId");
        }
    }
}
