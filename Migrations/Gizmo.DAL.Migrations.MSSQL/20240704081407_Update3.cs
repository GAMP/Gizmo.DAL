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
                name: "IX_HostLayoutGroup_Name",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_Name",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_Device_Name",
                table: "Device");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "StockTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Register",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "HostLayoutGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "HostGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Asset",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_BranchId",
                table: "StockTransaction",
                column: "BranchId");

            migrationBuilder.Sql(Scripts.BRANCH_SET);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BranchId",
                table: "Shift",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_BranchId",
                table: "Register",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register",
                columns: new[] { "Name", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_BranchId",
                table: "HostLayoutGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_Name_BranchId",
                table: "HostLayoutGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_Name_BranchId",
                table: "HostGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_BranchId",
                table: "Device",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name_BranchId",
                table: "Device",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_BranchId",
                table: "Asset",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Branch_BranchId",
                table: "Asset",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Branch_BranchId",
                table: "Device",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_Branch_BranchId",
                table: "HostLayoutGroup",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Branch_BranchId",
                table: "Reservation",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Branch_BranchId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_Branch_BranchId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostLayoutGroup_Branch_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Branch_BranchId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_BranchId",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Shift_BranchId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Register_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_HostLayoutGroup_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostLayoutGroup_Name_BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_HostGroup_Name_BranchId",
                table: "HostGroup");

            migrationBuilder.DropIndex(
                name: "IX_Device_BranchId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_Name_BranchId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Asset_BranchId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HostLayoutGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HostGroup");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Asset");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_Name",
                table: "HostLayoutGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_Name",
                table: "HostGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name",
                table: "Device",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
