using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class update15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StockCountId",
                table: "StockCountEntry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Expected",
                table: "StockCountEntry",
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Difference",
                table: "StockCountEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Actual",
                table: "StockCountEntry",
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

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "StockCountEntry",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<int>(
                name: "UnexpectedEntries",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StockCount",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.CreateTable(
                name: "StockCountAdjustement",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    AdjustmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountAdjustement", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustement_InventoryAdjustment_AdjustmentId",
                        column: x => x.AdjustmentId,
                        principalTable: "InventoryAdjustment",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustement_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCountInbound",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "int", nullable: false),
                    InboundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountInbound", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCountInbound_InventoryInbound_InboundId",
                        column: x => x.InboundId,
                        principalTable: "InventoryInbound",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCountInbound_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockCountAdjustement_AdjustmentId",
                table: "StockCountAdjustement",
                column: "AdjustmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCountInbound_InboundId",
                table: "StockCountInbound",
                column: "InboundId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockCountAdjustement");

            migrationBuilder.DropTable(
                name: "StockCountInbound");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "StockCountEntry");

            migrationBuilder.AlterColumn<int>(
                name: "StockCountId",
                table: "StockCountEntry",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Expected",
                table: "StockCountEntry",
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Difference",
                table: "StockCountEntry",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Actual",
                table: "StockCountEntry",
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

            migrationBuilder.AlterColumn<int>(
                name: "UnexpectedEntries",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "StockCount",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StockCount",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 4);
        }
    }
}
