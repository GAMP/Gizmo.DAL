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
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "StockTransaction",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_BranchId",
                table: "StockTransaction",
                newName: "IX_StockTransaction_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Stock_StockId",
                table: "StockTransaction",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Stock_StockId",
                table: "StockTransaction");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "StockTransaction",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransaction_StockId",
                table: "StockTransaction",
                newName: "IX_StockTransaction_BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Branch_BranchId",
                table: "StockTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
