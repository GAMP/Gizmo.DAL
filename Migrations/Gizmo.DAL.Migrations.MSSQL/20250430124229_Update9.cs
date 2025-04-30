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
            migrationBuilder.RenameColumn(
                name: "HideStart",
                table: "SecurityProfile",
                newName: "DisableStartMenu");

            migrationBuilder.AddColumn<int>(
                name: "LoginBlockAfterTime",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginBlockBeforeTime",
                table: "Reservation",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginBlockAfterTime",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LoginBlockBeforeTime",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "DisableStartMenu",
                table: "SecurityProfile",
                newName: "HideStart");
        }
    }
}
