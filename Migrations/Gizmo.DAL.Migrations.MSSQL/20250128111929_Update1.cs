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
            migrationBuilder.AddColumn<int>(
                name: "InventoryInboundId",
                table: "InventoryTransfer",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.CreateTable(
                name: "FileImage",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImage", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileImage_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_InventoryInbound_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId",
                principalTable: "InventoryInbound",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_InventoryInbound_InventoryInboundId",
                table: "InventoryTransfer");

            migrationBuilder.DropTable(
                name: "FileImage");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "InventoryInboundId",
                table: "InventoryTransfer");
        }
    }
}
