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
            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "UsageRate",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "ProductOrderDiscount",
                newName: "Discount");

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumPaymentPercentage",
                table: "Reservation",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "IntentOrder",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumPaymentPercentage",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "IntentOrder");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "UsageRate",
                newName: "DiscountAmount");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "ProductOrderDiscount",
                newName: "DiscountAmount");
        }
    }
}
