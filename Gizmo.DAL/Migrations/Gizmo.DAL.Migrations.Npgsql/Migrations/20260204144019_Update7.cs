using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserDisableReasonId",
                table: "UserMemberDisableReason",
                newName: "UserMemberDisableReasonId");

            migrationBuilder.RenameColumn(
                name: "UserDisableEntryId",
                table: "UserMemberDisableEntry",
                newName: "UserMemberDisableEntryId");

            migrationBuilder.AddColumn<int>(
                name: "AcknowledgeState",
                table: "UserMemberDisableEntry",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcknowledgedDate",
                table: "UserMemberDisableEntry",
                type: "timestamp without time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<bool>(
                name: "IsLoginAgeRatingEnabled",
                table: "UserGroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProductAgeRatingEnabled",
                table: "UserGroup",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcknowledgeState",
                table: "UserMemberDisableEntry");

            migrationBuilder.DropColumn(
                name: "AcknowledgedDate",
                table: "UserMemberDisableEntry");

            migrationBuilder.DropColumn(
                name: "IsLoginAgeRatingEnabled",
                table: "UserGroup");

            migrationBuilder.DropColumn(
                name: "IsProductAgeRatingEnabled",
                table: "UserGroup");

            migrationBuilder.RenameColumn(
                name: "UserMemberDisableReasonId",
                table: "UserMemberDisableReason",
                newName: "UserDisableReasonId");

            migrationBuilder.RenameColumn(
                name: "UserMemberDisableEntryId",
                table: "UserMemberDisableEntry",
                newName: "UserDisableEntryId");
        }
    }
}
