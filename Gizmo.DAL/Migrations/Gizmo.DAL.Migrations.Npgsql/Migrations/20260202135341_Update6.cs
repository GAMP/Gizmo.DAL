using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDisableReason",
                columns: table => new
                {
                    UserDisableReasonId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisableReason", x => x.UserDisableReasonId);
                    table.ForeignKey(
                        name: "FK_UserDisableReason_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserDisableReason_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserDisableEntry",
                columns: table => new
                {
                    UserDisableEntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DisableReasonId = table.Column<int>(type: "integer", nullable: true),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisableEntry", x => x.UserDisableEntryId);
                    table.ForeignKey(
                        name: "FK_UserDisableEntry_UserDisableReason_DisableReasonId",
                        column: x => x.DisableReasonId,
                        principalTable: "UserDisableReason",
                        principalColumn: "UserDisableReasonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserDisableEntry_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDisableEntry_CreatedById",
                table: "UserDisableEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDisableEntry_DisableReasonId",
                table: "UserDisableEntry",
                column: "DisableReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDisableReason_CreatedById",
                table: "UserDisableReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDisableReason_ModifiedById",
                table: "UserDisableReason",
                column: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDisableEntry");

            migrationBuilder.DropTable(
                name: "UserDisableReason");
        }
    }
}
