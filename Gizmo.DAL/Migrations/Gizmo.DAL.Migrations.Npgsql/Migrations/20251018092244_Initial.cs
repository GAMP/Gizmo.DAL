using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gizmo.DAL.Migrations.Npgsql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HostNumber = table.Column<int>(type: "integer", nullable: true),
                    Hostname = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    ModuleType = table.Column<int>(type: "integer", nullable: false),
                    ModuleVersion = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    MessageType = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "LogException",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false),
                    ExceptionData = table.Column<byte[]>(type: "bytea", maxLength: 65535, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogException", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_LogException_Log_LogId",
                        column: x => x.LogId,
                        principalTable: "Log",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgeRestriction",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AgeFrom = table.Column<int>(type: "integer", nullable: false),
                    AgeTo = table.Column<int>(type: "integer", nullable: false),
                    DayMinuteFrom = table.Column<int>(type: "integer", nullable: true),
                    DayMinuteTo = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestriction", x => x.AgeRestrictionId);
                });

            migrationBuilder.CreateTable(
                name: "AgeRestrictionLogin",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AgeRestrictionProduct",
                columns: table => new
                {
                    AgeRestrictionId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictionProduct", x => x.AgeRestrictionId);
                    table.ForeignKey(
                        name: "FK_AgeRestrictionProduct_AgeRestriction_AgeRestrictionId",
                        column: x => x.AgeRestrictionId,
                        principalTable: "AgeRestriction",
                        principalColumn: "AgeRestrictionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: true),
                    DeveloperId = table.Column<int>(type: "integer", nullable: true),
                    AppCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Version = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    AgeRating = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultExecutableId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                });

            migrationBuilder.CreateTable(
                name: "AppCategory",
                columns: table => new
                {
                    AppCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategory", x => x.AppCategoryId);
                    table.ForeignKey(
                        name: "FK_AppCategory_AppCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppCategory",
                        principalColumn: "AppCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "AppEnterprise",
                columns: table => new
                {
                    AppEnterpriseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEnterprise", x => x.AppEnterpriseId);
                });

            migrationBuilder.CreateTable(
                name: "AppExe",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<int>(type: "integer", nullable: false),
                    Caption = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ExecutablePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Arguments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WorkingDirectory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Modes = table.Column<int>(type: "integer", nullable: false),
                    RunMode = table.Column<int>(type: "integer", nullable: false),
                    DefaultDeploymentId = table.Column<int>(type: "integer", nullable: true),
                    ReservationType = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Accessible = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExe", x => x.AppExeId);
                    table.ForeignKey(
                        name: "FK_AppExe_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppExeBranch",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeBranch", x => new { x.AppExeId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_AppExeBranch_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeCdImage",
                columns: table => new
                {
                    AppExeCdImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MountOptions = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DeviceId = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    CheckExitCode = table.Column<bool>(type: "boolean", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeCdImage", x => x.AppExeCdImageId);
                    table.ForeignKey(
                        name: "FK_AppExeCdImage_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeDeployment",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    DeploymentId = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeDeployment", x => new { x.AppExeId, x.DeploymentId });
                    table.ForeignKey(
                        name: "FK_AppExeDeployment_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeImage",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeImage", x => x.AppExeId);
                    table.ForeignKey(
                        name: "FK_AppExeImage_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeLicense",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    LicenseId = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeLicense", x => new { x.AppExeId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_AppExeLicense_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeMaxUser",
                columns: table => new
                {
                    AppExeMaxUserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    Mode = table.Column<int>(type: "integer", nullable: false),
                    MaxUsers = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeMaxUser", x => x.AppExeMaxUserId);
                    table.ForeignKey(
                        name: "FK_AppExeMaxUser_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExePersonalFile",
                columns: table => new
                {
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    PersonalFileId = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExePersonalFile", x => new { x.AppExeId, x.PersonalFileId });
                    table.ForeignKey(
                        name: "FK_AppExePersonalFile_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppExeTask",
                columns: table => new
                {
                    AppExeTaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activation = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    TaskBaseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExeTask", x => x.AppExeTaskId);
                    table.ForeignKey(
                        name: "FK_AppExeTask_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppGroup",
                columns: table => new
                {
                    AppGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroup", x => x.AppGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AppGroupApp",
                columns: table => new
                {
                    AppGroupId = table.Column<int>(type: "integer", nullable: false),
                    AppId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroupApp", x => new { x.AppGroupId, x.AppId });
                    table.ForeignKey(
                        name: "FK_AppGroupApp_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppGroupApp_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppImage",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImage", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_AppImage_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppLink",
                columns: table => new
                {
                    AppLinkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<int>(type: "integer", nullable: false),
                    Caption = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLink", x => x.AppLinkId);
                    table.ForeignKey(
                        name: "FK_AppLink_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRating",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRating", x => new { x.AppId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppRating_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppStat",
                columns: table => new
                {
                    AppStatId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<int>(type: "integer", nullable: false),
                    AppExeId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Span = table.Column<double>(type: "double precision", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStat", x => x.AppStatId);
                    table.ForeignKey(
                        name: "FK_AppStat_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStat_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetTypeId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Tag = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SmartCardUID = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Barcode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SerialNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "AssetTransaction",
                columns: table => new
                {
                    AssetTransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetTypeId = table.Column<int>(type: "integer", nullable: false),
                    AssetTypeName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    AssetId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CheckedInById = table.Column<int>(type: "integer", nullable: true),
                    CheckInTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTransaction", x => x.AssetTransactionId);
                    table.ForeignKey(
                        name: "FK_AssetTransaction_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetType",
                columns: table => new
                {
                    AssetTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    PartNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetType", x => x.AssetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceRequest",
                columns: table => new
                {
                    AssistanceRequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    AssistanceRequestTypeId = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequest", x => x.AssistanceRequestId);
                });

            migrationBuilder.CreateTable(
                name: "AssistanceRequestType",
                columns: table => new
                {
                    AssistanceRequestTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceRequestType", x => x.AssistanceRequestTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    FriendlyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "BillProfile",
                columns: table => new
                {
                    BillProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillProfile", x => x.BillProfileId);
                });

            migrationBuilder.CreateTable(
                name: "BillRate",
                columns: table => new
                {
                    BillRateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillProfileId = table.Column<int>(type: "integer", nullable: false),
                    StartFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    ChargeEvery = table.Column<int>(type: "integer", nullable: false),
                    ChargeAfter = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRate", x => x.BillRateId);
                    table.ForeignKey(
                        name: "FK_BillRate_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRatePeriodDay",
                columns: table => new
                {
                    BillRatePeriodDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillRateId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRatePeriodDay", x => x.BillRatePeriodDayId);
                    table.ForeignKey(
                        name: "FK_BillRatePeriodDay_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRateStep",
                columns: table => new
                {
                    BillRateStepId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillRateId = table.Column<int>(type: "integer", nullable: false),
                    Minute = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<int>(type: "integer", nullable: false),
                    Charge = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    TargetMinute = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRateStep", x => x.BillRateStepId);
                    table.ForeignKey(
                        name: "FK_BillRateStep_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillRatePeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false),
                    PeriodDayId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillRatePeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_BillRatePeriodDayTime_BillRatePeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "BillRatePeriodDay",
                        principalColumn: "BillRatePeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    City = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Region = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Phone = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WebSite = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Info = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TimeZone = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    HasBusinessSchedule = table.Column<bool>(type: "boolean", nullable: false),
                    BusinessDayStart = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    BusinessDayEnd = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    BusinessStartWeekDay = table.Column<int>(type: "integer", nullable: true),
                    BusinessEndWeekDay = table.Column<int>(type: "integer", nullable: true),
                    IsFiscalizationEnabled = table.Column<bool>(type: "boolean", nullable: true),
                    BusinessVATId = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    TaxSystem = table.Column<int>(type: "integer", nullable: true),
                    GoodsTaxSystem = table.Column<int>(type: "integer", nullable: true),
                    ServicesTaxSystem = table.Column<int>(type: "integer", nullable: true),
                    TreatDepositsAsService = table.Column<bool>(type: "boolean", nullable: true),
                    DepositServiceDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TimeBasedServiceVATRate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    DepositVATRate = table.Column<int>(type: "integer", nullable: true),
                    DepositAdvancePaymentType = table.Column<int>(type: "integer", nullable: true),
                    CompanionId = table.Column<int>(type: "integer", nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    DisableTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "BundleProduct",
                columns: table => new
                {
                    BundleProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductBundleId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleProduct", x => x.BundleProductId);
                });

            migrationBuilder.CreateTable(
                name: "BundleProductUserPrice",
                columns: table => new
                {
                    BundleProductUserPriceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    BundleProductId = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleProductUserPrice", x => x.BundleProductUserPriceId);
                    table.ForeignKey(
                        name: "FK_BundleProductUserPrice_BundleProduct_BundleProductId",
                        column: x => x.BundleProductId,
                        principalTable: "BundleProduct",
                        principalColumn: "BundleProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientOptions",
                columns: table => new
                {
                    ClientOptionsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Data = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientOptions", x => x.ClientOptionsId);
                });

            migrationBuilder.CreateTable(
                name: "ClientTask",
                columns: table => new
                {
                    ClientTaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activation = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    TaskBaseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTask", x => x.ClientTaskId);
                });

            migrationBuilder.CreateTable(
                name: "Companion",
                columns: table => new
                {
                    CompanionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companion", x => x.CompanionId);
                });

            migrationBuilder.CreateTable(
                name: "Deployment",
                columns: table => new
                {
                    DeploymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Destination = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ExcludeDirectories = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    ExcludeFiles = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    IncludeDirectories = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    IncludeFiles = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    RegistryString = table.Column<string>(type: "text", maxLength: 16777215, nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    ComparisonLevel = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deployment", x => x.DeploymentId);
                });

            migrationBuilder.CreateTable(
                name: "DeploymentDeployment",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "integer", nullable: false),
                    ChildId = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeploymentDeployment", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_DeploymentDeployment_Deployment_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Deployment",
                        principalColumn: "DeploymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeploymentDeployment_Deployment_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Deployment",
                        principalColumn: "DeploymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepositPayment",
                columns: table => new
                {
                    DepositPaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepositTransactionId = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPayment", x => x.DepositPaymentId);
                    table.ForeignKey(
                        name: "FK_DepositPayment_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                });

            migrationBuilder.CreateTable(
                name: "DepositTransaction",
                columns: table => new
                {
                    DepositTransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositTransaction", x => x.DepositTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHdmi",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    UniqueId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHdmi", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_DeviceHdmi_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHost",
                columns: table => new
                {
                    DeviceHostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHost", x => x.DeviceHostId);
                    table.ForeignKey(
                        name: "FK_DeviceHost_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ApplyType = table.Column<int>(type: "integer", nullable: false),
                    CalculationType = table.Column<int>(type: "integer", nullable: false),
                    RewardType = table.Column<int>(type: "integer", nullable: false),
                    Requirement = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "DiscountBranch",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountBranch", x => new { x.DiscountId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_DiscountBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountBranch_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriod",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriod", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DiscountPeriod_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriodDay",
                columns: table => new
                {
                    DiscountPeriodDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountPeriodId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriodDay", x => x.DiscountPeriodDayId);
                    table.ForeignKey(
                        name: "FK_DiscountPeriodDay_DiscountPeriod_DiscountPeriodId",
                        column: x => x.DiscountPeriodId,
                        principalTable: "DiscountPeriod",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountPeriodDayTime",
                columns: table => new
                {
                    DiscountPeriodDayId = table.Column<int>(type: "integer", nullable: false),
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountPeriodDayTime", x => new { x.DiscountPeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_DiscountPeriodDayTime_DiscountPeriodDay_DiscountPeriodDayId",
                        column: x => x.DiscountPeriodDayId,
                        principalTable: "DiscountPeriodDay",
                        principalColumn: "DiscountPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountGroup",
                columns: table => new
                {
                    DiscountGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountGroup", x => x.DiscountGroupId);
                });

            migrationBuilder.CreateTable(
                name: "DiscountGroupDiscount",
                columns: table => new
                {
                    DiscountGroupId = table.Column<int>(type: "integer", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountGroupDiscount", x => new { x.DiscountGroupId, x.DiscountId });
                    table.ForeignKey(
                        name: "FK_DiscountGroupDiscount_DiscountGroup_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalTable: "DiscountGroup",
                        principalColumn: "DiscountGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountGroupDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Maximum = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.FeedId);
                });

            migrationBuilder.CreateTable(
                name: "FeedBranch",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBranch", x => new { x.FeedId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_FeedBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBranch_Feed_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feed",
                        principalColumn: "FeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    MimeType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Hash = table.Column<byte[]>(type: "bytea", maxLength: 32, nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "FileDocument",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDocument", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileDocument_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileDocument_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileImage",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FiscalReceipt",
                columns: table => new
                {
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TaxSystem = table.Column<int>(type: "integer", nullable: true),
                    DocumentNumber = table.Column<int>(type: "integer", nullable: true),
                    Signature = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalReceipt", x => x.FiscalReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "Host",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    HostGroupId = table.Column<int>(type: "integer", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    IconId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Host", x => x.HostId);
                });

            migrationBuilder.CreateTable(
                name: "HostComputer",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    Hostname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MACAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostComputer", x => x.HostId);
                    table.ForeignKey(
                        name: "FK_HostComputer_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostEndpoint",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    MaximumUsers = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostEndpoint", x => x.HostId);
                    table.ForeignKey(
                        name: "FK_HostEndpoint_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroup",
                columns: table => new
                {
                    HostGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    AppGroupId = table.Column<int>(type: "integer", nullable: true),
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: true),
                    SkinName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    DefaultGuestGroupId = table.Column<int>(type: "integer", nullable: true),
                    BillProfileId = table.Column<int>(type: "integer", nullable: true),
                    ClientOptionsId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroup", x => x.HostGroupId);
                    table.ForeignKey(
                        name: "FK_HostGroup_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                    table.ForeignKey(
                        name: "FK_HostGroup_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_HostGroup_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostGroup_ClientOptions_ClientOptionsId",
                        column: x => x.ClientOptionsId,
                        principalTable: "ClientOptions",
                        principalColumn: "ClientOptionsId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupUserBillProfile",
                columns: table => new
                {
                    HostGroupUserBillProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillProfileId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupUserBillProfile", x => x.HostGroupUserBillProfileId);
                    table.ForeignKey(
                        name: "FK_HostGroupUserBillProfile_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HostGroupUserBillProfile_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupWaitingLine",
                columns: table => new
                {
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    TimeOutOptions = table.Column<int>(type: "integer", nullable: false),
                    EnablePriorities = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupWaitingLine", x => x.HostGroupId);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLine_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupWaitingLineEntry",
                columns: table => new
                {
                    HostGroupWaitingLineEntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    IsManualPosition = table.Column<bool>(type: "boolean", nullable: false),
                    TimeInLine = table.Column<double>(type: "double precision", nullable: false),
                    ReadyTime = table.Column<double>(type: "double precision", nullable: false),
                    IsReadyTimedOut = table.Column<bool>(type: "boolean", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupWaitingLineEntry", x => x.HostGroupWaitingLineEntryId);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroupWaitingLine_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroupWaitingLine",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroup",
                columns: table => new
                {
                    HostLayoutGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroup", x => x.HostLayoutGroupId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroup_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroupImage",
                columns: table => new
                {
                    HostLayoutGroupId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroupImage", x => x.HostLayoutGroupId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupImage_HostLayoutGroup_HostLayoutGroupId",
                        column: x => x.HostLayoutGroupId,
                        principalTable: "HostLayoutGroup",
                        principalColumn: "HostLayoutGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroupLayout",
                columns: table => new
                {
                    HostLayoutGroupLayoutId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HostLayoutGroupId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Column = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroupLayout", x => x.HostLayoutGroupLayoutId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_HostLayoutGroup_HostLayoutGroupId",
                        column: x => x.HostLayoutGroupId,
                        principalTable: "HostLayoutGroup",
                        principalColumn: "HostLayoutGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    IconId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.IconId);
                });

            migrationBuilder.CreateTable(
                name: "IntentOrder",
                columns: table => new
                {
                    IntentOrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentIntentOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentOrder", x => x.IntentOrderId);
                });

            migrationBuilder.CreateTable(
                name: "IntentOrderDeposit",
                columns: table => new
                {
                    IntentOrderDepositId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentIntentOrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DepositPaymentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntentOrderDeposit", x => x.IntentOrderDepositId);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustment",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    AdjustmentType = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustment", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustment_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "integer", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    AdjustmentReasonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustmentEntry", x => x.InventoryEntryId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustmentReason",
                columns: table => new
                {
                    InventoryAdjustmentReasonId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustmentReason", x => x.InventoryAdjustmentReasonId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryDocument",
                columns: table => new
                {
                    InventoryDocumentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    FileDocumentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryDocument", x => x.InventoryDocumentId);
                    table.ForeignKey(
                        name: "FK_InventoryDocument_FileDocument_FileDocumentId",
                        column: x => x.FileDocumentId,
                        principalTable: "FileDocument",
                        principalColumn: "FileId");
                    table.ForeignKey(
                        name: "FK_InventoryDocument_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    StockTransactionId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryEntry_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInbound",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    InventoryTransferId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInbound", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryInbound_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryInboundEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "integer", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    InventoryTransferEntryId = table.Column<int>(type: "integer", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInboundEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryInboundEntry_InventoryEntry_InventoryEntryId",
                        column: x => x.InventoryEntryId,
                        principalTable: "InventoryEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransfer",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "integer", nullable: false),
                    TransferStockId = table.Column<int>(type: "integer", nullable: false),
                    InventoryInboundId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransfer", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_InventoryInbound_InventoryInboundId",
                        column: x => x.InventoryInboundId,
                        principalTable: "InventoryInbound",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransferEntry",
                columns: table => new
                {
                    InventoryEntryId = table.Column<int>(type: "integer", nullable: false),
                    TransferReasonId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransferEntry", x => x.InventoryEntryId);
                    table.ForeignKey(
                        name: "FK_InventoryTransferEntry_InventoryEntry_InventoryEntryId",
                        column: x => x.InventoryEntryId,
                        principalTable: "InventoryEntry",
                        principalColumn: "InventoryEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransferReason",
                columns: table => new
                {
                    InventoryTransferReasonId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransferReason", x => x.InventoryTransferReasonId);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "integer", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Outstanding = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    OutstandingPoints = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    SaleFiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    ReturnFiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceFiscalReceipt",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceFiscalReceiptId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFiscalReceipt", x => x.InvoiceFiscalReceiptId);
                    table.ForeignKey(
                        name: "FK_InvoiceFiscalReceipt_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceFiscalReceipt_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitListPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPointsPrice = table.Column<int>(type: "integer", nullable: false),
                    UnitPointsListPrice = table.Column<int>(type: "integer", nullable: true),
                    UnitCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    TaxRate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PreTaxTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    PointsAward = table.Column<int>(type: "integer", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PayType = table.Column<int>(type: "integer", nullable: false),
                    PointsTransactionId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ReservationId = table.Column<int>(type: "integer", nullable: true),
                    ReservationHostId = table.Column<int>(type: "integer", nullable: true),
                    ReservationSlot = table.Column<int>(type: "integer", nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineExtended",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    BundleLineId = table.Column<int>(type: "integer", nullable: true),
                    StockTransactionId = table.Column<int>(type: "integer", nullable: true),
                    StockReturnTransactionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineExtended", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineExtended_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineProduct",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    OrderLineId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineProduct", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineProduct_InvoiceLineExtended_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineExtended",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineReservationFee",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    OrderLineId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineReservationFee", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineReservationFee_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineSession",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    OrderLineId = table.Column<int>(type: "integer", nullable: false),
                    UsageSessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineSession", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineSession_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineTime",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    OrderLineId = table.Column<int>(type: "integer", nullable: false),
                    ProductTimeId = table.Column<int>(type: "integer", nullable: false),
                    IsDepleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsExpired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineTime", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineTime_InvoiceLineExtended_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineExtended",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineTimeFixed",
                columns: table => new
                {
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false),
                    OrderLineId = table.Column<int>(type: "integer", nullable: false),
                    IsDepleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineTimeFixed", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineTimeFixed_InvoiceLine_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLine",
                        principalColumn: "InvoiceLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayment",
                columns: table => new
                {
                    InvoicePaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.InvoicePaymentId);
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_InvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Assembly = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Plugin = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Settings = table.Column<byte[]>(type: "bytea", maxLength: 65535, nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.LicenseId);
                });

            migrationBuilder.CreateTable(
                name: "LicenseKey",
                columns: table => new
                {
                    LicenseKeyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LicenseId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<byte[]>(type: "bytea", maxLength: 65535, nullable: false),
                    Comment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AssignedHostId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseKey", x => x.LicenseKeyId);
                    table.ForeignKey(
                        name: "FK_LicenseKey_HostComputer_AssignedHostId",
                        column: x => x.AssignedHostId,
                        principalTable: "HostComputer",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_LicenseKey_License_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "License",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mapping",
                columns: table => new
                {
                    MappingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MountPoint = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Password = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapping", x => x.MappingId);
                });

            migrationBuilder.CreateTable(
                name: "MonetaryUnit",
                columns: table => new
                {
                    MonetaryUnitId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonetaryUnit", x => x.MonetaryUnitId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Data = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    Url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MediaUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BackgroundUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "NewsBranch",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsBranch", x => new { x.NewsId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_NewsBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsBranch_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "NewsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    Sevirity = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "text", maxLength: 16777215, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FocusType = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimed",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    Minute = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimed", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimed_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimedRemaining",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimedRemaining", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimedRemaining_NotificationTimed_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "NotificationTimed",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimedReservation",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimedReservation", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_NotificationTimedReservation_NotificationTimed_Notification~",
                        column: x => x.NotificationId,
                        principalTable: "NotificationTimed",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    AmountReceived = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    DepositTransactionId = table.Column<int>(type: "integer", nullable: true),
                    PointTransactionId = table.Column<int>(type: "integer", nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_Payment_DepositTransaction_DepositTransactionId",
                        column: x => x.DepositTransactionId,
                        principalTable: "DepositTransaction",
                        principalColumn: "DepositTransactionId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntent",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TransactionId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TransactionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Provider = table.Column<Guid>(type: "uuid", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Expiration = table.Column<int>(type: "integer", nullable: true),
                    ExpireAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntent", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntent_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_PaymentIntent_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntentDeposit",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "integer", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntentDeposit", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntentDeposit_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId");
                    table.ForeignKey(
                        name: "FK_PaymentIntentDeposit_PaymentIntent_PaymentIntentId",
                        column: x => x.PaymentIntentId,
                        principalTable: "PaymentIntent",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentIntentOrder",
                columns: table => new
                {
                    PaymentIntentId = table.Column<int>(type: "integer", nullable: false),
                    AutoComplete = table.Column<bool>(type: "boolean", nullable: false),
                    DisableReceiptPrinting = table.Column<bool>(type: "boolean", nullable: false),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntentOrder", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntentOrder_PaymentIntent_PaymentIntentId",
                        column: x => x.PaymentIntentId,
                        principalTable: "PaymentIntent",
                        principalColumn: "PaymentIntentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Surcharge = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    IsClient = table.Column<bool>(type: "boolean", nullable: false),
                    IsManager = table.Column<bool>(type: "boolean", nullable: false),
                    IsPortal = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentProvider = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceipt",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false),
                    RRN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceipt", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentReceipt_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalFile",
                columns: table => new
                {
                    PersonalFileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Caption = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Source = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Activation = table.Column<int>(type: "integer", nullable: false),
                    Deactivation = table.Column<int>(type: "integer", nullable: false),
                    MaxQuota = table.Column<int>(type: "integer", nullable: false),
                    CompressionLevel = table.Column<int>(type: "integer", nullable: false),
                    ExcludeDirectories = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    ExcludeFiles = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    IncludeDirectories = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    IncludeFiles = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    Accessible = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalFile", x => x.PersonalFileId);
                });

            migrationBuilder.CreateTable(
                name: "PluginLibrary",
                columns: table => new
                {
                    PluginLibraryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Scope = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginLibrary", x => x.PluginLibraryId);
                });

            migrationBuilder.CreateTable(
                name: "PointTransaction",
                columns: table => new
                {
                    PointTransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointTransaction", x => x.PointTransactionId);
                });

            migrationBuilder.CreateTable(
                name: "PresetReservationTime",
                columns: table => new
                {
                    PresetReservationTimeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetReservationTime", x => x.PresetReservationTimeId);
                });

            migrationBuilder.CreateTable(
                name: "PresetTimeSale",
                columns: table => new
                {
                    PresetTimeSaleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTimeSale", x => x.PresetTimeSaleId);
                });

            migrationBuilder.CreateTable(
                name: "PresetTimeSaleMoney",
                columns: table => new
                {
                    PresetTimeSaleMoneyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTimeSaleMoney", x => x.PresetTimeSaleMoneyId);
                });

            migrationBuilder.CreateTable(
                name: "PresetTopUp",
                columns: table => new
                {
                    PresetTopUpId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresetTopUp", x => x.PresetTopUpId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductBase",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    PointsPrice = table.Column<int>(type: "integer", nullable: true),
                    Barcode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    OrderOptions = table.Column<int>(type: "integer", nullable: false),
                    PurchaseOptions = table.Column<int>(type: "integer", nullable: false),
                    StockOptions = table.Column<int>(type: "integer", nullable: false),
                    StockAlert = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    StockProductId = table.Column<int>(type: "integer", nullable: true),
                    StockProductAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBase", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBase_ProductBase_StockProductId",
                        column: x => x.StockProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductBaseExtended",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBaseExtended", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBaseExtended_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBranch",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBranch", x => new { x.ProductId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_ProductBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBranch_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriod",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriod", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductPeriod_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTime",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Minutes = table.Column<int>(type: "integer", nullable: false),
                    WeekDayMaxMinutes = table.Column<int>(type: "integer", nullable: true),
                    WeekEndMaxMinutes = table.Column<int>(type: "integer", nullable: true),
                    AppGroupId = table.Column<int>(type: "integer", nullable: true),
                    ExpiresAfter = table.Column<int>(type: "integer", nullable: false),
                    ExpirationOptions = table.Column<int>(type: "integer", nullable: false),
                    ExpireFromOptions = table.Column<int>(type: "integer", nullable: false),
                    UsageOptions = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    ExpireAfterType = table.Column<int>(type: "integer", nullable: false),
                    ExpireAtDayTimeMinute = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTime", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductTime_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                    table.ForeignKey(
                        name: "FK_ProductTime_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundle",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    BundleStockOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundle", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBundle_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriodDay",
                columns: table => new
                {
                    ProductPeriodDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductPeriodId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriodDay", x => x.ProductPeriodDayId);
                    table.ForeignKey(
                        name: "FK_ProductPeriodDay_ProductPeriod_ProductPeriodId",
                        column: x => x.ProductPeriodId,
                        principalTable: "ProductPeriod",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriod",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriod", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductTimePeriod_ProductTime_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false),
                    PeriodDayId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_ProductPeriodDayTime_ProductPeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "ProductPeriodDay",
                        principalColumn: "ProductPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriodDay",
                columns: table => new
                {
                    ProductTimePeriodDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductTimePeriodId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriodDay", x => x.ProductTimePeriodDayId);
                    table.ForeignKey(
                        name: "FK_ProductTimePeriodDay_ProductTimePeriod_ProductTimePeriodId",
                        column: x => x.ProductTimePeriodId,
                        principalTable: "ProductTimePeriod",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimePeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false),
                    PeriodDayId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimePeriodDayTime", x => new { x.PeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_ProductTimePeriodDayTime_ProductTimePeriodDay_PeriodDayId",
                        column: x => x.PeriodDayId,
                        principalTable: "ProductTimePeriodDay",
                        principalColumn: "ProductTimePeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundleUserPrice",
                columns: table => new
                {
                    ProductBundleUserPriceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductBundleId = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    PointsPrice = table.Column<int>(type: "integer", nullable: true),
                    PurchaseOptions = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundleUserPrice", x => x.ProductBundleUserPriceId);
                    table.ForeignKey(
                        name: "FK_ProductBundleUserPrice_ProductBundle_ProductBundleId",
                        column: x => x.ProductBundleId,
                        principalTable: "ProductBundle",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    SortOption = table.Column<int>(type: "integer", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.ProductGroupId);
                    table.ForeignKey(
                        name: "FK_ProductGroup_ProductGroup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductGroup",
                        principalColumn: "ProductGroupId");
                });

            migrationBuilder.CreateTable(
                name: "ProductHostHidden",
                columns: table => new
                {
                    ProductHostHiddenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHostHidden", x => x.ProductHostHiddenId);
                    table.ForeignKey(
                        name: "FK_ProductHostHidden_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductHostHidden_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOL",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitListPrice = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UnitPointsPrice = table.Column<int>(type: "integer", nullable: false),
                    UnitPointsListPrice = table.Column<int>(type: "integer", nullable: true),
                    UnitCost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    TaxRate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PreTaxTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    PointsAward = table.Column<int>(type: "integer", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PayType = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ReservationId = table.Column<int>(type: "integer", nullable: true),
                    ReservationHostId = table.Column<int>(type: "integer", nullable: true),
                    ReservationSlot = table.Column<int>(type: "integer", nullable: true),
                    PrepareStatus = table.Column<int>(type: "integer", nullable: false),
                    PreparedQuantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PrepareTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    DeliveredQuantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOL", x => x.ProductOLId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLReservationFee",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Fee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLReservationFee", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLReservationFee_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLTimeFixed",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLTimeFixed", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLTimeFixed_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLExtended",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false),
                    BundleLineId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLExtended", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLExtended_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLProduct",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLProduct", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLProduct_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ProductOLProduct_ProductOLExtended_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOLExtended",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOLTime",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false),
                    ProductTimeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLTime", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLTime_ProductOLExtended_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOLExtended",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOLTime_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOLSession",
                columns: table => new
                {
                    ProductOLId = table.Column<int>(type: "integer", nullable: false),
                    UsageSessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOLSession", x => x.ProductOLId);
                    table.ForeignKey(
                        name: "FK_ProductOLSession_ProductOL_ProductOLId",
                        column: x => x.ProductOLId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTotal = table.Column<int>(type: "integer", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    PreferredPaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    UserNote = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PrepareStatus = table.Column<int>(type: "integer", nullable: false),
                    PreparedQuantity = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PrepareTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.ProductOrderId);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_ProductOrder_PaymentMethod_PreferredPaymentMethodId",
                        column: x => x.PreferredPaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDiscount",
                columns: table => new
                {
                    ProductOrderDiscountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductOrderLineId = table.Column<int>(type: "integer", nullable: true),
                    PromotionId = table.Column<int>(type: "integer", nullable: true),
                    PromotionCodeId = table.Column<int>(type: "integer", nullable: true),
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    DiscountName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    CalculationType = table.Column<int>(type: "integer", nullable: false),
                    ApplyType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDiscount", x => x.ProductOrderDiscountId);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOL_ProductOrderLineId",
                        column: x => x.ProductOrderLineId,
                        principalTable: "ProductOL",
                        principalColumn: "ProductOLId");
                    table.ForeignKey(
                        name: "FK_ProductOrderDiscount_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTax",
                columns: table => new
                {
                    ProductTaxId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    TaxId = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTax", x => x.ProductTaxId);
                    table.ForeignKey(
                        name: "FK_ProductTax_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTimeHostDisallowed",
                columns: table => new
                {
                    ProductTimeHostDisallowedId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductTimeId = table.Column<int>(type: "integer", nullable: false),
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTimeHostDisallowed", x => x.ProductTimeHostDisallowedId);
                    table.ForeignKey(
                        name: "FK_ProductTimeHostDisallowed_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTimeHostDisallowed_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductUserDisallowed",
                columns: table => new
                {
                    ProductUserDisallowedId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserDisallowed", x => x.ProductUserDisallowedId);
                    table.ForeignKey(
                        name: "FK_ProductUserDisallowed_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductUserPrice",
                columns: table => new
                {
                    ProductUserPriceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    PointsPrice = table.Column<int>(type: "integer", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PurchaseOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserPrice", x => x.ProductUserPriceId);
                    table.ForeignKey(
                        name: "FK_ProductUserPrice_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CodeType = table.Column<int>(type: "integer", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "PromotionBranch",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionBranch", x => new { x.PromotionId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_PromotionBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionBranch_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscount",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscount", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscount_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDiscountGroup",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    DiscountGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDiscountGroup", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountGroup_DiscountGroup_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalTable: "DiscountGroup",
                        principalColumn: "DiscountGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDiscountGroup_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionLimit",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionLimit", x => new { x.PromotionId, x.Type });
                    table.ForeignKey(
                        name: "FK_PromotionLimit_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriod",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriod", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionPeriod_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriodDay",
                columns: table => new
                {
                    PromotionPeriodDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PromotionPeriodId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriodDay", x => x.PromotionPeriodDayId);
                    table.ForeignKey(
                        name: "FK_PromotionPeriodDay_PromotionPeriod_PromotionPeriodId",
                        column: x => x.PromotionPeriodId,
                        principalTable: "PromotionPeriod",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPeriodDayTime",
                columns: table => new
                {
                    StartSecond = table.Column<int>(type: "integer", nullable: false),
                    EndSecond = table.Column<int>(type: "integer", nullable: false),
                    PromotionPeriodDayId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPeriodDayTime", x => new { x.PromotionPeriodDayId, x.StartSecond, x.EndSecond });
                    table.ForeignKey(
                        name: "FK_PromotionPeriodDayTime_PromotionPeriodDay_PromotionPeriodDa~",
                        column: x => x.PromotionPeriodDayId,
                        principalTable: "PromotionPeriodDay",
                        principalColumn: "PromotionPeriodDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCode",
                columns: table => new
                {
                    PromotionCodeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCode", x => x.PromotionCodeId);
                    table.ForeignKey(
                        name: "FK_PromotionCode_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.RecipientId);
                });

            migrationBuilder.CreateTable(
                name: "RecipientChannel",
                columns: table => new
                {
                    RecipientChannelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    ChannelType = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientChannel", x => x.RecipientChannelId);
                    table.ForeignKey(
                        name: "FK_RecipientChannel_Recipient_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Recipient",
                        principalColumn: "RecipientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Refund",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DepositTransactionId = table.Column<int>(type: "integer", nullable: true),
                    PointTransactionId = table.Column<int>(type: "integer", nullable: true),
                    RefundMethodId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_Refund_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_Refund_DepositTransaction_DepositTransactionId",
                        column: x => x.DepositTransactionId,
                        principalTable: "DepositTransaction",
                        principalColumn: "DepositTransactionId");
                    table.ForeignKey(
                        name: "FK_Refund_PaymentMethod_RefundMethodId",
                        column: x => x.RefundMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refund_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Refund_PointTransaction_PointTransactionId",
                        column: x => x.PointTransactionId,
                        principalTable: "PointTransaction",
                        principalColumn: "PointTransactionId");
                });

            migrationBuilder.CreateTable(
                name: "RefundDepositPayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "integer", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "integer", nullable: true),
                    FiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundDepositPayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundDepositPayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundInvoicePayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "integer", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundInvoicePayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundPayment",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundPayment", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundPayment_FiscalReceipt_FiscalReceiptId",
                        column: x => x.FiscalReceiptId,
                        principalTable: "FiscalReceipt",
                        principalColumn: "FiscalReceiptId");
                    table.ForeignKey(
                        name: "FK_RefundPayment_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefundReceipt",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "integer", nullable: false),
                    RRN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundReceipt", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_RefundReceipt_Refund_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Refund",
                        principalColumn: "RefundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    RegisterId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    MacAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartCash = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IdleTimeout = table.Column<int>(type: "integer", nullable: true),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentTerminalNumber = table.Column<int>(type: "integer", nullable: true),
                    FiscalReceiptPrinterNumber = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CompanionId = table.Column<int>(type: "integer", nullable: true),
                    StockId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.RegisterId);
                    table.ForeignKey(
                        name: "FK_Register_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Register_Companion_CompanionId",
                        column: x => x.CompanionId,
                        principalTable: "Companion",
                        principalColumn: "CompanionId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RegisterTransaction",
                columns: table => new
                {
                    RegisterTransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisterId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterTransaction", x => x.RegisterTransactionId);
                    table.ForeignKey(
                        name: "FK_RegisterTransaction_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Pin = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ContactPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ContactEmail = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    ActivationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpireAfter = table.Column<int>(type: "integer", nullable: true),
                    CancellationGracePeriod = table.Column<int>(type: "integer", nullable: true),
                    CancellationRefundPercentage = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumPaymentPercentage = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    LoginBlockBeforeTime = table.Column<int>(type: "integer", nullable: true),
                    LoginBlockAfterTime = table.Column<int>(type: "integer", nullable: true),
                    FinalizedById = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHost",
                columns: table => new
                {
                    ReservationHostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    PreferredUserId = table.Column<int>(type: "integer", nullable: true),
                    MovedToReservationHostId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ActivationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FinalizedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationHost", x => x.ReservationHostId);
                    table.ForeignKey(
                        name: "FK_ReservationHost_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationHost_ReservationHost_MovedToReservationHostId",
                        column: x => x.MovedToReservationHostId,
                        principalTable: "ReservationHost",
                        principalColumn: "ReservationHostId");
                    table.ForeignKey(
                        name: "FK_ReservationHost_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationProductOrder",
                columns: table => new
                {
                    ReservationProductOrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationProductOrder", x => x.ReservationProductOrderId);
                    table.ForeignKey(
                        name: "FK_ReservationProductOrder_ProductOrder_ProductOrderId",
                        column: x => x.ProductOrderId,
                        principalTable: "ProductOrder",
                        principalColumn: "ProductOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationProductOrder_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationUser",
                columns: table => new
                {
                    ReservationUserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationUser", x => x.ReservationUserId);
                    table.ForeignKey(
                        name: "FK_ReservationUser_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReport",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReport", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleReport_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReportEntry",
                columns: table => new
                {
                    ScheduleReportEntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduleReportId = table.Column<int>(type: "integer", nullable: false),
                    ReportType = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportRange = table.Column<int>(type: "integer", nullable: false),
                    Filters = table.Column<string>(type: "text", nullable: true),
                    ReportPresetId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleReportEntry", x => x.ScheduleReportEntryId);
                    table.ForeignKey(
                        name: "FK_ScheduleReportEntry_ReportPreset_ReportPresetId",
                        column: x => x.ReportPresetId,
                        principalTable: "ReportPreset",
                        principalColumn: "ReportPresetId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ScheduleReportEntry_ScheduleReport_ScheduleReportId",
                        column: x => x.ScheduleReportId,
                        principalTable: "ScheduleReport",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleReportRecipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "integer", nullable: false),
                    ScheduleReportId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfile",
                columns: table => new
                {
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DisabledDrives = table.Column<int>(type: "integer", nullable: false),
                    DisableStartMenu = table.Column<bool>(type: "boolean", nullable: false),
                    DisableDesktopSwitching = table.Column<bool>(type: "boolean", nullable: false),
                    StickyShell = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfile", x => x.SecurityProfileId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfilePolicy",
                columns: table => new
                {
                    SecurityProfilePolicyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfilePolicy", x => x.SecurityProfilePolicyId);
                    table.ForeignKey(
                        name: "FK_SecurityProfilePolicy_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProfileRestriction",
                columns: table => new
                {
                    SecurityProfileRestrictionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: false),
                    Parameter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProfileRestriction", x => x.SecurityProfileRestrictionId);
                    table.ForeignKey(
                        name: "FK_SecurityProfileRestriction_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    GroupName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OperatorId = table.Column<int>(type: "integer", nullable: false),
                    RegisterId = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StartCash = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsEnding = table.Column<bool>(type: "boolean", nullable: false),
                    EndedById = table.Column<int>(type: "integer", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shift_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_Shift_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftCount",
                columns: table => new
                {
                    ShiftCountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    StartCash = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Sales = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Deposits = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PayIns = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Withdrawals = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PayOuts = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Refunds = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Voids = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Expected = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Actual = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Difference = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftCount", x => x.ShiftCountId);
                    table.ForeignKey(
                        name: "FK_ShiftCount_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftCount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stock_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCount",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UnexpectedEntries = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCount", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCount_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_StockCount_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_StockCount_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCountAdjustment",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "integer", nullable: false),
                    AdjustmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountAdjustment", x => x.StockCountId);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustment_InventoryAdjustment_AdjustmentId",
                        column: x => x.AdjustmentId,
                        principalTable: "InventoryAdjustment",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCountAdjustment_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCountInbound",
                columns: table => new
                {
                    StockCountId = table.Column<int>(type: "integer", nullable: false),
                    InboundId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "StockCountEntry",
                columns: table => new
                {
                    StockCountEntryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockCountId = table.Column<int>(type: "integer", nullable: false),
                    Expected = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Actual = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Difference = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCountEntry", x => x.StockCountEntryId);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockCountEntry_StockCount_StockCountId",
                        column: x => x.StockCountId,
                        principalTable: "StockCount",
                        principalColumn: "StockCountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransaction",
                columns: table => new
                {
                    StockTransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    SourceProductId = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    OnHand = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    SourceProductAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    SourceProductOnHand = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    StockId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.StockTransactionId);
                    table.ForeignKey(
                        name: "FK_StockTransaction_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransaction_ProductBase_SourceProductId",
                        column: x => x.SourceProductId,
                        principalTable: "ProductBase",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_StockTransaction_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.TargetId);
                });

            migrationBuilder.CreateTable(
                name: "TargetBillProfile",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    TargetGroupBillProfileId = table.Column<int>(type: "integer", nullable: false),
                    BillProfileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetBillProfile", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetBillProfile_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetBillProfile_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroup",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountId = table.Column<int>(type: "integer", nullable: false),
                    Requirement = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    IncludeAll = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroup", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroup_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupBillProfile",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupBillProfile", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupBillProfile_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupPaymentMethod",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupPaymentMethod", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupPaymentMethod_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProduct",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProduct", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProduct_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProductGroup",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProductGroup", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProductGroup_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupProductTime",
                columns: table => new
                {
                    TargetGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupProductTime", x => x.TargetGroupId);
                    table.ForeignKey(
                        name: "FK_TargetGroupProductTime_TargetGroup_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetPaymentMethod",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    TargetGroupPaymentMethodId = table.Column<int>(type: "integer", nullable: false),
                    MethodId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetPaymentMethod", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_PaymentMethod_MethodId",
                        column: x => x.MethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_TargetGroupPaymentMethod_TargetGroupPay~",
                        column: x => x.TargetGroupPaymentMethodId,
                        principalTable: "TargetGroupPaymentMethod",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetPaymentMethod_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProduct",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    TargetGroupProductId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProduct", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProduct_ProductBaseExtended_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBaseExtended",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProduct_TargetGroupProduct_TargetGroupProductId",
                        column: x => x.TargetGroupProductId,
                        principalTable: "TargetGroupProduct",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProduct_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProductGroup",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    TargetGroupProductGroupId = table.Column<int>(type: "integer", nullable: false),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProductGroup", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "ProductGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_TargetGroupProductGroup_TargetGroupProdu~",
                        column: x => x.TargetGroupProductGroupId,
                        principalTable: "TargetGroupProductGroup",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProductGroup_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetProductTime",
                columns: table => new
                {
                    TargetId = table.Column<int>(type: "integer", nullable: false),
                    TargetGroupProductTimeId = table.Column<int>(type: "integer", nullable: false),
                    ProductTimeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetProductTime", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_ProductTime_ProductTimeId",
                        column: x => x.ProductTimeId,
                        principalTable: "ProductTime",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_TargetGroupProductTime_TargetGroupProduct~",
                        column: x => x.TargetGroupProductTimeId,
                        principalTable: "TargetGroupProductTime",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetProductTime_Target_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Target",
                        principalColumn: "TargetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskBase",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBase", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "TaskJunction",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    SourceDirectory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DestinationDirectory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskJunction", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskJunction_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskNotification",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    NotificationOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNotification", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskNotification_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskProcess",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Arguments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WorkingDirectory = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    ProcessOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProcess", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskProcess_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskScript",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    ScriptType = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    ProcessOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskScript", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskScript_TaskBase_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskBase",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Value = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Value = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ConfirmationCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "Usage",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsageSessionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Seconds = table.Column<double>(type: "double precision", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usage", x => x.UsageId);
                });

            migrationBuilder.CreateTable(
                name: "UsageRate",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false),
                    BillRateId = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    BillProfileStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: true),
                    DiscountCalculationType = table.Column<int>(type: "integer", nullable: true),
                    DiscountValue = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: true),
                    Discount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRate", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageRate_BillRate_BillRateId",
                        column: x => x.BillRateId,
                        principalTable: "BillRate",
                        principalColumn: "BillRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsageRate_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsageSession",
                columns: table => new
                {
                    UsageSessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CurrentUsageId = table.Column<int>(type: "integer", nullable: true),
                    CurrentSecond = table.Column<double>(type: "double precision", nullable: false),
                    NegativeSeconds = table.Column<double>(type: "double precision", nullable: false),
                    StartFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RatesTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageSession", x => x.UsageSessionId);
                    table.ForeignKey(
                        name: "FK_UsageSession_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsageSession_Usage_CurrentUsageId",
                        column: x => x.CurrentUsageId,
                        principalTable: "Usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsageTime",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageTime", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageTime_InvoiceLineTime_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineTime",
                        principalColumn: "InvoiceLineId");
                });

            migrationBuilder.CreateTable(
                name: "UsageTimeFixed",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceLineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageTimeFixed", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageTimeFixed_InvoiceLineTimeFixed_InvoiceLineId",
                        column: x => x.InvoiceLineId,
                        principalTable: "InvoiceLineTimeFixed",
                        principalColumn: "InvoiceLineId");
                });

            migrationBuilder.CreateTable(
                name: "UsageUserSession",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "integer", nullable: false),
                    UserSessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageUserSession", x => x.UsageId);
                    table.ForeignKey(
                        name: "FK_UsageUserSession_Usage_UsageId",
                        column: x => x.UsageId,
                        principalTable: "Usage",
                        principalColumn: "UsageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    LastName = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    Country = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    PostCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MobilePhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    SmartCardUID = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Identification = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PreferredChannel = table.Column<Guid>(type: "uuid", nullable: true),
                    PermissionSetId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_User_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserOperator",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    ShiftOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperator", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserOperator_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPicture",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Picture = table.Column<byte[]>(type: "bytea", maxLength: 16777215, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPicture", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserPicture_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPicture_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPicture_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TokenId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_Verification_Token_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Token",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Verification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserAgreement",
                columns: table => new
                {
                    UserAgreementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Agreement = table.Column<string>(type: "text", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    DisplayOptions = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreement", x => x.UserAgreementId);
                    table.ForeignKey(
                        name: "FK_UserAgreement_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreement_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserApiKey",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ApiKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApiKey", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserApiKey_UserOperator_UserId",
                        column: x => x.UserId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAttribute",
                columns: table => new
                {
                    UserAttributeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AttributeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAttribute", x => x.UserAttributeId);
                    table.ForeignKey(
                        name: "FK_UserAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAttribute_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAttribute_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAttribute_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChannel",
                columns: table => new
                {
                    UserChannelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Channel = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannel", x => x.UserChannelId);
                    table.ForeignKey(
                        name: "FK_UserChannel_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserChannel_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserChannel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredential",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", fixedLength: true, maxLength: 64, nullable: true),
                    Salt = table.Column<byte[]>(type: "bytea", fixedLength: true, maxLength: 100, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredential", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredential_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCredential_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCredential_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AppGroupId = table.Column<int>(type: "integer", nullable: true),
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: true),
                    BillProfileId = table.Column<int>(type: "integer", nullable: true),
                    RequiredUserInfo = table.Column<int>(type: "integer", nullable: false),
                    Overrides = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<int>(type: "integer", nullable: false),
                    CreditLimitOptions = table.Column<int>(type: "integer", nullable: false),
                    IsNegativeBalanceAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsAwardOptions = table.Column<int>(type: "integer", nullable: false),
                    PointsMoneyRatio = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    PointsTimeRatio = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsAgeRatingEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    BillingOptions = table.Column<int>(type: "integer", nullable: false),
                    WaitingLinePriority = table.Column<int>(type: "integer", nullable: false),
                    IsWaitingLinePriorityEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    DiscountGroupId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.UserGroupId);
                    table.ForeignKey(
                        name: "FK_UserGroup_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroup_BillProfile_BillProfileId",
                        column: x => x.BillProfileId,
                        principalTable: "BillProfile",
                        principalColumn: "BillProfileId");
                    table.ForeignKey(
                        name: "FK_UserGroup_DiscountGroup_DiscountGroupId",
                        column: x => x.DiscountGroupId,
                        principalTable: "DiscountGroup",
                        principalColumn: "DiscountGroupId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserGroup_SecurityProfile_SecurityProfileId",
                        column: x => x.SecurityProfileId,
                        principalTable: "SecurityProfile",
                        principalColumn: "SecurityProfileId");
                    table.ForeignKey(
                        name: "FK_UserGroup_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserGroup_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserOperatorBranch",
                columns: table => new
                {
                    OperatorBranchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperatorId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperatorBranch", x => x.OperatorBranchId);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserOperatorBranch_UserOperator_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "UserOperator",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    UserPermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.UserPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermission_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermission_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSet",
                columns: table => new
                {
                    UserPermissionSetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSet", x => x.UserPermissionSetId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserPermissionSet_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Variable",
                columns: table => new
                {
                    VariableId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Value = table.Column<string>(type: "character varying(65535)", maxLength: 65535, nullable: false),
                    Scope = table.Column<int>(type: "integer", nullable: false),
                    UseOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variable", x => x.VariableId);
                    table.ForeignKey(
                        name: "FK_Variable_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Variable_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Void",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Void", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_Void_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_Void_Register_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Register",
                        principalColumn: "RegisterId");
                    table.ForeignKey(
                        name: "FK_Void_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "ShiftId");
                    table.ForeignKey(
                        name: "FK_Void_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VerificationEmail",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationEmail", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_VerificationEmail_Verification_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verification",
                        principalColumn: "VerificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificationMobilePhone",
                columns: table => new
                {
                    VerificationId = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationMobilePhone", x => x.VerificationId);
                    table.ForeignKey(
                        name: "FK_VerificationMobilePhone_Verification_VerificationId",
                        column: x => x.VerificationId,
                        principalTable: "Verification",
                        principalColumn: "VerificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAgreementState",
                columns: table => new
                {
                    UserAgreementStateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserAgreementId = table.Column<int>(type: "integer", nullable: false),
                    AcceptState = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreementState", x => x.UserAgreementStateId);
                    table.ForeignKey(
                        name: "FK_UserAgreementState_UserAgreement_UserAgreementId",
                        column: x => x.UserAgreementId,
                        principalTable: "UserAgreement",
                        principalColumn: "UserAgreementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserAgreementState_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupHostDisallowed",
                columns: table => new
                {
                    UserGroupHostDisallowedId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    HostGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsDisallowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupHostDisallowed", x => x.UserGroupHostDisallowedId);
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserGroupHostDisallowed_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserMember",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    UserGroupId = table.Column<int>(type: "integer", nullable: false),
                    IsNegativeBalanceAllowed = table.Column<bool>(type: "boolean", nullable: true),
                    IsPersonalInfoRequested = table.Column<bool>(type: "boolean", nullable: false),
                    BillingOptions = table.Column<int>(type: "integer", nullable: true),
                    EnableDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DisabledDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMember", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserMember_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionSetPermission",
                columns: table => new
                {
                    UserPermissionSetPermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionSetId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    Value = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionSetPermission", x => x.UserPermissionSetPermissionId);
                    table.ForeignKey(
                        name: "FK_UserPermissionSetPermission_UserPermissionSet_PermissionSet~",
                        column: x => x.PermissionSetId,
                        principalTable: "UserPermissionSet",
                        principalColumn: "UserPermissionSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoidDepositPayment",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "integer", nullable: false),
                    DepositPaymentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoidDepositPayment", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_VoidDepositPayment_DepositPayment_DepositPaymentId",
                        column: x => x.DepositPaymentId,
                        principalTable: "DepositPayment",
                        principalColumn: "DepositPaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoidDepositPayment_Void_VoidId",
                        column: x => x.VoidId,
                        principalTable: "Void",
                        principalColumn: "VoidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoidInvoice",
                columns: table => new
                {
                    VoidId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoidInvoice", x => x.VoidId);
                    table.ForeignKey(
                        name: "FK_VoidInvoice_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoidInvoice_Void_VoidId",
                        column: x => x.VoidId,
                        principalTable: "Void",
                        principalColumn: "VoidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCreditLimit",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCreditLimit", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserCreditLimit_UserOperator_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserGuest",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsJoined = table.Column<bool>(type: "boolean", nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    ReservedHostId = table.Column<int>(type: "integer", nullable: true),
                    ReservedSlot = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGuest", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserGuest_Host_ReservedHostId",
                        column: x => x.ReservedHostId,
                        principalTable: "Host",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_UserGuest_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserNoteOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNote", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_UserNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNote_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserSession",
                columns: table => new
                {
                    UserSessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Slot = table.Column<int>(type: "integer", nullable: false),
                    Span = table.Column<double>(type: "double precision", nullable: false),
                    BilledSpan = table.Column<double>(type: "double precision", nullable: false),
                    PendTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PendSpan = table.Column<double>(type: "double precision", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PendSpanTotal = table.Column<double>(type: "double precision", nullable: false),
                    PauseSpan = table.Column<double>(type: "double precision", nullable: false),
                    PauseSpanTotal = table.Column<double>(type: "double precision", nullable: false),
                    GraceTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    GraceSpan = table.Column<double>(type: "double precision", nullable: false),
                    GraceSpanTotal = table.Column<double>(type: "double precision", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.UserSessionId);
                    table.ForeignKey(
                        name: "FK_UserSession_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserSession_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserSessionChange",
                columns: table => new
                {
                    UserSessionChangeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSessionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Slot = table.Column<int>(type: "integer", nullable: false),
                    Span = table.Column<double>(type: "double precision", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionChange", x => x.UserSessionChangeId);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserSessionChange_UserSession_UserSessionId",
                        column: x => x.UserSessionId,
                        principalTable: "UserSession",
                        principalColumn: "UserSessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSessionChange_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgeRestriction_CreatedById",
                table: "AgeRestriction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgeRestrictionProduct_ProductId",
                table: "AgeRestrictionProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_App_AppCategoryId",
                table: "App",
                column: "AppCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_App_CreatedById",
                table: "App",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_App_DeveloperId",
                table: "App",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_App_Guid",
                table: "App",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_App_ModifiedById",
                table: "App",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_App_PublisherId",
                table: "App",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_CreatedById",
                table: "AppCategory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_Guid",
                table: "AppCategory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_ModifiedById",
                table: "AppCategory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_ParentId",
                table: "AppCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_CreatedById",
                table: "AppEnterprise",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_Guid",
                table: "AppEnterprise",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_ModifiedById",
                table: "AppEnterprise",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_Name",
                table: "AppEnterprise",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExe_AppId",
                table: "AppExe",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExe_CreatedById",
                table: "AppExe",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExe_DefaultDeploymentId",
                table: "AppExe",
                column: "DefaultDeploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExe_ModifiedById",
                table: "AppExe",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeBranch_AppExeId_BranchId",
                table: "AppExeBranch",
                columns: new[] { "AppExeId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeBranch_BranchId",
                table: "AppExeBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_AppExeId",
                table: "AppExeCdImage",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_CreatedById",
                table: "AppExeCdImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_Guid",
                table: "AppExeCdImage",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_ModifiedById",
                table: "AppExeCdImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeDeployment_AppExeId",
                table: "AppExeDeployment",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeDeployment_CreatedById",
                table: "AppExeDeployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeDeployment_DeploymentId",
                table: "AppExeDeployment",
                column: "DeploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeDeployment_ModifiedById",
                table: "AppExeDeployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeImage_AppExeId",
                table: "AppExeImage",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeImage_CreatedById",
                table: "AppExeImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeImage_ModifiedById",
                table: "AppExeImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeLicense_AppExeId",
                table: "AppExeLicense",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeLicense_CreatedById",
                table: "AppExeLicense",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeLicense_LicenseId",
                table: "AppExeLicense",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeLicense_ModifiedById",
                table: "AppExeLicense",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeMaxUser_AppExeId_Mode",
                table: "AppExeMaxUser",
                columns: new[] { "AppExeId", "Mode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExeMaxUser_CreatedById",
                table: "AppExeMaxUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeMaxUser_ModifiedById",
                table: "AppExeMaxUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExePersonalFile_AppExeId",
                table: "AppExePersonalFile",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExePersonalFile_CreatedById",
                table: "AppExePersonalFile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExePersonalFile_ModifiedById",
                table: "AppExePersonalFile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExePersonalFile_PersonalFileId",
                table: "AppExePersonalFile",
                column: "PersonalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeTask_AppExeId",
                table: "AppExeTask",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeTask_CreatedById",
                table: "AppExeTask",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeTask_ModifiedById",
                table: "AppExeTask",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeTask_TaskBaseId",
                table: "AppExeTask",
                column: "TaskBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGroup_CreatedById",
                table: "AppGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppGroup_Guid",
                table: "AppGroup",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroup_ModifiedById",
                table: "AppGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppGroup_Name",
                table: "AppGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupApp_AppGroupId",
                table: "AppGroupApp",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupApp_AppId",
                table: "AppGroupApp",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImage_AppId",
                table: "AppImage",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppImage_CreatedById",
                table: "AppImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppImage_ModifiedById",
                table: "AppImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppLink_AppId",
                table: "AppLink",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLink_CreatedById",
                table: "AppLink",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppLink_Guid",
                table: "AppLink",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppLink_ModifiedById",
                table: "AppLink",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppRating_AppId",
                table: "AppRating",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRating_UserId",
                table: "AppRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_AppExeId",
                table: "AppStat",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_AppId",
                table: "AppStat",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_BranchId",
                table: "AppStat",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_HostId",
                table: "AppStat",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStat_UserId",
                table: "AppStat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Barcode",
                table: "Asset",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_BranchId",
                table: "Asset",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_CreatedById",
                table: "Asset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ModifiedById",
                table: "Asset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SmartCardUID",
                table: "Asset",
                column: "SmartCardUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_AssetId",
                table: "AssetTransaction",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_AssetTypeId",
                table: "AssetTransaction",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_BranchId",
                table: "AssetTransaction",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_CheckedInById",
                table: "AssetTransaction",
                column: "CheckedInById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_CreatedById",
                table: "AssetTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_ModifiedById",
                table: "AssetTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTransaction_UserId",
                table: "AssetTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_CreatedById",
                table: "AssetType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_ModifiedById",
                table: "AssetType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssetType_Name",
                table: "AssetType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_AssistanceRequestTypeId",
                table: "AssistanceRequest",
                column: "AssistanceRequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_BranchId",
                table: "AssistanceRequest",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_CreatedById",
                table: "AssistanceRequest",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_HostId",
                table: "AssistanceRequest",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_ModifiedById",
                table: "AssistanceRequest",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_UserId",
                table: "AssistanceRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequestType_CreatedById",
                table: "AssistanceRequestType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequestType_ModifiedById",
                table: "AssistanceRequestType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CreatedById",
                table: "Attribute",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ModifiedById",
                table: "Attribute",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Name",
                table: "Attribute",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillProfile_CreatedById",
                table: "BillProfile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BillProfile_ModifiedById",
                table: "BillProfile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BillProfile_Name",
                table: "BillProfile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillRate_BillProfileId",
                table: "BillRate",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BillRatePeriodDay_BillRateId_Day",
                table: "BillRatePeriodDay",
                columns: new[] { "BillRateId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillRatePeriodDayTime_PeriodDayId",
                table: "BillRatePeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_BillRateStep_BillRateId_Minute",
                table: "BillRateStep",
                columns: new[] { "BillRateId", "Minute" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchId",
                table: "Branch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CompanionId",
                table: "Branch",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CreatedById",
                table: "Branch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Guid",
                table: "Branch",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_ModifiedById",
                table: "Branch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Name",
                table: "Branch",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BundleProduct_CreatedById",
                table: "BundleProduct",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProduct_ModifiedById",
                table: "BundleProduct",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProduct_ProductBundleId",
                table: "BundleProduct",
                column: "ProductBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProduct_ProductId",
                table: "BundleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProductUserPrice_BundleProductId_UserGroupId",
                table: "BundleProductUserPrice",
                columns: new[] { "BundleProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BundleProductUserPrice_CreatedById",
                table: "BundleProductUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProductUserPrice_ModifiedById",
                table: "BundleProductUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BundleProductUserPrice_UserGroupId",
                table: "BundleProductUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOptions_CreatedById",
                table: "ClientOptions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientOptions_ModifiedById",
                table: "ClientOptions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTask_CreatedById",
                table: "ClientTask",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTask_ModifiedById",
                table: "ClientTask",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTask_TaskBaseId",
                table: "ClientTask",
                column: "TaskBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Companion_CreatedById",
                table: "Companion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companion_Guid",
                table: "Companion",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companion_ModifiedById",
                table: "Companion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Companion_Name",
                table: "Companion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_CreatedById",
                table: "Deployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_Guid",
                table: "Deployment",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_ModifiedById",
                table: "Deployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_Name",
                table: "Deployment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentDeployment_ChildId",
                table: "DeploymentDeployment",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentDeployment_CreatedById",
                table: "DeploymentDeployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentDeployment_ModifiedById",
                table: "DeploymentDeployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeploymentDeployment_ParentId",
                table: "DeploymentDeployment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_BranchId",
                table: "DepositPayment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_CreatedById",
                table: "DepositPayment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_DepositTransactionId",
                table: "DepositPayment",
                column: "DepositTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_FiscalReceiptId",
                table: "DepositPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_ModifiedById",
                table: "DepositPayment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_PaymentId",
                table: "DepositPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_RegisterId",
                table: "DepositPayment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_ShiftId",
                table: "DepositPayment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPayment_UserId",
                table: "DepositPayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_CreatedById",
                table: "DepositTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_ModifiedById",
                table: "DepositTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_RegisterId",
                table: "DepositTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_ShiftId",
                table: "DepositTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositTransaction_UserId",
                table: "DepositTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_BranchId",
                table: "Device",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_CreatedById",
                table: "Device",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceId",
                table: "Device",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_ModifiedById",
                table: "Device",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name_BranchId",
                table: "Device",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHdmi_DeviceId",
                table: "DeviceHdmi",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHdmi_UniqueId",
                table: "DeviceHdmi",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_CreatedById",
                table: "DeviceHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_DeviceId_HostId",
                table: "DeviceHost",
                columns: new[] { "DeviceId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_HostId",
                table: "DeviceHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_ModifiedById",
                table: "DeviceHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CreatedById",
                table: "Discount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ModifiedById",
                table: "Discount",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_Name",
                table: "Discount",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountBranch_BranchId",
                table: "DiscountBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountBranch_DiscountId_BranchId",
                table: "DiscountBranch",
                columns: new[] { "DiscountId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_CreatedById",
                table: "DiscountGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_ModifiedById",
                table: "DiscountGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroup_Name",
                table: "DiscountGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountGroupDiscount_DiscountId",
                table: "DiscountGroupDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriod_DiscountId",
                table: "DiscountPeriod",
                column: "DiscountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriodDay_DiscountPeriodId_Day",
                table: "DiscountPeriodDay",
                columns: new[] { "DiscountPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountPeriodDayTime_DiscountPeriodDayId",
                table: "DiscountPeriodDayTime",
                column: "DiscountPeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_CreatedById",
                table: "DocumentType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_ModifiedById",
                table: "DocumentType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_Name",
                table: "DocumentType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feed_CreatedById",
                table: "Feed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_ModifiedById",
                table: "Feed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBranch_BranchId",
                table: "FeedBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBranch_FeedId_BranchId",
                table: "FeedBranch",
                columns: new[] { "FeedId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_CreatedById",
                table: "File",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_File_Guid",
                table: "File",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_ModifiedById",
                table: "File",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FileDocument_DocumentTypeId",
                table: "FileDocument",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceipt_CreatedById",
                table: "FiscalReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceipt_FiscalReceiptId",
                table: "FiscalReceipt",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceipt_RegisterId",
                table: "FiscalReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalReceipt_ShiftId",
                table: "FiscalReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_CreatedById",
                table: "Host",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Host_Guid",
                table: "Host",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Host_HostGroupId",
                table: "Host",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_HostId",
                table: "Host",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_IconId",
                table: "Host",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_ModifiedById",
                table: "Host",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostComputer_HostId",
                table: "HostComputer",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_HostComputer_MACAddress",
                table: "HostComputer",
                column: "MACAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostEndpoint_HostId",
                table: "HostEndpoint",
                column: "HostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_AppGroupId",
                table: "HostGroup",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BillProfileId",
                table: "HostGroup",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_BranchId",
                table: "HostGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_ClientOptionsId",
                table: "HostGroup",
                column: "ClientOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_CreatedById",
                table: "HostGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_DefaultGuestGroupId",
                table: "HostGroup",
                column: "DefaultGuestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_ModifiedById",
                table: "HostGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_Name_BranchId",
                table: "HostGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroup_SecurityProfileId",
                table: "HostGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupUserBillProfile_BillProfileId",
                table: "HostGroupUserBillProfile",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupUserBillProfile_HostGroupId_UserGroupId",
                table: "HostGroupUserBillProfile",
                columns: new[] { "HostGroupId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupUserBillProfile_UserGroupId",
                table: "HostGroupUserBillProfile",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLine_CreatedById",
                table: "HostGroupWaitingLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLine_HostGroupId",
                table: "HostGroupWaitingLine",
                column: "HostGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLine_ModifiedById",
                table: "HostGroupWaitingLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLineEntry_CreatedById",
                table: "HostGroupWaitingLineEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLineEntry_HostGroupId",
                table: "HostGroupWaitingLineEntry",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLineEntry_ModifiedById",
                table: "HostGroupWaitingLineEntry",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLineEntry_UserId",
                table: "HostGroupWaitingLineEntry",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_BranchId",
                table: "HostLayoutGroup",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_CreatedById",
                table: "HostLayoutGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_ModifiedById",
                table: "HostLayoutGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_Name_BranchId",
                table: "HostLayoutGroup",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupImage_CreatedById",
                table: "HostLayoutGroupImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupImage_HostLayoutGroupId",
                table: "HostLayoutGroupImage",
                column: "HostLayoutGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupImage_ModifiedById",
                table: "HostLayoutGroupImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupLayout_CreatedById",
                table: "HostLayoutGroupLayout",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupLayout_HostId",
                table: "HostLayoutGroupLayout",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupLayout_HostLayoutGroupId_HostId",
                table: "HostLayoutGroupLayout",
                columns: new[] { "HostLayoutGroupId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroupLayout_ModifiedById",
                table: "HostLayoutGroupLayout",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Icon_CreatedById",
                table: "Icon",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Icon_ModifiedById",
                table: "Icon",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_CreatedById",
                table: "IntentOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_InvoicePaymentId",
                table: "IntentOrder",
                column: "InvoicePaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_ModifiedById",
                table: "IntentOrder",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_PaymentIntentOrderId_ProductOrderId",
                table: "IntentOrder",
                columns: new[] { "PaymentIntentOrderId", "ProductOrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrder_ProductOrderId",
                table: "IntentOrder",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_CreatedById",
                table: "IntentOrderDeposit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_ModifiedById",
                table: "IntentOrderDeposit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_PaymentIntentOrderId_UserId",
                table: "IntentOrderDeposit",
                columns: new[] { "PaymentIntentOrderId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntentOrderDeposit_UserId",
                table: "IntentOrderDeposit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CreatedById",
                table: "Inventory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ShiftId",
                table: "Inventory",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StockId",
                table: "Inventory",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentEntry_AdjustmentReasonId",
                table: "InventoryAdjustmentEntry",
                column: "AdjustmentReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_CreatedById",
                table: "InventoryAdjustmentReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_ModifiedById",
                table: "InventoryAdjustmentReason",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustmentReason_Name",
                table: "InventoryAdjustmentReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_CreatedById",
                table: "InventoryDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_FileDocumentId",
                table: "InventoryDocument",
                column: "FileDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryDocument_InventoryId_FileDocumentId",
                table: "InventoryDocument",
                columns: new[] { "InventoryId", "FileDocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_CreatedById",
                table: "InventoryEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_InventoryId",
                table: "InventoryEntry",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_ProductId",
                table: "InventoryEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_ShiftId",
                table: "InventoryEntry",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_StockId",
                table: "InventoryEntry",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryEntry_StockTransactionId",
                table: "InventoryEntry",
                column: "StockTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInbound_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInboundEntry_InventoryTransferEntryId",
                table: "InventoryInboundEntry",
                column: "InventoryTransferEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_InventoryInboundId",
                table: "InventoryTransfer",
                column: "InventoryInboundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TransferStockId",
                table: "InventoryTransfer",
                column: "TransferStockId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferEntry_TransferReasonId",
                table: "InventoryTransferEntry",
                column: "TransferReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_CreatedById",
                table: "InventoryTransferReason",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_ModifiedById",
                table: "InventoryTransferReason",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferReason_Name",
                table: "InventoryTransferReason",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CreatedById",
                table: "Invoice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ModifiedById",
                table: "Invoice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ProductOrderId",
                table: "Invoice",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_RegisterId",
                table: "Invoice",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ShiftId",
                table: "Invoice",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiscalReceipt_CreatedById",
                table: "InvoiceFiscalReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiscalReceipt_FiscalReceiptId",
                table: "InvoiceFiscalReceipt",
                column: "FiscalReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiscalReceipt_InvoiceId",
                table: "InvoiceFiscalReceipt",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiscalReceipt_RegisterId",
                table: "InvoiceFiscalReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFiscalReceipt_ShiftId",
                table: "InvoiceFiscalReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_CreatedById",
                table: "InvoiceLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceLineId",
                table: "InvoiceLine",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ModifiedById",
                table: "InvoiceLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_PointsTransactionId",
                table: "InvoiceLine",
                column: "PointsTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_RegisterId",
                table: "InvoiceLine",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ReservationHostId",
                table: "InvoiceLine",
                column: "ReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ReservationId",
                table: "InvoiceLine",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ShiftId",
                table: "InvoiceLine",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_UserId",
                table: "InvoiceLine",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineExtended_BundleLineId",
                table: "InvoiceLineExtended",
                column: "BundleLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineExtended_InvoiceLineId",
                table: "InvoiceLineExtended",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineExtended_StockReturnTransactionId",
                table: "InvoiceLineExtended",
                column: "StockReturnTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineExtended_StockTransactionId",
                table: "InvoiceLineExtended",
                column: "StockTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineProduct_InvoiceLineId",
                table: "InvoiceLineProduct",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineProduct_OrderLineId",
                table: "InvoiceLineProduct",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineProduct_ProductId",
                table: "InvoiceLineProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineReservationFee_OrderLineId",
                table: "InvoiceLineReservationFee",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineSession_InvoiceLineId",
                table: "InvoiceLineSession",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineSession_OrderLineId",
                table: "InvoiceLineSession",
                column: "OrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineSession_UsageSessionId",
                table: "InvoiceLineSession",
                column: "UsageSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTime_InvoiceLineId",
                table: "InvoiceLineTime",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTime_OrderLineId",
                table: "InvoiceLineTime",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTime_ProductTimeId",
                table: "InvoiceLineTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTimeFixed_InvoiceLineId",
                table: "InvoiceLineTimeFixed",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTimeFixed_OrderLineId",
                table: "InvoiceLineTimeFixed",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_BranchId",
                table: "InvoicePayment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_CreatedById",
                table: "InvoicePayment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_InvoiceId",
                table: "InvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_ModifiedById",
                table: "InvoicePayment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_PaymentId",
                table: "InvoicePayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_RegisterId",
                table: "InvoicePayment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_ShiftId",
                table: "InvoicePayment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayment_UserId",
                table: "InvoicePayment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_License_CreatedById",
                table: "License",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_License_Guid",
                table: "License",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_License_ModifiedById",
                table: "License",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_License_Name",
                table: "License",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_AssignedHostId",
                table: "LicenseKey",
                column: "AssignedHostId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_CreatedById",
                table: "LicenseKey",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_Guid",
                table: "LicenseKey",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_LicenseId",
                table: "LicenseKey",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_ModifiedById",
                table: "LicenseKey",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Category",
                table: "Log",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Log_HostNumber",
                table: "Log",
                column: "HostNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Log_MessageType",
                table: "Log",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Time",
                table: "Log",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_LogException_LogId",
                table: "LogException",
                column: "LogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_CreatedById",
                table: "Mapping",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_ModifiedById",
                table: "Mapping",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Mapping_MountPoint",
                table: "Mapping",
                column: "MountPoint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryUnit_CreatedById",
                table: "MonetaryUnit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryUnit_ModifiedById",
                table: "MonetaryUnit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryUnit_Name",
                table: "MonetaryUnit",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatedById",
                table: "News",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_News_ModifiedById",
                table: "News",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_NewsBranch_BranchId",
                table: "NewsBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsBranch_NewsId_BranchId",
                table: "NewsBranch",
                columns: new[] { "NewsId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_CreatedById",
                table: "Note",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ModifiedById",
                table: "Note",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_NoteId",
                table: "Note",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CreatedById",
                table: "Notification",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ModifiedById",
                table: "Notification",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BranchId",
                table: "Payment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreatedById",
                table: "Payment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_DepositTransactionId",
                table: "Payment",
                column: "DepositTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ModifiedById",
                table: "Payment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PointTransactionId",
                table: "Payment",
                column: "PointTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RegisterId",
                table: "Payment",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ShiftId",
                table: "Payment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_BranchId",
                table: "PaymentIntent",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_CreatedById",
                table: "PaymentIntent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_Guid",
                table: "PaymentIntent",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_ModifiedById",
                table: "PaymentIntent",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_PaymentId",
                table: "PaymentIntent",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_PaymentIntentId",
                table: "PaymentIntent",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_PaymentMethodId",
                table: "PaymentIntent",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_UserId",
                table: "PaymentIntent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentDeposit_DepositPaymentId",
                table: "PaymentIntentDeposit",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentDeposit_PaymentIntentId",
                table: "PaymentIntentDeposit",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_CreatedById",
                table: "PaymentMethod",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_ModifiedById",
                table: "PaymentMethod",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_Name",
                table: "PaymentMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_CreatedById",
                table: "PaymentReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_RegisterId",
                table: "PaymentReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipt_ShiftId",
                table: "PaymentReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_CreatedById",
                table: "PersonalFile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_Guid",
                table: "PersonalFile",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_ModifiedById",
                table: "PersonalFile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_Name",
                table: "PersonalFile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginLibrary_CreatedById",
                table: "PluginLibrary",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PluginLibrary_FileName",
                table: "PluginLibrary",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginLibrary_ModifiedById",
                table: "PluginLibrary",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_CreatedById",
                table: "PointTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_ModifiedById",
                table: "PointTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_RegisterId",
                table: "PointTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_ShiftId",
                table: "PointTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_PointTransaction_UserId",
                table: "PointTransaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PresetReservationTime_CreatedById",
                table: "PresetReservationTime",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetReservationTime_ModifiedById",
                table: "PresetReservationTime",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTimeSale_CreatedById",
                table: "PresetTimeSale",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTimeSale_ModifiedById",
                table: "PresetTimeSale",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTimeSaleMoney_CreatedById",
                table: "PresetTimeSaleMoney",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTimeSaleMoney_ModifiedById",
                table: "PresetTimeSaleMoney",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTopUp_CreatedById",
                table: "PresetTopUp",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PresetTopUp_ModifiedById",
                table: "PresetTopUp",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductId",
                table: "Product",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_Barcode",
                table: "ProductBase",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_CreatedById",
                table: "ProductBase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_ModifiedById",
                table: "ProductBase",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_Name",
                table: "ProductBase",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_ProductGroupId",
                table: "ProductBase",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_ProductId",
                table: "ProductBase",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_StockProductId",
                table: "ProductBase",
                column: "StockProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBaseExtended_ProductId",
                table: "ProductBaseExtended",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBranch_BranchId",
                table: "ProductBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBranch_ProductId_BranchId",
                table: "ProductBranch",
                columns: new[] { "ProductId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundle_ProductId",
                table: "ProductBundle",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundleUserPrice_CreatedById",
                table: "ProductBundleUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundleUserPrice_ModifiedById",
                table: "ProductBundleUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundleUserPrice_ProductBundleId_UserGroupId",
                table: "ProductBundleUserPrice",
                columns: new[] { "ProductBundleId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundleUserPrice_UserGroupId",
                table: "ProductBundleUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_CreatedById",
                table: "ProductGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ModifiedById",
                table: "ProductGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_Name",
                table: "ProductGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ParentId",
                table: "ProductGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHostHidden_CreatedById",
                table: "ProductHostHidden",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHostHidden_HostGroupId",
                table: "ProductHostHidden",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHostHidden_ModifiedById",
                table: "ProductHostHidden",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductHostHidden_ProductId_HostGroupId",
                table: "ProductHostHidden",
                columns: new[] { "ProductId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_CreatedById",
                table: "ProductImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ModifiedById",
                table: "ProductImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_CreatedById",
                table: "ProductOL",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ModifiedById",
                table: "ProductOL",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ProductOLId",
                table: "ProductOL",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ProductOrderId",
                table: "ProductOL",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_RegisterId",
                table: "ProductOL",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ReservationHostId",
                table: "ProductOL",
                column: "ReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ReservationId",
                table: "ProductOL",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_ShiftId",
                table: "ProductOL",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOL_UserId",
                table: "ProductOL",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLExtended_BundleLineId",
                table: "ProductOLExtended",
                column: "BundleLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLExtended_ProductOLId",
                table: "ProductOLExtended",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLProduct_ProductId",
                table: "ProductOLProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLProduct_ProductOLId",
                table: "ProductOLProduct",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLReservationFee_ProductOLId",
                table: "ProductOLReservationFee",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLSession_ProductOLId",
                table: "ProductOLSession",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLSession_UsageSessionId",
                table: "ProductOLSession",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLTime_ProductOLId",
                table: "ProductOLTime",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLTime_ProductTimeId",
                table: "ProductOLTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOLTimeFixed_ProductOLId",
                table: "ProductOLTimeFixed",
                column: "ProductOLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_BranchId",
                table: "ProductOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_CreatedById",
                table: "ProductOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_HostId",
                table: "ProductOrder",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ModifiedById",
                table: "ProductOrder",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_PreferredPaymentMethodId",
                table: "ProductOrder",
                column: "PreferredPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_RegisterId",
                table: "ProductOrder",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ShiftId",
                table: "ProductOrder",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_UserId",
                table: "ProductOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_CreatedById",
                table: "ProductOrderDiscount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_DiscountId",
                table: "ProductOrderDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderId",
                table: "ProductOrderDiscount",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ProductOrderLineId",
                table: "ProductOrderDiscount",
                column: "ProductOrderLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionCodeId",
                table: "ProductOrderDiscount",
                column: "PromotionCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_PromotionId",
                table: "ProductOrderDiscount",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_RegisterId",
                table: "ProductOrderDiscount",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_ShiftId",
                table: "ProductOrderDiscount",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderDiscount_UserId",
                table: "ProductOrderDiscount",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPeriod_ProductId",
                table: "ProductPeriod",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPeriodDay_ProductPeriodId_Day",
                table: "ProductPeriodDay",
                columns: new[] { "ProductPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPeriodDayTime_PeriodDayId",
                table: "ProductPeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_CreatedById",
                table: "ProductTax",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_ModifiedById",
                table: "ProductTax",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_ProductId_TaxId",
                table: "ProductTax",
                columns: new[] { "ProductId", "TaxId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTax_TaxId",
                table: "ProductTax",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTime_AppGroupId",
                table: "ProductTime",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTime_ProductId",
                table: "ProductTime",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeHostDisallowed_CreatedById",
                table: "ProductTimeHostDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeHostDisallowed_HostGroupId",
                table: "ProductTimeHostDisallowed",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeHostDisallowed_ModifiedById",
                table: "ProductTimeHostDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimeHostDisallowed_ProductTimeId_HostGroupId",
                table: "ProductTimeHostDisallowed",
                columns: new[] { "ProductTimeId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimePeriod_ProductId",
                table: "ProductTimePeriod",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimePeriodDay_ProductTimePeriodDayId",
                table: "ProductTimePeriodDay",
                column: "ProductTimePeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimePeriodDay_ProductTimePeriodId_Day",
                table: "ProductTimePeriodDay",
                columns: new[] { "ProductTimePeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTimePeriodDayTime_PeriodDayId",
                table: "ProductTimePeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserDisallowed_CreatedById",
                table: "ProductUserDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserDisallowed_ModifiedById",
                table: "ProductUserDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserDisallowed_ProductId_UserGroupId",
                table: "ProductUserDisallowed",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserDisallowed_UserGroupId",
                table: "ProductUserDisallowed",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_CreatedById",
                table: "ProductUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_ModifiedById",
                table: "ProductUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_ProductId_UserGroupId",
                table: "ProductUserPrice",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_UserGroupId",
                table: "ProductUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_CreatedById",
                table: "Promotion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_ModifiedById",
                table: "Promotion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionBranch_BranchId",
                table: "PromotionBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionBranch_PromotionId_BranchId",
                table: "PromotionBranch",
                columns: new[] { "PromotionId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_CreatedById",
                table: "PromotionCode",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_ModifiedById",
                table: "PromotionCode",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_PromotionId",
                table: "PromotionCode",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCode_Value",
                table: "PromotionCode",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscount_DiscountId",
                table: "PromotionDiscount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDiscountGroup_DiscountGroupId",
                table: "PromotionDiscountGroup",
                column: "DiscountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriod_PromotionId",
                table: "PromotionPeriod",
                column: "PromotionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriodDay_PromotionPeriodId_Day",
                table: "PromotionPeriodDay",
                columns: new[] { "PromotionPeriodId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPeriodDayTime_PromotionPeriodDayId",
                table: "PromotionPeriodDayTime",
                column: "PromotionPeriodDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_CreatedById",
                table: "Recipient",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientChannel_CreatedById",
                table: "RecipientChannel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RecipientChannel_RecipientId_ChannelType",
                table: "RecipientChannel",
                columns: new[] { "RecipientId", "ChannelType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_BranchId",
                table: "Refund",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_CreatedById",
                table: "Refund",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_DepositTransactionId",
                table: "Refund",
                column: "DepositTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PaymentId",
                table: "Refund",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_PointTransactionId",
                table: "Refund",
                column: "PointTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_RefundId",
                table: "Refund",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refund_RefundMethodId",
                table: "Refund",
                column: "RefundMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_RegisterId",
                table: "Refund",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Refund_ShiftId",
                table: "Refund",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundDepositPayment_DepositPaymentId",
                table: "RefundDepositPayment",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundDepositPayment_FiscalReceiptId",
                table: "RefundDepositPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundDepositPayment_RefundId",
                table: "RefundDepositPayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundInvoicePayment_InvoiceId",
                table: "RefundInvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundInvoicePayment_InvoicePaymentId",
                table: "RefundInvoicePayment",
                column: "InvoicePaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundInvoicePayment_RefundId",
                table: "RefundInvoicePayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundPayment_FiscalReceiptId",
                table: "RefundPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_CreatedById",
                table: "RefundReceipt",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_RegisterId",
                table: "RefundReceipt",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundReceipt_ShiftId",
                table: "RefundReceipt",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_BranchId",
                table: "Register",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_CompanionId",
                table: "Register",
                column: "CompanionId");

            migrationBuilder.CreateIndex(
                name: "IX_Register_CreatedById",
                table: "Register",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Register_MacAddress",
                table: "Register",
                column: "MacAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_ModifiedById",
                table: "Register",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Register_Name_BranchId",
                table: "Register",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_StockId",
                table: "Register",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_CreatedById",
                table: "RegisterTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_ModifiedById",
                table: "RegisterTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_RegisterId",
                table: "RegisterTransaction",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterTransaction_ShiftId",
                table: "RegisterTransaction",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_CreatedById",
                table: "ReportPreset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_ModifiedById",
                table: "ReportPreset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPreset_Name",
                table: "ReportPreset",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CreatedById",
                table: "Reservation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FinalizedById",
                table: "Reservation",
                column: "FinalizedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ModifiedById",
                table: "Reservation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Pin",
                table: "Reservation",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Status",
                table: "Reservation",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_CreatedById",
                table: "ReservationHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_FinalizedById",
                table: "ReservationHost",
                column: "FinalizedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_HostId",
                table: "ReservationHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_MovedToReservationHostId",
                table: "ReservationHost",
                column: "MovedToReservationHostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_PreferredUserId",
                table: "ReservationHost",
                column: "PreferredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_ReservationId_HostId",
                table: "ReservationHost",
                columns: new[] { "ReservationId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_Status",
                table: "ReservationHost",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationProductOrder_CreatedById",
                table: "ReservationProductOrder",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationProductOrder_ProductOrderId",
                table: "ReservationProductOrder",
                column: "ProductOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationProductOrder_ReservationId_ProductOrderId",
                table: "ReservationProductOrder",
                columns: new[] { "ReservationId", "ProductOrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationUser_CreatedById",
                table: "ReservationUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationUser_ModifiedById",
                table: "ReservationUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationUser_ReservationId_UserId",
                table: "ReservationUser",
                columns: new[] { "ReservationId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationUser_UserId",
                table: "ReservationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CreatedById",
                table: "Schedule",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ModifiedById",
                table: "Schedule",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Name",
                table: "Schedule",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_CreatedById",
                table: "ScheduleReportEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_ReportPresetId",
                table: "ScheduleReportEntry",
                column: "ReportPresetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportEntry_ScheduleReportId",
                table: "ScheduleReportEntry",
                column: "ScheduleReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_ScheduleReportId_UserId",
                table: "ScheduleReportRecipient",
                columns: new[] { "ScheduleReportId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleReportRecipient_UserId",
                table: "ScheduleReportRecipient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfile_CreatedById",
                table: "SecurityProfile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfile_ModifiedById",
                table: "SecurityProfile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfile_Name",
                table: "SecurityProfile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfilePolicy_CreatedById",
                table: "SecurityProfilePolicy",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfilePolicy_ModifiedById",
                table: "SecurityProfilePolicy",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfilePolicy_SecurityProfileId_Type",
                table: "SecurityProfilePolicy",
                columns: new[] { "SecurityProfileId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileRestriction_CreatedById",
                table: "SecurityProfileRestriction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileRestriction_ModifiedById",
                table: "SecurityProfileRestriction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfileRestriction_SecurityProfileId",
                table: "SecurityProfileRestriction",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_CreatedById",
                table: "Setting",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_ModifiedById",
                table: "Setting",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Setting_Name_GroupName",
                table: "Setting",
                columns: new[] { "Name", "GroupName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BranchId",
                table: "Shift",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_CreatedById",
                table: "Shift",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_EndedById",
                table: "Shift",
                column: "EndedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ModifiedById",
                table: "Shift",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_OperatorId",
                table: "Shift",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_RegisterId",
                table: "Shift",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ShiftId",
                table: "Shift",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCount_CreatedById",
                table: "ShiftCount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCount_ModifiedById",
                table: "ShiftCount",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCount_PaymentMethodId",
                table: "ShiftCount",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCount_ShiftId_PaymentMethodId",
                table: "ShiftCount",
                columns: new[] { "ShiftId", "PaymentMethodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_BranchId",
                table: "Stock",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_CreatedById",
                table: "Stock",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ModifiedById",
                table: "Stock",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Name_BranchId",
                table: "Stock",
                columns: new[] { "Name", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_CreatedById",
                table: "StockCount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_RegisterId",
                table: "StockCount",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_ShiftId",
                table: "StockCount",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_StockId",
                table: "StockCount",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountAdjustment_AdjustmentId",
                table: "StockCountAdjustment",
                column: "AdjustmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_CreatedById",
                table: "StockCountEntry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_ProductId_StockCountId",
                table: "StockCountEntry",
                columns: new[] { "ProductId", "StockCountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCountEntry_StockCountId",
                table: "StockCountEntry",
                column: "StockCountId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCountInbound_InboundId",
                table: "StockCountInbound",
                column: "InboundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_CreatedById",
                table: "StockTransaction",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ModifiedById",
                table: "StockTransaction",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_ProductId",
                table: "StockTransaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_SourceProductId",
                table: "StockTransaction",
                column: "SourceProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_StockId",
                table: "StockTransaction",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_CreatedById",
                table: "Target",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBillProfile_BillProfileId",
                table: "TargetBillProfile",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBillProfile_TargetGroupBillProfileId_BillProfileId",
                table: "TargetBillProfile",
                columns: new[] { "TargetGroupBillProfileId", "BillProfileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_CreatedById",
                table: "TargetGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_DiscountId",
                table: "TargetGroup",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroup_ModifiedById",
                table: "TargetGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPaymentMethod_MethodId",
                table: "TargetPaymentMethod",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPaymentMethod_TargetGroupPaymentMethodId_MethodId",
                table: "TargetPaymentMethod",
                columns: new[] { "TargetGroupPaymentMethodId", "MethodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetProduct_ProductId",
                table: "TargetProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProduct_TargetGroupProductId_ProductId",
                table: "TargetProduct",
                columns: new[] { "TargetGroupProductId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductGroup_ProductGroupId",
                table: "TargetProductGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductGroup_TargetGroupProductGroupId_ProductGroupId",
                table: "TargetProductGroup",
                columns: new[] { "TargetGroupProductGroupId", "ProductGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductTime_ProductTimeId",
                table: "TargetProductTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetProductTime_TargetGroupProductTimeId_ProductTimeId",
                table: "TargetProductTime",
                columns: new[] { "TargetGroupProductTimeId", "ProductTimeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_CreatedById",
                table: "TaskBase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_Guid",
                table: "TaskBase",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_ModifiedById",
                table: "TaskBase",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_Name",
                table: "TaskBase",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_TaskId",
                table: "TaskBase",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskJunction_TaskId",
                table: "TaskJunction",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskNotification_TaskId",
                table: "TaskNotification",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskProcess_TaskId",
                table: "TaskProcess",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskScript_TaskId",
                table: "TaskScript",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tax_CreatedById",
                table: "Tax",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_ModifiedById",
                table: "Tax",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tax_Name",
                table: "Tax",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Token_CreatedById",
                table: "Token",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Token_ModifiedById",
                table: "Token",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_Value",
                table: "Token",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usage_UsageId",
                table: "Usage",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usage_UsageSessionId",
                table: "Usage",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usage_UserId",
                table: "Usage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRate_BillRateId",
                table: "UsageRate",
                column: "BillRateId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRate_DiscountId",
                table: "UsageRate",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageRate_UsageId",
                table: "UsageRate",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_BranchId",
                table: "UsageSession",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_CurrentUsageId",
                table: "UsageSession",
                column: "CurrentUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_UsageSessionId",
                table: "UsageSession",
                column: "UsageSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageSession_UserId",
                table: "UsageSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageTime_InvoiceLineId",
                table: "UsageTime",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageTime_UsageId",
                table: "UsageTime",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageTimeFixed_InvoiceLineId",
                table: "UsageTimeFixed",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageTimeFixed_UsageId",
                table: "UsageTimeFixed",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageUserSession_UsageId",
                table: "UsageUserSession",
                column: "UsageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageUserSession_UserSessionId",
                table: "UsageUserSession",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BranchId",
                table: "User",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_Guid",
                table: "User",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Identification",
                table: "User",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ModifiedById",
                table: "User",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_PermissionSetId",
                table: "User",
                column: "PermissionSetId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SmartCardUID",
                table: "User",
                column: "SmartCardUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId",
                table: "User",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreement_CreatedById",
                table: "UserAgreement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreement_ModifiedById",
                table: "UserAgreement",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreementState_CreatedById",
                table: "UserAgreementState",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreementState_ModifiedById",
                table: "UserAgreementState",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreementState_UserAgreementId_UserId",
                table: "UserAgreementState",
                columns: new[] { "UserAgreementId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreementState_UserId",
                table: "UserAgreementState",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApiKey_ApiKey",
                table: "UserApiKey",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApiKey_UserId",
                table: "UserApiKey",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAttribute_AttributeId",
                table: "UserAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttribute_CreatedById",
                table: "UserAttribute",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttribute_ModifiedById",
                table: "UserAttribute",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttribute_UserId_AttributeId",
                table: "UserAttribute",
                columns: new[] { "UserId", "AttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_CreatedById",
                table: "UserChannel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_ModifiedById",
                table: "UserChannel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_UserId_Channel",
                table: "UserChannel",
                columns: new[] { "UserId", "Channel" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCredential_CreatedById",
                table: "UserCredential",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserCredential_ModifiedById",
                table: "UserCredential",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserCredential_UserId",
                table: "UserCredential",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditLimit_CreatedById",
                table: "UserCreditLimit",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditLimit_ModifiedById",
                table: "UserCreditLimit",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserCreditLimit_UserId",
                table: "UserCreditLimit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_AppGroupId",
                table: "UserGroup",
                column: "AppGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_BillProfileId",
                table: "UserGroup",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_CreatedById",
                table: "UserGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_DiscountGroupId",
                table: "UserGroup",
                column: "DiscountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_ModifiedById",
                table: "UserGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Name",
                table: "UserGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_SecurityProfileId",
                table: "UserGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupHostDisallowed_CreatedById",
                table: "UserGroupHostDisallowed",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupHostDisallowed_HostGroupId",
                table: "UserGroupHostDisallowed",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupHostDisallowed_ModifiedById",
                table: "UserGroupHostDisallowed",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupHostDisallowed_UserGroupId_HostGroupId",
                table: "UserGroupHostDisallowed",
                columns: new[] { "UserGroupId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGuest_ReservedHostId_ReservedSlot",
                table: "UserGuest",
                columns: new[] { "ReservedHostId", "ReservedSlot" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGuest_UserId",
                table: "UserGuest",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMember_Email",
                table: "UserMember",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMember_UserGroupId",
                table: "UserMember",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMember_UserId",
                table: "UserMember",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMember_Username",
                table: "UserMember",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNote_NoteId",
                table: "UserNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNote_UserId",
                table: "UserNote",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperator_Email",
                table: "UserOperator",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperator_UserId",
                table: "UserOperator",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperator_Username",
                table: "UserOperator",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_BranchId_OperatorId",
                table: "UserOperatorBranch",
                columns: new[] { "BranchId", "OperatorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_CreatedById",
                table: "UserOperatorBranch",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_ModifiedById",
                table: "UserOperatorBranch",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperatorBranch_OperatorId",
                table: "UserOperatorBranch",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_CreatedById",
                table: "UserPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_ModifiedById",
                table: "UserPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_UserId_Type_Value",
                table: "UserPermission",
                columns: new[] { "UserId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_CreatedById",
                table: "UserPermissionSet",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_ModifiedById",
                table: "UserPermissionSet",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSet_Name",
                table: "UserPermissionSet",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionSetPermission_PermissionSetId_Type_Value",
                table: "UserPermissionSetPermission",
                columns: new[] { "PermissionSetId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPicture_CreatedById",
                table: "UserPicture",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPicture_ModifiedById",
                table: "UserPicture",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPicture_UserId",
                table: "UserPicture",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_BranchId",
                table: "UserSession",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_CreatedById",
                table: "UserSession",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_HostId",
                table: "UserSession",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_UserId",
                table: "UserSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionChange_CreatedById",
                table: "UserSessionChange",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionChange_HostId",
                table: "UserSessionChange",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionChange_UserId",
                table: "UserSessionChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionChange_UserSessionId",
                table: "UserSessionChange",
                column: "UserSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Variable_CreatedById",
                table: "Variable",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Variable_ModifiedById",
                table: "Variable",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Variable_Name",
                table: "Variable",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verification_CreatedById",
                table: "Verification",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ModifiedById",
                table: "Verification",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_TokenId",
                table: "Verification",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_UserId",
                table: "Verification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_VerificationId",
                table: "Verification",
                column: "VerificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationEmail_VerificationId",
                table: "VerificationEmail",
                column: "VerificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationMobilePhone_VerificationId",
                table: "VerificationMobilePhone",
                column: "VerificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Void_BranchId",
                table: "Void",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Void_CreatedById",
                table: "Void",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Void_RegisterId",
                table: "Void",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Void_ShiftId",
                table: "Void",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Void_VoidId",
                table: "Void",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidDepositPayment_DepositPaymentId",
                table: "VoidDepositPayment",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidDepositPayment_VoidId",
                table: "VoidDepositPayment",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidInvoice_InvoiceId",
                table: "VoidInvoice",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidInvoice_VoidId",
                table: "VoidInvoice",
                column: "VoidId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AgeRestriction_UserOperator_CreatedById",
                table: "AgeRestriction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgeRestrictionProduct_ProductBase_ProductId",
                table: "AgeRestrictionProduct",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppCategory_AppCategoryId",
                table: "App",
                column: "AppCategoryId",
                principalTable: "AppCategory",
                principalColumn: "AppCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppEnterprise_DeveloperId",
                table: "App",
                column: "DeveloperId",
                principalTable: "AppEnterprise",
                principalColumn: "AppEnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_AppEnterprise_PublisherId",
                table: "App",
                column: "PublisherId",
                principalTable: "AppEnterprise",
                principalColumn: "AppEnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_UserOperator_CreatedById",
                table: "App",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_UserOperator_ModifiedById",
                table: "App",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategory_UserOperator_CreatedById",
                table: "AppCategory",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCategory_UserOperator_ModifiedById",
                table: "AppCategory",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEnterprise_UserOperator_CreatedById",
                table: "AppEnterprise",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEnterprise_UserOperator_ModifiedById",
                table: "AppEnterprise",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_Deployment_DefaultDeploymentId",
                table: "AppExe",
                column: "DefaultDeploymentId",
                principalTable: "Deployment",
                principalColumn: "DeploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_UserOperator_CreatedById",
                table: "AppExe",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExe_UserOperator_ModifiedById",
                table: "AppExe",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeBranch_Branch_BranchId",
                table: "AppExeBranch",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeCdImage_UserOperator_CreatedById",
                table: "AppExeCdImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeCdImage_UserOperator_ModifiedById",
                table: "AppExeCdImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_Deployment_DeploymentId",
                table: "AppExeDeployment",
                column: "DeploymentId",
                principalTable: "Deployment",
                principalColumn: "DeploymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_UserOperator_CreatedById",
                table: "AppExeDeployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeDeployment_UserOperator_ModifiedById",
                table: "AppExeDeployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeImage_UserOperator_CreatedById",
                table: "AppExeImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeImage_UserOperator_ModifiedById",
                table: "AppExeImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_License_LicenseId",
                table: "AppExeLicense",
                column: "LicenseId",
                principalTable: "License",
                principalColumn: "LicenseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_UserOperator_CreatedById",
                table: "AppExeLicense",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeLicense_UserOperator_ModifiedById",
                table: "AppExeLicense",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeMaxUser_UserOperator_CreatedById",
                table: "AppExeMaxUser",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeMaxUser_UserOperator_ModifiedById",
                table: "AppExeMaxUser",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_PersonalFile_PersonalFileId",
                table: "AppExePersonalFile",
                column: "PersonalFileId",
                principalTable: "PersonalFile",
                principalColumn: "PersonalFileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_UserOperator_CreatedById",
                table: "AppExePersonalFile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExePersonalFile_UserOperator_ModifiedById",
                table: "AppExePersonalFile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_TaskBase_TaskBaseId",
                table: "AppExeTask",
                column: "TaskBaseId",
                principalTable: "TaskBase",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_UserOperator_CreatedById",
                table: "AppExeTask",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExeTask_UserOperator_ModifiedById",
                table: "AppExeTask",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGroup_UserOperator_CreatedById",
                table: "AppGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGroup_UserOperator_ModifiedById",
                table: "AppGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImage_UserOperator_CreatedById",
                table: "AppImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImage_UserOperator_ModifiedById",
                table: "AppImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLink_UserOperator_CreatedById",
                table: "AppLink",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLink_UserOperator_ModifiedById",
                table: "AppLink",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRating_UserMember_UserId",
                table: "AppRating",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_Branch_BranchId",
                table: "AppStat",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_HostComputer_HostId",
                table: "AppStat",
                column: "HostId",
                principalTable: "HostComputer",
                principalColumn: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppStat_UserMember_UserId",
                table: "AppStat",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AssetType_AssetTypeId",
                table: "Asset",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "AssetTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Branch_BranchId",
                table: "Asset",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_UserOperator_CreatedById",
                table: "Asset",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_UserOperator_ModifiedById",
                table: "Asset",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_AssetType_AssetTypeId",
                table: "AssetTransaction",
                column: "AssetTypeId",
                principalTable: "AssetType",
                principalColumn: "AssetTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_Branch_BranchId",
                table: "AssetTransaction",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserMember_UserId",
                table: "AssetTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_CheckedInById",
                table: "AssetTransaction",
                column: "CheckedInById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_CreatedById",
                table: "AssetTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetTransaction_UserOperator_ModifiedById",
                table: "AssetTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetType_UserOperator_CreatedById",
                table: "AssetType",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetType_UserOperator_ModifiedById",
                table: "AssetType",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_AssistanceRequestType_AssistanceRequestTy~",
                table: "AssistanceRequest",
                column: "AssistanceRequestTypeId",
                principalTable: "AssistanceRequestType",
                principalColumn: "AssistanceRequestTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Branch_BranchId",
                table: "AssistanceRequest",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Host_HostId",
                table: "AssistanceRequest",
                column: "HostId",
                principalTable: "Host",
                principalColumn: "HostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_UserMember_UserId",
                table: "AssistanceRequest",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_User_CreatedById",
                table: "AssistanceRequest",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_User_ModifiedById",
                table: "AssistanceRequest",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequestType_UserOperator_CreatedById",
                table: "AssistanceRequestType",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequestType_UserOperator_ModifiedById",
                table: "AssistanceRequestType",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_UserOperator_CreatedById",
                table: "Attribute",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attribute_UserOperator_ModifiedById",
                table: "Attribute",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillProfile_UserOperator_CreatedById",
                table: "BillProfile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillProfile_UserOperator_ModifiedById",
                table: "BillProfile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Companion_CompanionId",
                table: "Branch",
                column: "CompanionId",
                principalTable: "Companion",
                principalColumn: "CompanionId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_UserOperator_CreatedById",
                table: "Branch",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_UserOperator_ModifiedById",
                table: "Branch",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_ProductBase_ProductId",
                table: "BundleProduct",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_ProductBundle_ProductBundleId",
                table: "BundleProduct",
                column: "ProductBundleId",
                principalTable: "ProductBundle",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_UserOperator_CreatedById",
                table: "BundleProduct",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProduct_UserOperator_ModifiedById",
                table: "BundleProduct",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserGroup_UserGroupId",
                table: "BundleProductUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserOperator_CreatedById",
                table: "BundleProductUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BundleProductUserPrice_UserOperator_ModifiedById",
                table: "BundleProductUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOptions_UserOperator_CreatedById",
                table: "ClientOptions",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientOptions_UserOperator_ModifiedById",
                table: "ClientOptions",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_TaskBase_TaskBaseId",
                table: "ClientTask",
                column: "TaskBaseId",
                principalTable: "TaskBase",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_UserOperator_CreatedById",
                table: "ClientTask",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTask_UserOperator_ModifiedById",
                table: "ClientTask",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companion_UserOperator_CreatedById",
                table: "Companion",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companion_UserOperator_ModifiedById",
                table: "Companion",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_UserOperator_CreatedById",
                table: "Deployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deployment_UserOperator_ModifiedById",
                table: "Deployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeploymentDeployment_UserOperator_CreatedById",
                table: "DeploymentDeployment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeploymentDeployment_UserOperator_ModifiedById",
                table: "DeploymentDeployment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_DepositTransaction_DepositTransactionId",
                table: "DepositPayment",
                column: "DepositTransactionId",
                principalTable: "DepositTransaction",
                principalColumn: "DepositTransactionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_FiscalReceipt_FiscalReceiptId",
                table: "DepositPayment",
                column: "FiscalReceiptId",
                principalTable: "FiscalReceipt",
                principalColumn: "FiscalReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Payment_PaymentId",
                table: "DepositPayment",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Register_RegisterId",
                table: "DepositPayment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_Shift_ShiftId",
                table: "DepositPayment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserMember_UserId",
                table: "DepositPayment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserOperator_CreatedById",
                table: "DepositPayment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositPayment_UserOperator_ModifiedById",
                table: "DepositPayment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_Register_RegisterId",
                table: "DepositTransaction",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_Shift_ShiftId",
                table: "DepositTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserMember_UserId",
                table: "DepositTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserOperator_CreatedById",
                table: "DepositTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepositTransaction_UserOperator_ModifiedById",
                table: "DepositTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_UserOperator_CreatedById",
                table: "Device",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_UserOperator_ModifiedById",
                table: "Device",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_Host_HostId",
                table: "DeviceHost",
                column: "HostId",
                principalTable: "Host",
                principalColumn: "HostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_UserOperator_CreatedById",
                table: "DeviceHost",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHost_UserOperator_ModifiedById",
                table: "DeviceHost",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_UserOperator_CreatedById",
                table: "Discount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_UserOperator_ModifiedById",
                table: "Discount",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountGroup_UserOperator_CreatedById",
                table: "DiscountGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountGroup_UserOperator_ModifiedById",
                table: "DiscountGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentType_UserOperator_CreatedById",
                table: "DocumentType",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentType_UserOperator_ModifiedById",
                table: "DocumentType",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_UserOperator_CreatedById",
                table: "Feed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_UserOperator_ModifiedById",
                table: "Feed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_UserOperator_CreatedById",
                table: "File",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_UserOperator_ModifiedById",
                table: "File",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_Register_RegisterId",
                table: "FiscalReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_Shift_ShiftId",
                table: "FiscalReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_FiscalReceipt_UserOperator_CreatedById",
                table: "FiscalReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_HostGroup_HostGroupId",
                table: "Host",
                column: "HostGroupId",
                principalTable: "HostGroup",
                principalColumn: "HostGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_Icon_IconId",
                table: "Host",
                column: "IconId",
                principalTable: "Icon",
                principalColumn: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_UserOperator_CreatedById",
                table: "Host",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Host_UserOperator_ModifiedById",
                table: "Host",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_SecurityProfile_SecurityProfileId",
                table: "HostGroup",
                column: "SecurityProfileId",
                principalTable: "SecurityProfile",
                principalColumn: "SecurityProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserGroup_DefaultGuestGroupId",
                table: "HostGroup",
                column: "DefaultGuestGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserOperator_CreatedById",
                table: "HostGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroup_UserOperator_ModifiedById",
                table: "HostGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupUserBillProfile_UserGroup_UserGroupId",
                table: "HostGroupUserBillProfile",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLine_UserOperator_CreatedById",
                table: "HostGroupWaitingLine",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLine_UserOperator_ModifiedById",
                table: "HostGroupWaitingLine",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_UserMember_UserId",
                table: "HostGroupWaitingLineEntry",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_UserOperator_CreatedById",
                table: "HostGroupWaitingLineEntry",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostGroupWaitingLineEntry_User_ModifiedById",
                table: "HostGroupWaitingLineEntry",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_UserOperator_CreatedById",
                table: "HostLayoutGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroup_UserOperator_ModifiedById",
                table: "HostLayoutGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupImage_UserOperator_CreatedById",
                table: "HostLayoutGroupImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupImage_UserOperator_ModifiedById",
                table: "HostLayoutGroupImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupLayout_UserOperator_CreatedById",
                table: "HostLayoutGroupLayout",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HostLayoutGroupLayout_UserOperator_ModifiedById",
                table: "HostLayoutGroupLayout",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Icon_UserOperator_CreatedById",
                table: "Icon",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Icon_UserOperator_ModifiedById",
                table: "Icon",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrder_InvoicePayment_InvoicePaymentId",
                table: "IntentOrder",
                column: "InvoicePaymentId",
                principalTable: "InvoicePayment",
                principalColumn: "InvoicePaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrder_PaymentIntentOrder_PaymentIntentOrderId",
                table: "IntentOrder",
                column: "PaymentIntentOrderId",
                principalTable: "PaymentIntentOrder",
                principalColumn: "PaymentIntentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrder_ProductOrder_ProductOrderId",
                table: "IntentOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrder_User_CreatedById",
                table: "IntentOrder",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrder_User_ModifiedById",
                table: "IntentOrder",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrderDeposit_PaymentIntentOrder_PaymentIntentOrderId",
                table: "IntentOrderDeposit",
                column: "PaymentIntentOrderId",
                principalTable: "PaymentIntentOrder",
                principalColumn: "PaymentIntentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrderDeposit_UserMember_UserId",
                table: "IntentOrderDeposit",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrderDeposit_User_CreatedById",
                table: "IntentOrderDeposit",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntentOrderDeposit_User_ModifiedById",
                table: "IntentOrderDeposit",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Shift_ShiftId",
                table: "Inventory",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Stock_StockId",
                table: "Inventory",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_UserOperator_CreatedById",
                table: "Inventory",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentEntry_InventoryAdjustmentReason_Adjustme~",
                table: "InventoryAdjustmentEntry",
                column: "AdjustmentReasonId",
                principalTable: "InventoryAdjustmentReason",
                principalColumn: "InventoryAdjustmentReasonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentEntry_InventoryEntry_InventoryEntryId",
                table: "InventoryAdjustmentEntry",
                column: "InventoryEntryId",
                principalTable: "InventoryEntry",
                principalColumn: "InventoryEntryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentReason_UserOperator_CreatedById",
                table: "InventoryAdjustmentReason",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryAdjustmentReason_UserOperator_ModifiedById",
                table: "InventoryAdjustmentReason",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDocument_UserOperator_CreatedById",
                table: "InventoryDocument",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEntry_ProductBase_ProductId",
                table: "InventoryEntry",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEntry_Shift_ShiftId",
                table: "InventoryEntry",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEntry_StockTransaction_StockTransactionId",
                table: "InventoryEntry",
                column: "StockTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEntry_Stock_StockId",
                table: "InventoryEntry",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryEntry_UserOperator_CreatedById",
                table: "InventoryEntry",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound",
                column: "InventoryTransferId",
                principalTable: "InventoryTransfer",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInboundEntry_InventoryTransferEntry_InventoryTrans~",
                table: "InventoryInboundEntry",
                column: "InventoryTransferEntryId",
                principalTable: "InventoryTransferEntry",
                principalColumn: "InventoryEntryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_Stock_TransferStockId",
                table: "InventoryTransfer",
                column: "TransferStockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferEntry_InventoryTransferReason_TransferReas~",
                table: "InventoryTransferEntry",
                column: "TransferReasonId",
                principalTable: "InventoryTransferReason",
                principalColumn: "InventoryTransferReasonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferReason_UserOperator_CreatedById",
                table: "InventoryTransferReason",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferReason_UserOperator_ModifiedById",
                table: "InventoryTransferReason",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_ProductOrder_ProductOrderId",
                table: "Invoice",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Register_RegisterId",
                table: "Invoice",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Shift_ShiftId",
                table: "Invoice",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserMember_UserId",
                table: "Invoice",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserOperator_CreatedById",
                table: "Invoice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserOperator_ModifiedById",
                table: "Invoice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_Register_RegisterId",
                table: "InvoiceFiscalReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_Shift_ShiftId",
                table: "InvoiceFiscalReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceFiscalReceipt_UserOperator_CreatedById",
                table: "InvoiceFiscalReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_PointTransaction_PointsTransactionId",
                table: "InvoiceLine",
                column: "PointsTransactionId",
                principalTable: "PointTransaction",
                principalColumn: "PointTransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Register_RegisterId",
                table: "InvoiceLine",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_ReservationHost_ReservationHostId",
                table: "InvoiceLine",
                column: "ReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Reservation_ReservationId",
                table: "InvoiceLine",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_Shift_ShiftId",
                table: "InvoiceLine",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserMember_UserId",
                table: "InvoiceLine",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserOperator_CreatedById",
                table: "InvoiceLine",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLine_UserOperator_ModifiedById",
                table: "InvoiceLine",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_InvoiceLineProduct_BundleLineId",
                table: "InvoiceLineExtended",
                column: "BundleLineId",
                principalTable: "InvoiceLineProduct",
                principalColumn: "InvoiceLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockReturnTransaction~",
                table: "InvoiceLineExtended",
                column: "StockReturnTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockTransactionId",
                table: "InvoiceLineExtended",
                column: "StockTransactionId",
                principalTable: "StockTransaction",
                principalColumn: "StockTransactionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineProduct_ProductBaseExtended_ProductId",
                table: "InvoiceLineProduct",
                column: "ProductId",
                principalTable: "ProductBaseExtended",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineProduct_ProductOLProduct_OrderLineId",
                table: "InvoiceLineProduct",
                column: "OrderLineId",
                principalTable: "ProductOLProduct",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineReservationFee_ProductOLReservationFee_OrderLine~",
                table: "InvoiceLineReservationFee",
                column: "OrderLineId",
                principalTable: "ProductOLReservationFee",
                principalColumn: "ProductOLId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineSession_ProductOLSession_OrderLineId",
                table: "InvoiceLineSession",
                column: "OrderLineId",
                principalTable: "ProductOLSession",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineSession_UsageSession_UsageSessionId",
                table: "InvoiceLineSession",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTime_ProductOLTime_OrderLineId",
                table: "InvoiceLineTime",
                column: "OrderLineId",
                principalTable: "ProductOLTime",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTime_ProductTime_ProductTimeId",
                table: "InvoiceLineTime",
                column: "ProductTimeId",
                principalTable: "ProductTime",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineTimeFixed_ProductOLTimeFixed_OrderLineId",
                table: "InvoiceLineTimeFixed",
                column: "OrderLineId",
                principalTable: "ProductOLTimeFixed",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Payment_PaymentId",
                table: "InvoicePayment",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Register_RegisterId",
                table: "InvoicePayment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_Shift_ShiftId",
                table: "InvoicePayment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserMember_UserId",
                table: "InvoicePayment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserOperator_CreatedById",
                table: "InvoicePayment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePayment_UserOperator_ModifiedById",
                table: "InvoicePayment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_License_UserOperator_CreatedById",
                table: "License",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_License_UserOperator_ModifiedById",
                table: "License",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseKey_UserOperator_CreatedById",
                table: "LicenseKey",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LicenseKey_UserOperator_ModifiedById",
                table: "LicenseKey",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_UserOperator_CreatedById",
                table: "Mapping",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapping_UserOperator_ModifiedById",
                table: "Mapping",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryUnit_UserOperator_CreatedById",
                table: "MonetaryUnit",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonetaryUnit_UserOperator_ModifiedById",
                table: "MonetaryUnit",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_UserOperator_CreatedById",
                table: "News",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_UserOperator_ModifiedById",
                table: "News",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_UserOperator_CreatedById",
                table: "Note",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_UserOperator_ModifiedById",
                table: "Note",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_UserOperator_CreatedById",
                table: "Notification",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_UserOperator_ModifiedById",
                table: "Notification",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PointTransaction_PointTransactionId",
                table: "Payment",
                column: "PointTransactionId",
                principalTable: "PointTransaction",
                principalColumn: "PointTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Register_RegisterId",
                table: "Payment",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Shift_ShiftId",
                table: "Payment",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserMember_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserOperator_CreatedById",
                table: "Payment",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_UserOperator_ModifiedById",
                table: "Payment",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_PaymentMethod_PaymentMethodId",
                table: "PaymentIntent",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_UserMember_UserId",
                table: "PaymentIntent",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_User_CreatedById",
                table: "PaymentIntent",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntent_User_ModifiedById",
                table: "PaymentIntent",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentIntentOrder_ProductOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_UserOperator_CreatedById",
                table: "PaymentMethod",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_UserOperator_ModifiedById",
                table: "PaymentMethod",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceipt_Register_RegisterId",
                table: "PaymentReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceipt_Shift_ShiftId",
                table: "PaymentReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentReceipt_UserOperator_CreatedById",
                table: "PaymentReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalFile_UserOperator_CreatedById",
                table: "PersonalFile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalFile_UserOperator_ModifiedById",
                table: "PersonalFile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PluginLibrary_UserOperator_CreatedById",
                table: "PluginLibrary",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PluginLibrary_UserOperator_ModifiedById",
                table: "PluginLibrary",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_Register_RegisterId",
                table: "PointTransaction",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_Shift_ShiftId",
                table: "PointTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserMember_UserId",
                table: "PointTransaction",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserOperator_CreatedById",
                table: "PointTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PointTransaction_UserOperator_ModifiedById",
                table: "PointTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetReservationTime_UserOperator_CreatedById",
                table: "PresetReservationTime",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetReservationTime_UserOperator_ModifiedById",
                table: "PresetReservationTime",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSale_UserOperator_CreatedById",
                table: "PresetTimeSale",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSale_UserOperator_ModifiedById",
                table: "PresetTimeSale",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSaleMoney_UserOperator_CreatedById",
                table: "PresetTimeSaleMoney",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTimeSaleMoney_UserOperator_ModifiedById",
                table: "PresetTimeSaleMoney",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTopUp_UserOperator_CreatedById",
                table: "PresetTopUp",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresetTopUp_UserOperator_ModifiedById",
                table: "PresetTopUp",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductBaseExtended_ProductId",
                table: "Product",
                column: "ProductId",
                principalTable: "ProductBaseExtended",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_ProductGroup_ProductGroupId",
                table: "ProductBase",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "ProductGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_UserOperator_CreatedById",
                table: "ProductBase",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_UserOperator_ModifiedById",
                table: "ProductBase",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserGroup_UserGroupId",
                table: "ProductBundleUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserOperator_CreatedById",
                table: "ProductBundleUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundleUserPrice_UserOperator_ModifiedById",
                table: "ProductBundleUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_UserOperator_CreatedById",
                table: "ProductGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_UserOperator_ModifiedById",
                table: "ProductGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHostHidden_UserOperator_CreatedById",
                table: "ProductHostHidden",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHostHidden_UserOperator_ModifiedById",
                table: "ProductHostHidden",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_UserOperator_CreatedById",
                table: "ProductImage",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_UserOperator_ModifiedById",
                table: "ProductImage",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_ProductOrder_ProductOrderId",
                table: "ProductOL",
                column: "ProductOrderId",
                principalTable: "ProductOrder",
                principalColumn: "ProductOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Register_RegisterId",
                table: "ProductOL",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_ReservationHost_ReservationHostId",
                table: "ProductOL",
                column: "ReservationHostId",
                principalTable: "ReservationHost",
                principalColumn: "ReservationHostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Reservation_ReservationId",
                table: "ProductOL",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_Shift_ShiftId",
                table: "ProductOL",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserMember_UserId",
                table: "ProductOL",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserOperator_CreatedById",
                table: "ProductOL",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOL_UserOperator_ModifiedById",
                table: "ProductOL",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOLExtended_ProductOLProduct_BundleLineId",
                table: "ProductOLExtended",
                column: "BundleLineId",
                principalTable: "ProductOLProduct",
                principalColumn: "ProductOLId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOLSession_UsageSession_UsageSessionId",
                table: "ProductOLSession",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Register_RegisterId",
                table: "ProductOrder",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Shift_ShiftId",
                table: "ProductOrder",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserMember_UserId",
                table: "ProductOrder",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserOperator_CreatedById",
                table: "ProductOrder",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_UserOperator_ModifiedById",
                table: "ProductOrder",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_PromotionCode_PromotionCodeId",
                table: "ProductOrderDiscount",
                column: "PromotionCodeId",
                principalTable: "PromotionCode",
                principalColumn: "PromotionCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_Promotion_PromotionId",
                table: "ProductOrderDiscount",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_Register_RegisterId",
                table: "ProductOrderDiscount",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_Shift_ShiftId",
                table: "ProductOrderDiscount",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_UserMember_UserId",
                table: "ProductOrderDiscount",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderDiscount_UserOperator_CreatedById",
                table: "ProductOrderDiscount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_Tax_TaxId",
                table: "ProductTax",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_UserOperator_CreatedById",
                table: "ProductTax",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTax_UserOperator_ModifiedById",
                table: "ProductTax",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTimeHostDisallowed_UserOperator_CreatedById",
                table: "ProductTimeHostDisallowed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTimeHostDisallowed_UserOperator_ModifiedById",
                table: "ProductTimeHostDisallowed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserGroup_UserGroupId",
                table: "ProductUserDisallowed",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserOperator_CreatedById",
                table: "ProductUserDisallowed",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserDisallowed_UserOperator_ModifiedById",
                table: "ProductUserDisallowed",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserGroup_UserGroupId",
                table: "ProductUserPrice",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "UserGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserOperator_CreatedById",
                table: "ProductUserPrice",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUserPrice_UserOperator_ModifiedById",
                table: "ProductUserPrice",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_UserOperator_CreatedById",
                table: "Promotion",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_UserOperator_ModifiedById",
                table: "Promotion",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionCode_UserOperator_CreatedById",
                table: "PromotionCode",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionCode_UserOperator_ModifiedById",
                table: "PromotionCode",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipient_UserOperator_CreatedById",
                table: "Recipient",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipientChannel_UserOperator_CreatedById",
                table: "RecipientChannel",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Register_RegisterId",
                table: "Refund",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_Shift_ShiftId",
                table: "Refund",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Refund_UserOperator_CreatedById",
                table: "Refund",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundReceipt_Register_RegisterId",
                table: "RefundReceipt",
                column: "RegisterId",
                principalTable: "Register",
                principalColumn: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundReceipt_Shift_ShiftId",
                table: "RefundReceipt",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundReceipt_UserOperator_CreatedById",
                table: "RefundReceipt",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Stock_StockId",
                table: "Register",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "StockId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Register_UserOperator_CreatedById",
                table: "Register",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_UserOperator_ModifiedById",
                table: "Register",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_Shift_ShiftId",
                table: "RegisterTransaction",
                column: "ShiftId",
                principalTable: "Shift",
                principalColumn: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_UserOperator_CreatedById",
                table: "RegisterTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterTransaction_UserOperator_ModifiedById",
                table: "RegisterTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPreset_UserOperator_CreatedById",
                table: "ReportPreset",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPreset_UserOperator_ModifiedById",
                table: "ReportPreset",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_UserMember_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_CreatedById",
                table: "Reservation",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_FinalizedById",
                table: "Reservation",
                column: "FinalizedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_ModifiedById",
                table: "Reservation",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost",
                column: "PreferredUserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_CreatedById",
                table: "ReservationHost",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_FinalizedById",
                table: "ReservationHost",
                column: "FinalizedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationProductOrder_UserOperator_CreatedById",
                table: "ReservationProductOrder",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_UserMember_UserId",
                table: "ReservationUser",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_User_CreatedById",
                table: "ReservationUser",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationUser_User_ModifiedById",
                table: "ReservationUser",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_UserOperator_CreatedById",
                table: "Schedule",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_UserOperator_ModifiedById",
                table: "Schedule",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReportEntry_UserOperator_CreatedById",
                table: "ScheduleReportEntry",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleReportRecipient_UserOperator_UserId",
                table: "ScheduleReportRecipient",
                column: "UserId",
                principalTable: "UserOperator",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfile_UserOperator_CreatedById",
                table: "SecurityProfile",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfile_UserOperator_ModifiedById",
                table: "SecurityProfile",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfilePolicy_UserOperator_CreatedById",
                table: "SecurityProfilePolicy",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfilePolicy_UserOperator_ModifiedById",
                table: "SecurityProfilePolicy",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfileRestriction_UserOperator_CreatedById",
                table: "SecurityProfileRestriction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityProfileRestriction_UserOperator_ModifiedById",
                table: "SecurityProfileRestriction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_UserOperator_CreatedById",
                table: "Setting",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setting_UserOperator_ModifiedById",
                table: "Setting",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_CreatedById",
                table: "Shift",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_EndedById",
                table: "Shift",
                column: "EndedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_ModifiedById",
                table: "Shift",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_UserOperator_OperatorId",
                table: "Shift",
                column: "OperatorId",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCount_UserOperator_CreatedById",
                table: "ShiftCount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCount_UserOperator_ModifiedById",
                table: "ShiftCount",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_UserOperator_CreatedById",
                table: "Stock",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_UserOperator_ModifiedById",
                table: "Stock",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCount_UserOperator_CreatedById",
                table: "StockCount",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCountEntry_UserOperator_CreatedById",
                table: "StockCountEntry",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_UserOperator_CreatedById",
                table: "StockTransaction",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_UserOperator_ModifiedById",
                table: "StockTransaction",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Target_UserOperator_CreatedById",
                table: "Target",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetBillProfile_TargetGroupBillProfile_TargetGroupBillPro~",
                table: "TargetBillProfile",
                column: "TargetGroupBillProfileId",
                principalTable: "TargetGroupBillProfile",
                principalColumn: "TargetGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetGroup_UserOperator_CreatedById",
                table: "TargetGroup",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetGroup_UserOperator_ModifiedById",
                table: "TargetGroup",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBase_UserOperator_CreatedById",
                table: "TaskBase",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskBase_UserOperator_ModifiedById",
                table: "TaskBase",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_UserOperator_CreatedById",
                table: "Tax",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tax_UserOperator_ModifiedById",
                table: "Tax",
                column: "ModifiedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_CreatedById",
                table: "Token",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_ModifiedById",
                table: "Token",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_UserId",
                table: "Token",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_UsageSession_UsageSessionId",
                table: "Usage",
                column: "UsageSessionId",
                principalTable: "UsageSession",
                principalColumn: "UsageSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_UserMember_UserId",
                table: "Usage",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageRate_UsageUserSession_UsageId",
                table: "UsageRate",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageTime_UsageUserSession_UsageId",
                table: "UsageTime",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageTimeFixed_UsageUserSession_UsageId",
                table: "UsageTimeFixed",
                column: "UsageId",
                principalTable: "UsageUserSession",
                principalColumn: "UsageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsageUserSession_UserSession_UserSessionId",
                table: "UsageUserSession",
                column: "UserSessionId",
                principalTable: "UserSession",
                principalColumn: "UserSessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserOperator_CreatedById",
                table: "User",
                column: "CreatedById",
                principalTable: "UserOperator",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserPermissionSet_PermissionSetId",
                table: "User",
                column: "PermissionSetId",
                principalTable: "UserPermissionSet",
                principalColumn: "UserPermissionSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGroup_UserOperator_CreatedById",
                table: "AppGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_AppGroup_UserOperator_ModifiedById",
                table: "AppGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_BillProfile_UserOperator_CreatedById",
                table: "BillProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_BillProfile_UserOperator_ModifiedById",
                table: "BillProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_UserOperator_CreatedById",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_UserOperator_ModifiedById",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientOptions_UserOperator_CreatedById",
                table: "ClientOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientOptions_UserOperator_ModifiedById",
                table: "ClientOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Companion_UserOperator_CreatedById",
                table: "Companion");

            migrationBuilder.DropForeignKey(
                name: "FK_Companion_UserOperator_ModifiedById",
                table: "Companion");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountGroup_UserOperator_CreatedById",
                table: "DiscountGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscountGroup_UserOperator_ModifiedById",
                table: "DiscountGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Host_UserOperator_CreatedById",
                table: "Host");

            migrationBuilder.DropForeignKey(
                name: "FK_Host_UserOperator_ModifiedById",
                table: "Host");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_UserOperator_CreatedById",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_UserOperator_ModifiedById",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Icon_UserOperator_CreatedById",
                table: "Icon");

            migrationBuilder.DropForeignKey(
                name: "FK_Icon_UserOperator_ModifiedById",
                table: "Icon");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_UserOperator_CreatedById",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserOperator_CreatedById",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserOperator_ModifiedById",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserOperator_CreatedById",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserOperator_ModifiedById",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_UserOperator_CreatedById",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_UserOperator_ModifiedById",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserOperator_CreatedById",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserOperator_ModifiedById",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_UserOperator_CreatedById",
                table: "ProductBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_UserOperator_ModifiedById",
                table: "ProductBase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_UserOperator_CreatedById",
                table: "ProductGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_UserOperator_ModifiedById",
                table: "ProductGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserOperator_CreatedById",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserOperator_ModifiedById",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserOperator_CreatedById",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserOperator_ModifiedById",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_UserOperator_CreatedById",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_UserOperator_ModifiedById",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityProfile_UserOperator_CreatedById",
                table: "SecurityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityProfile_UserOperator_ModifiedById",
                table: "SecurityProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_CreatedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_EndedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_ModifiedById",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_UserOperator_OperatorId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_UserOperator_CreatedById",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_UserOperator_ModifiedById",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_UserOperator_CreatedById",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_UserOperator_ModifiedById",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserOperator_CreatedById",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_UserOperator_CreatedById",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_UserOperator_ModifiedById",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissionSet_UserOperator_CreatedById",
                table: "UserPermissionSet");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissionSet_UserOperator_ModifiedById",
                table: "UserPermissionSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBaseExtended_ProductBase_ProductId",
                table: "ProductBaseExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_ProductBase_ProductId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_ProductBase_SourceProductId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_Branch_BranchId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Branch_BranchId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Branch_BranchId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Branch_BranchId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Branch_BranchId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Branch_BranchId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Branch_BranchId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_Branch_BranchId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Branch_BranchId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_HostGroup_AppGroup_AppGroupId",
                table: "HostGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_AppGroup_AppGroupId",
                table: "UserGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserMember_UserId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_UserMember_UserId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_UserMember_UserId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_UserMember_UserId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_UserMember_UserId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_UserMember_UserId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_UserMember_PreferredUserId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_UserMember_UserId",
                table: "Usage");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Host_HostId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_Host_HostId",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_CreatedById",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_FinalizedById",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_ModifiedById",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_User_CreatedById",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_User_FinalizedById",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationHost_User_ModifiedById",
                table: "ReservationHost");

            migrationBuilder.DropForeignKey(
                name: "FK_Register_Companion_CompanionId",
                table: "Register");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Register_RegisterId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Register_RegisterId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_Register_RegisterId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Register_RegisterId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Register_RegisterId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Register_RegisterId",
                table: "Shift");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Shift_ShiftId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Shift_ShiftId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Shift_ShiftId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_PointTransaction_Shift_ShiftId",
                table: "PointTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Shift_ShiftId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Shift_ShiftId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_ProductOrder_ProductOrderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_ProductOrder_ProductOrderId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Stock_StockId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Stock_TransferStockId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Stock_StockId",
                table: "StockTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInbound_Inventory_InventoryId",
                table: "InventoryInbound");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Inventory_InventoryId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockReturnTransaction~",
                table: "InvoiceLineExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineExtended_StockTransaction_StockTransactionId",
                table: "InvoiceLineExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInbound_InventoryTransfer_InventoryTransferId",
                table: "InventoryInbound");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_PointTransaction_PointsTransactionId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_ReservationHost_ReservationHostId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_ReservationHost_ReservationHostId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Reservation_ReservationId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_Reservation_ReservationId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineExtended_InvoiceLineProduct_BundleLineId",
                table: "InvoiceLineExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOLProduct_ProductBaseExtended_ProductId",
                table: "ProductOLProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOLExtended_ProductOLProduct_BundleLineId",
                table: "ProductOLExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_UsageSession_UsageSessionId",
                table: "Usage");

            migrationBuilder.DropTable(
                name: "AgeRestrictionLogin");

            migrationBuilder.DropTable(
                name: "AgeRestrictionProduct");

            migrationBuilder.DropTable(
                name: "AppExeBranch");

            migrationBuilder.DropTable(
                name: "AppExeCdImage");

            migrationBuilder.DropTable(
                name: "AppExeDeployment");

            migrationBuilder.DropTable(
                name: "AppExeImage");

            migrationBuilder.DropTable(
                name: "AppExeLicense");

            migrationBuilder.DropTable(
                name: "AppExeMaxUser");

            migrationBuilder.DropTable(
                name: "AppExePersonalFile");

            migrationBuilder.DropTable(
                name: "AppExeTask");

            migrationBuilder.DropTable(
                name: "AppGroupApp");

            migrationBuilder.DropTable(
                name: "AppImage");

            migrationBuilder.DropTable(
                name: "AppLink");

            migrationBuilder.DropTable(
                name: "AppRating");

            migrationBuilder.DropTable(
                name: "AppStat");

            migrationBuilder.DropTable(
                name: "AssetTransaction");

            migrationBuilder.DropTable(
                name: "AssistanceRequest");

            migrationBuilder.DropTable(
                name: "BillRatePeriodDayTime");

            migrationBuilder.DropTable(
                name: "BillRateStep");

            migrationBuilder.DropTable(
                name: "BundleProductUserPrice");

            migrationBuilder.DropTable(
                name: "ClientTask");

            migrationBuilder.DropTable(
                name: "DeploymentDeployment");

            migrationBuilder.DropTable(
                name: "DeviceHdmi");

            migrationBuilder.DropTable(
                name: "DeviceHost");

            migrationBuilder.DropTable(
                name: "DiscountBranch");

            migrationBuilder.DropTable(
                name: "DiscountGroupDiscount");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDayTime");

            migrationBuilder.DropTable(
                name: "FeedBranch");

            migrationBuilder.DropTable(
                name: "FileImage");

            migrationBuilder.DropTable(
                name: "HostEndpoint");

            migrationBuilder.DropTable(
                name: "HostGroupUserBillProfile");

            migrationBuilder.DropTable(
                name: "HostGroupWaitingLineEntry");

            migrationBuilder.DropTable(
                name: "HostLayoutGroupImage");

            migrationBuilder.DropTable(
                name: "HostLayoutGroupLayout");

            migrationBuilder.DropTable(
                name: "IntentOrder");

            migrationBuilder.DropTable(
                name: "IntentOrderDeposit");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentEntry");

            migrationBuilder.DropTable(
                name: "InventoryDocument");

            migrationBuilder.DropTable(
                name: "InventoryInboundEntry");

            migrationBuilder.DropTable(
                name: "InvoiceFiscalReceipt");

            migrationBuilder.DropTable(
                name: "InvoiceLineReservationFee");

            migrationBuilder.DropTable(
                name: "InvoiceLineSession");

            migrationBuilder.DropTable(
                name: "LicenseKey");

            migrationBuilder.DropTable(
                name: "LogException");

            migrationBuilder.DropTable(
                name: "Mapping");

            migrationBuilder.DropTable(
                name: "MonetaryUnit");

            migrationBuilder.DropTable(
                name: "NewsBranch");

            migrationBuilder.DropTable(
                name: "NotificationTimedRemaining");

            migrationBuilder.DropTable(
                name: "NotificationTimedReservation");

            migrationBuilder.DropTable(
                name: "PaymentIntentDeposit");

            migrationBuilder.DropTable(
                name: "PaymentReceipt");

            migrationBuilder.DropTable(
                name: "PluginLibrary");

            migrationBuilder.DropTable(
                name: "PresetReservationTime");

            migrationBuilder.DropTable(
                name: "PresetTimeSale");

            migrationBuilder.DropTable(
                name: "PresetTimeSaleMoney");

            migrationBuilder.DropTable(
                name: "PresetTopUp");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductBranch");

            migrationBuilder.DropTable(
                name: "ProductBundleUserPrice");

            migrationBuilder.DropTable(
                name: "ProductHostHidden");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductOrderDiscount");

            migrationBuilder.DropTable(
                name: "ProductPeriodDayTime");

            migrationBuilder.DropTable(
                name: "ProductTax");

            migrationBuilder.DropTable(
                name: "ProductTimeHostDisallowed");

            migrationBuilder.DropTable(
                name: "ProductTimePeriodDayTime");

            migrationBuilder.DropTable(
                name: "ProductUserDisallowed");

            migrationBuilder.DropTable(
                name: "ProductUserPrice");

            migrationBuilder.DropTable(
                name: "PromotionBranch");

            migrationBuilder.DropTable(
                name: "PromotionDiscount");

            migrationBuilder.DropTable(
                name: "PromotionDiscountGroup");

            migrationBuilder.DropTable(
                name: "PromotionLimit");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDayTime");

            migrationBuilder.DropTable(
                name: "RecipientChannel");

            migrationBuilder.DropTable(
                name: "RefundDepositPayment");

            migrationBuilder.DropTable(
                name: "RefundInvoicePayment");

            migrationBuilder.DropTable(
                name: "RefundPayment");

            migrationBuilder.DropTable(
                name: "RefundReceipt");

            migrationBuilder.DropTable(
                name: "RegisterTransaction");

            migrationBuilder.DropTable(
                name: "ReservationProductOrder");

            migrationBuilder.DropTable(
                name: "ReservationUser");

            migrationBuilder.DropTable(
                name: "ScheduleReportEntry");

            migrationBuilder.DropTable(
                name: "ScheduleReportRecipient");

            migrationBuilder.DropTable(
                name: "SecurityProfilePolicy");

            migrationBuilder.DropTable(
                name: "SecurityProfileRestriction");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ShiftCount");

            migrationBuilder.DropTable(
                name: "StockCountAdjustment");

            migrationBuilder.DropTable(
                name: "StockCountEntry");

            migrationBuilder.DropTable(
                name: "StockCountInbound");

            migrationBuilder.DropTable(
                name: "TargetBillProfile");

            migrationBuilder.DropTable(
                name: "TargetPaymentMethod");

            migrationBuilder.DropTable(
                name: "TargetProduct");

            migrationBuilder.DropTable(
                name: "TargetProductGroup");

            migrationBuilder.DropTable(
                name: "TargetProductTime");

            migrationBuilder.DropTable(
                name: "TaskJunction");

            migrationBuilder.DropTable(
                name: "TaskNotification");

            migrationBuilder.DropTable(
                name: "TaskProcess");

            migrationBuilder.DropTable(
                name: "TaskScript");

            migrationBuilder.DropTable(
                name: "UsageRate");

            migrationBuilder.DropTable(
                name: "UsageTime");

            migrationBuilder.DropTable(
                name: "UsageTimeFixed");

            migrationBuilder.DropTable(
                name: "UserAgreementState");

            migrationBuilder.DropTable(
                name: "UserApiKey");

            migrationBuilder.DropTable(
                name: "UserAttribute");

            migrationBuilder.DropTable(
                name: "UserChannel");

            migrationBuilder.DropTable(
                name: "UserCredential");

            migrationBuilder.DropTable(
                name: "UserCreditLimit");

            migrationBuilder.DropTable(
                name: "UserGroupHostDisallowed");

            migrationBuilder.DropTable(
                name: "UserGuest");

            migrationBuilder.DropTable(
                name: "UserNote");

            migrationBuilder.DropTable(
                name: "UserOperatorBranch");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "UserPermissionSetPermission");

            migrationBuilder.DropTable(
                name: "UserPicture");

            migrationBuilder.DropTable(
                name: "UserSessionChange");

            migrationBuilder.DropTable(
                name: "Variable");

            migrationBuilder.DropTable(
                name: "VerificationEmail");

            migrationBuilder.DropTable(
                name: "VerificationMobilePhone");

            migrationBuilder.DropTable(
                name: "VoidDepositPayment");

            migrationBuilder.DropTable(
                name: "VoidInvoice");

            migrationBuilder.DropTable(
                name: "AgeRestriction");

            migrationBuilder.DropTable(
                name: "PersonalFile");

            migrationBuilder.DropTable(
                name: "AppExe");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssistanceRequestType");

            migrationBuilder.DropTable(
                name: "BillRatePeriodDay");

            migrationBuilder.DropTable(
                name: "BundleProduct");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "DiscountPeriodDay");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "HostGroupWaitingLine");

            migrationBuilder.DropTable(
                name: "HostLayoutGroup");

            migrationBuilder.DropTable(
                name: "PaymentIntentOrder");

            migrationBuilder.DropTable(
                name: "InventoryAdjustmentReason");

            migrationBuilder.DropTable(
                name: "FileDocument");

            migrationBuilder.DropTable(
                name: "InventoryTransferEntry");

            migrationBuilder.DropTable(
                name: "ProductOLReservationFee");

            migrationBuilder.DropTable(
                name: "ProductOLSession");

            migrationBuilder.DropTable(
                name: "HostComputer");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "NotificationTimed");

            migrationBuilder.DropTable(
                name: "PromotionCode");

            migrationBuilder.DropTable(
                name: "ProductPeriodDay");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "ProductTimePeriodDay");

            migrationBuilder.DropTable(
                name: "PromotionPeriodDay");

            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "ReportPreset");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "ScheduleReport");

            migrationBuilder.DropTable(
                name: "InventoryAdjustment");

            migrationBuilder.DropTable(
                name: "StockCount");

            migrationBuilder.DropTable(
                name: "TargetGroupBillProfile");

            migrationBuilder.DropTable(
                name: "TargetGroupPaymentMethod");

            migrationBuilder.DropTable(
                name: "TargetGroupProduct");

            migrationBuilder.DropTable(
                name: "TargetGroupProductGroup");

            migrationBuilder.DropTable(
                name: "TargetGroupProductTime");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.DropTable(
                name: "TaskBase");

            migrationBuilder.DropTable(
                name: "InvoiceLineTime");

            migrationBuilder.DropTable(
                name: "InvoiceLineTimeFixed");

            migrationBuilder.DropTable(
                name: "UsageUserSession");

            migrationBuilder.DropTable(
                name: "UserAgreement");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "DepositPayment");

            migrationBuilder.DropTable(
                name: "Void");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Deployment");

            migrationBuilder.DropTable(
                name: "AssetType");

            migrationBuilder.DropTable(
                name: "BillRate");

            migrationBuilder.DropTable(
                name: "ProductBundle");

            migrationBuilder.DropTable(
                name: "DiscountPeriod");

            migrationBuilder.DropTable(
                name: "PaymentIntent");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "InventoryEntry");

            migrationBuilder.DropTable(
                name: "InventoryTransferReason");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ProductPeriod");

            migrationBuilder.DropTable(
                name: "ProductTimePeriod");

            migrationBuilder.DropTable(
                name: "PromotionPeriod");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "TargetGroup");

            migrationBuilder.DropTable(
                name: "ProductOLTime");

            migrationBuilder.DropTable(
                name: "ProductOLTimeFixed");

            migrationBuilder.DropTable(
                name: "UserSession");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "FiscalReceipt");

            migrationBuilder.DropTable(
                name: "AppCategory");

            migrationBuilder.DropTable(
                name: "AppEnterprise");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "ProductTime");

            migrationBuilder.DropTable(
                name: "DepositTransaction");

            migrationBuilder.DropTable(
                name: "UserOperator");

            migrationBuilder.DropTable(
                name: "ProductBase");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "AppGroup");

            migrationBuilder.DropTable(
                name: "UserMember");

            migrationBuilder.DropTable(
                name: "Host");

            migrationBuilder.DropTable(
                name: "HostGroup");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropTable(
                name: "ClientOptions");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "BillProfile");

            migrationBuilder.DropTable(
                name: "DiscountGroup");

            migrationBuilder.DropTable(
                name: "SecurityProfile");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserPermissionSet");

            migrationBuilder.DropTable(
                name: "Companion");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "StockTransaction");

            migrationBuilder.DropTable(
                name: "InventoryTransfer");

            migrationBuilder.DropTable(
                name: "InventoryInbound");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "PointTransaction");

            migrationBuilder.DropTable(
                name: "ReservationHost");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "InvoiceLineProduct");

            migrationBuilder.DropTable(
                name: "InvoiceLineExtended");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "ProductBaseExtended");

            migrationBuilder.DropTable(
                name: "ProductOLProduct");

            migrationBuilder.DropTable(
                name: "ProductOLExtended");

            migrationBuilder.DropTable(
                name: "ProductOL");

            migrationBuilder.DropTable(
                name: "UsageSession");

            migrationBuilder.DropTable(
                name: "Usage");
        }
    }
}
