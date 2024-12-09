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
            migrationBuilder.DropIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register");

            migrationBuilder.CreateIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register",
                columns: new[] { "Name", "BranchId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register");

            migrationBuilder.CreateIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register",
                columns: new[] { "Name", "BranchId" });
        }
    }
}
