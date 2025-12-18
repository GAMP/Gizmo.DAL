using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QrDisplayNumber",
                table: "Register",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_DepositPaymentId",
                table: "IntentOrderDeposit",
                column: "DepositPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrderDeposit_DepositPayment_DepositPaymentId",
                table: "IntentOrderDeposit",
                column: "DepositPaymentId",
                principalTable: "DepositPayment",
                principalColumn: "DepositPaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntentOrderDeposit_DepositPayment_DepositPaymentId",
                table: "IntentOrderDeposit");

            migrationBuilder.DropIndex(
                name: "IX_IntentOrderDeposit_DepositPaymentId",
                table: "IntentOrderDeposit");

            migrationBuilder.DropColumn(
                name: "QrDisplayNumber",
                table: "Register");
        }
    }
}
