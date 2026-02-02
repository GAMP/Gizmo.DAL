using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMemberDisableReason",
                columns: table => new
                {
                    UserDisableReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMemberDisableReason", x => x.UserDisableReasonId);
                    table.ForeignKey(
                        name: "FK_UserMemberDisableReason_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserMemberDisableReason_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserMemberDisableEntry",
                columns: table => new
                {
                    UserDisableEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DisableReasonId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMemberDisableEntry", x => x.UserDisableEntryId);
                    table.ForeignKey(
                        name: "FK_UserMemberDisableEntry_UserMemberDisableReason_DisableReasonId",
                        column: x => x.DisableReasonId,
                        principalTable: "UserMemberDisableReason",
                        principalColumn: "UserDisableReasonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserMemberDisableEntry_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMemberDisableEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberDisableEntry_CreatedById",
                table: "UserMemberDisableEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberDisableEntry_DisableReasonId",
                table: "UserMemberDisableEntry",
                column: "DisableReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberDisableEntry_UserId",
                table: "UserMemberDisableEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberDisableReason_CreatedById",
                table: "UserMemberDisableReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberDisableReason_ModifiedById",
                table: "UserMemberDisableReason",
                column: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMemberDisableEntry");

            migrationBuilder.DropTable(
                name: "UserMemberDisableReason");
        }
    }
}
