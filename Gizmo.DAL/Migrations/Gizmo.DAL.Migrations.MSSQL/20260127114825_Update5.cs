using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Refund",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Refund");
        }
    }
}
