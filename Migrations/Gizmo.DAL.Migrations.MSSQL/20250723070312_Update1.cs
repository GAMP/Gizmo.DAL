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
            migrationBuilder.AddColumn<string>(
                name: "PaymentLinkUrl",
                table: "PaymentIntent",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentLinkUrl",
                table: "PaymentIntent");
        }
    }
}
