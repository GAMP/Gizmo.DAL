using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "RegisterTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE [dbo].[RegisterTransaction] Set BranchId=1;");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_BranchId",
                table: "RegisterTransaction",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_Branch_BranchId",
                table: "RegisterTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTransaction_Branch_BranchId",
                table: "RegisterTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTransaction_BranchId",
                table: "RegisterTransaction");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "RegisterTransaction");
        }
    }
}
