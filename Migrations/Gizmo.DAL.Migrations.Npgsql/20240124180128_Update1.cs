using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportPreset",
                columns: table => new
                {
                    ReportPresetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Report = table.Column<Guid>(type: "uuid", nullable: false),
                    Range = table.Column<int>(type: "integer", nullable: false),
                    Filters = table.Column<string>(type: "text", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPreset", x => x.ReportPresetId);
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ReportPreset_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_CreatedById",
                table: "ReportPreset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_ModifiedById",
                table: "ReportPreset",
                column: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportPreset");
        }
    }
}
