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
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransferEntry_StockTransaction_TransferStockTransactionId",
                table: "InventoryTransferEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransferEntry_Stock_TransferStockId",
                table: "InventoryTransferEntry");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransferEntry_TransferStockId",
                table: "InventoryTransferEntry");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransferEntry_TransferStockTransactionId",
                table: "InventoryTransferEntry");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "TransferStockId",
                table: "InventoryTransferEntry");

            migrationBuilder.DropColumn(
                name: "TransferStockTransactionId",
                table: "InventoryTransferEntry");

            migrationBuilder.AlterColumn<int>(
                name: "TransferStockId",
                table: "InventoryTransfer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryInboundId",
                table: "InventoryTransfer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "InventoryInboundEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "InventoryInboundEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "InventoryTransferEntryId",
                table: "InventoryInboundEntry",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "InventoryInbound",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "InventoryTransferId",
                table: "InventoryInbound",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "InventoryEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InventoryEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "InventoryEntry",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InventoryAdjustment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "AdjustmentType",
                table: "InventoryAdjustment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "InventoryAdjustment",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId",
                unique: true,
                filter: "[InventoryInboundId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInboundEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry",
                column: "InventoryTransferEntryId",
                unique: true,
                filter: "[InventoryTransferEntryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInbound_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                unique: true,
                filter: "[InventoryTransferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_Name",
                table: "InventoryAdjustmentReason",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                principalTable: "InventoryTransfer",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInboundEntry_InventoryTransferEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry",
                column: "InventoryTransferEntryId",
                principalTable: "InventoryTransferEntry",
                principalColumn: "InventoryEntryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInboundEntry_InventoryTransferEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer");

            migrationBuilder.DropIndex(
                name: "IX_InventoryInboundEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry");

            migrationBuilder.DropIndex(
                name: "IX_InventoryInbound_InventoryTransferId",
                table: "InventoryInbound");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAdjustmentReason_Name",
                table: "InventoryAdjustmentReason");

            migrationBuilder.DropColumn(
                name: "InventoryTransferEntryId",
                table: "InventoryInboundEntry");

            migrationBuilder.DropColumn(
                name: "InventoryTransferId",
                table: "InventoryInbound");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "InventoryAdjustment");

            migrationBuilder.AddColumn<int>(
                name: "TransferStockId",
                table: "InventoryTransferEntry",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "TransferStockTransactionId",
                table: "InventoryTransferEntry",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "TransferStockId",
                table: "InventoryTransfer",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryInboundId",
                table: "InventoryTransfer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "InventoryInboundEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "InventoryInboundEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "InventoryInbound",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ShiftId",
                table: "InventoryEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InventoryEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "InventoryEntry",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "InventoryAdjustmentEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "InventoryAdjustment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "AdjustmentType",
                table: "InventoryAdjustment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferStockId",
                table: "InventoryTransferEntry",
                column: "TransferStockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferStockTransactionId",
                table: "InventoryTransferEntry",
                column: "TransferStockTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferEntry_StockTransaction_TransferStockTransactionId",
                table: "InventoryTransferEntry",
                column: "TransferStockTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferEntry_Stock_TransferStockId",
                table: "InventoryTransferEntry",
                column: "TransferStockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
