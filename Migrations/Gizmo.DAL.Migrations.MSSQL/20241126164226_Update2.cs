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
            migrationBuilder.RenameColumn(
                name: "HasWorkingSchedule",
                table: "Branch",
                newName: "HasBusinessSchedule");

            migrationBuilder.CreateTable(
                name: "UserApiKey",
                columns: table => new
                {
                    ApiKey = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApiKey", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserApiKey_UserOperator_UserId",
                        column: x => x.UserId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSet",
                columns: table => new
                {
                    UserPermissionSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSet", x => x.UserPermissionSetId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSetPermission",
                columns: table => new
                {
                    UserPermissionSetPermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionSetId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSetPermission", x => x.UserPermissionSetPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSetPermission_UserPermissionSet_PermissionSetId",
                        column: x => x.PermissionSetId,
                        principalTable: "UserPermissionSet",
                        principalColumn: "UserPermissionSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApiKey_ApiKey",
                table: "UserApiKey",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApiKey_UserId",
                table: "UserApiKey",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_CreatedById",
                table: "UserPermissionSet",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_ModifiedById",
                table: "UserPermissionSet",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_Name",
                table: "UserPermissionSet",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSetPermission_PermissionSetId_Type_Value",
                table: "UserPermissionSetPermission",
                columns: new[] { "PermissionSetId", "Type", "Value" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserApiKey");

            migrationBuilder.DropTable(
                name: "UserPermissionSetPermission");

            migrationBuilder.DropTable(
                name: "UserPermissionSet");

            migrationBuilder.RenameColumn(
                name: "HasBusinessSchedule",
                table: "Branch",
                newName: "HasWorkingSchedule");
        }
    }
}
