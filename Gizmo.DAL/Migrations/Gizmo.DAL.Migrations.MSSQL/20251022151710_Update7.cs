using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FiscalReceiptId",
                table: "RegisterTransaction",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<int>(
                name: "FiscalReceiptStatus",
                table: "RegisterTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "RRN",
                table: "PaymentReceipt",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "PaymentReceipt",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<int>(
                name: "TerminalNumber",
                table: "PaymentReceipt",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PaymentReceipt",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "CompanionId",
                table: "FiscalReceipt",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "PrinterNumber",
                table: "FiscalReceipt",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_FiscalReceiptId",
                table: "RegisterTransaction",
                column: "FiscalReceiptId",
                unique: true,
                filter: "[FiscalReceiptId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_CompanionId",
                table: "PaymentReceipt",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceipt_CompanionId",
                table: "FiscalReceipt",
                column: "CompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_Companion_CompanionId",
                table: "FiscalReceipt",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "CompanionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceipt_Companion_CompanionId",
                table: "PaymentReceipt",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "CompanionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_FiscalReceipt_FiscalReceiptId",
                table: "RegisterTransaction",
                column: "FiscalReceiptId",
                principalTable: "FiscalReceipt",
                principalColumn: "FiscalReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiscalReceipt_Companion_CompanionId",
                table: "FiscalReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentReceipt_Companion_CompanionId",
                table: "PaymentReceipt");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterTransaction_FiscalReceipt_FiscalReceiptId",
                table: "RegisterTransaction");

            migrationBuilder.DropIndex(
                name: "IX_RegisterTransaction_FiscalReceiptId",
                table: "RegisterTransaction");

            migrationBuilder.DropIndex(
                name: "IX_PaymentReceipt_CompanionId",
                table: "PaymentReceipt");

            migrationBuilder.DropIndex(
                name: "IX_FiscalReceipt_CompanionId",
                table: "FiscalReceipt");

            migrationBuilder.DropColumn(
                name: "FiscalReceiptId",
                table: "RegisterTransaction");

            migrationBuilder.DropColumn(
                name: "FiscalReceiptStatus",
                table: "RegisterTransaction");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "PaymentReceipt");

            migrationBuilder.DropColumn(
                name: "TerminalNumber",
                table: "PaymentReceipt");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PaymentReceipt");

            migrationBuilder.DropColumn(
                name: "CompanionId",
                table: "FiscalReceipt");

            migrationBuilder.DropColumn(
                name: "PrinterNumber",
                table: "FiscalReceipt");

            migrationBuilder.AlterColumn<string>(
                name: "RRN",
                table: "PaymentReceipt",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 2);
        }
    }
}
