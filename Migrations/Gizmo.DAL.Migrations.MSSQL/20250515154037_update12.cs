using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class update12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleReportRecipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    ScheduleReportId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReportRecipient", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_ScheduleReport_ScheduleReportId",
                        column: x => x.ScheduleReportId,
                        principalTable: "ScheduleReport",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleReportRecipient_UserOperator_UserId",
                        column: x => x.UserId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_ScheduleReportId_UserId",
                table: "ScheduleReportRecipient",
                columns: new[] { "ScheduleReportId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_UserId",
                table: "ScheduleReportRecipient",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleReportRecipient");
        }
    }
}
