using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillProfileId",
                table: "HostGroup",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "ClientOptionsId",
                table: "HostGroup",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.CreateTable(
                name: "ClientOptions",
                columns: table => new
                {
                    ClientOptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 65535, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOptions", x => x.ClientOptionsId);
                    table.ForeignKey(
                        name: "FK_ClientOptions_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ClientOptions_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RefundReceipt",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false),
                    RRN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    RegisterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundReceipt", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_RefundReceipt_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BillProfileId",
                table: "HostGroup",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_ClientOptionsId",
                table: "HostGroup",
                column: "ClientOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOptions_CreatedById",
                table: "ClientOptions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOptions_ModifiedById",
                table: "ClientOptions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_CreatedById",
                table: "RefundReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_RegisterId",
                table: "RefundReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_ShiftId",
                table: "RefundReceipt",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_BillProfile_BillProfileId",
                table: "HostGroup",
                column: "BillProfileId",
                principalTable: "BillProfile",
                principalColumn: "BillProfileId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_ClientOptions_ClientOptionsId",
                table: "HostGroup",
                column: "ClientOptionsId",
                principalTable: "ClientOptions",
                principalColumn: "ClientOptionsId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_BillProfile_BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_ClientOptions_ClientOptionsId",
                table: "HostGroup");

            migrationBuilder.DropTable(
                name: "ClientOptions");

            migrationBuilder.DropTable(
                name: "RefundReceipt");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_ClientOptionsId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "BillProfileId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "ClientOptionsId",
                table: "HostGroup");
        }
    }
}
