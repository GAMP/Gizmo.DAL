using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gizmo.DAL.Migrations.MSSQL
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeRestriction",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeFrom = table.Column<int>(type: "int", nullable: false),
                    AgeTo = table.Column<int>(type: "int", nullable: false),
                    DayMinuteFrom = table.Column<int>(type: "int", nullable: true),
                    DayMinuteTo = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestriction", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestriction_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AgeRestrictionLogin",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictionLogin", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestrictionLogin_AgeRestriction_AgeRestrictionId",
                        column: x => x.AgeRestrictionId,
                        principalTable: "AgeRestriction",
                        principalColumn: "AgeRestrictionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgeRestriction_CreatedById",
                table: "AgeRestriction",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeRestrictionLogin");

            migrationBuilder.DropTable(
                name: "AgeRestriction");
        }
    }
}
