using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Integration",
                columns: table => new
                {
                    IntegrationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    TypeGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PublicId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigJson = table.Column<string>(type: "text", nullable: true),
                    ConfigSchemaVersion = table.Column<int>(type: "integer", nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integration", x => x.IntegrationId);
                    table.ForeignKey(
                        name: "FK_Integration_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integration_CreatedById",
                table: "Integration",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Integration_PublicId",
                table: "Integration",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Integration_TypeGuid",
                table: "Integration",
                column: "TypeGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integration");
        }
    }
}
