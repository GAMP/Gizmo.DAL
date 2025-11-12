using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class update11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentIntentOrder_ProductOrder_ProductOrderId",
                table: "PaymentIntentOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                table: "ReservationProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_PaymentIntentOrder_ProductOrderId",
                table: "PaymentIntentOrder");

            migrationBuilder.DropColumn(
                name: "ProductOrderId",
                table: "PaymentIntentOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                table: "ReservationProductOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                table: "ReservationProductOrder");

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderId",
                table: "PaymentIntentOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntentOrder_ProductOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                table: "ReservationProductOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
