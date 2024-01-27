using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    City = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Info = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Branch_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ReportPreset",
                columns: table => new
                {
                    ReportPresetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Report = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Filters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPreset", x => x.ReportPresetId);
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserOperatorBranch",
                columns: table => new
                {
                    OperatorBranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperatorBranch", x => x.OperatorBranchId);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchId",
                table: "Branch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CreatedById",
                table: "Branch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Guid",
                table: "Branch",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_ModifiedById",
                table: "Branch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Name",
                table: "Branch",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_CreatedById",
                table: "ReportPreset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_ModifiedById",
                table: "ReportPreset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_Name",
                table: "ReportPreset",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_BranchId_OperatorId",
                table: "UserOperatorBranch",
                columns: new[] { "BranchId", "OperatorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_CreatedById",
                table: "UserOperatorBranch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_ModifiedById",
                table: "UserOperatorBranch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_OperatorId",
                table: "UserOperatorBranch",
                column: "OperatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportPreset");

            migrationBuilder.DropTable(
                name: "UserOperatorBranch");

            migrationBuilder.DropTable(
                name: "Branch");
        }
    }
}
