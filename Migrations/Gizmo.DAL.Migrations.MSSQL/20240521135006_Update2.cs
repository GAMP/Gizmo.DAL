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
            migrationBuilder.AddColumn<int>(
                name: "FiscalReceiptPrinterNumber",
                table: "Register",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTerminalNumber",
                table: "Register",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "HostLayoutGroupLayout",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 9);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "HostLayoutGroupLayout",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiscalReceiptPrinterNumber",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "PaymentTerminalNumber",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "HostLayoutGroupLayout");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "HostLayoutGroupLayout");
        }
    }
}
