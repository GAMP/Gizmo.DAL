using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gizmo.DAL.EFCore.Migrations.Npgsql
{
    public partial class Initial : Migration
    {
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
                        name: "FK_AppGroupApp_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppGroupApp_AppGroup_AppGroupId",
                        column: x => x.AppGroupId,
                        principalTable: "AppGroup",
                        principalColumn: "AppGroupId",
                        onDelete: ReferentialAction.Cascade);
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
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStat", x => x.AppStatId);
                    table.ForeignKey(
                        name: "FK_AppStat_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStat_AppExe_AppExeId",
                        column: x => x.AppExeId,
                        principalTable: "AppExe",
                        principalColumn: "AppExeId",
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
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
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
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RefundStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    FiscalReceiptId = table.Column<int>(type: "integer", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPayment", x => x.DepositPaymentId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
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
                    HosGroupId = table.Column<int>(type: "integer", nullable: false),
                    TimeOutOptions = table.Column<int>(type: "integer", nullable: false),
                    EnablePriorities = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostGroupWaitingLine", x => x.HosGroupId);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLine_HostGroup_HosGroupId",
                        column: x => x.HosGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostGroupWaitingLineEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
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
                    table.PrimaryKey("PK_HostGroupWaitingLineEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroup_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroup",
                        principalColumn: "HostGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HostGroupWaitingLineEntry_HostGroupWaitingLine_HostGroupId",
                        column: x => x.HostGroupId,
                        principalTable: "HostGroupWaitingLine",
                        principalColumn: "HosGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostLayoutGroup",
                columns: table => new
                {
                    HostLayoutGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroup", x => x.HostLayoutGroupId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostLayoutGroupLayout", x => x.HostLayoutGroupLayoutId);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostLayoutGroupLayout_HostLayoutGroup_HostLayoutGroupId",
                        column: x => x.HostLayoutGroupId,
                        principalTable: "HostLayoutGroup",
                        principalColumn: "HostLayoutGroupId",
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
                    OutstandngPoints = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true),
                    IsVoided = table.Column<bool>(type: "boolean", nullable: false),
                    SaleFiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    ReturnFiscalReceiptStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
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
                    IsDepleted = table.Column<bool>(type: "boolean", nullable: false)
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.InvoicePaymentId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntent", x => x.PaymentIntentId);
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
                    ProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    InvoicePaymentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentIntentOrder", x => x.PaymentIntentId);
                    table.ForeignKey(
                        name: "FK_PaymentIntentOrder_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId");
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
                    ProductId = table.Column<int>(type: "integer", nullable: false)
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
                    PreferedPaymentMethodId = table.Column<int>(type: "integer", nullable: true),
                    IsDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    UserNote = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.ProductOrderId);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId");
                    table.ForeignKey(
                        name: "FK_ProductOrder_PaymentMethod_PreferedPaymentMethodId",
                        column: x => x.PreferedPaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId");
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refund", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_Refund_DepositTransaction_DepositTransactionId",
                        column: x => x.DepositTransactionId,
                        principalTable: "DepositTransaction",
                        principalColumn: "DepositTransactionId");
                    table.ForeignKey(
                        name: "FK_Refund_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Refund_PaymentMethod_RefundMethodId",
                        column: x => x.RefundMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_RefundInvoicePayment_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_InvoicePayment_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayment",
                        principalColumn: "InvoicePaymentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefundInvoicePayment_Refund_RefundId",
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.RegisterId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                });

            migrationBuilder.CreateTable(
                name: "ReservationHost",
                columns: table => new
                {
                    ReservationHostId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    HostId = table.Column<int>(type: "integer", nullable: false),
                    PreferedUserId = table.Column<int>(type: "integer", nullable: true),
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
                        name: "FK_ReservationHost_Reservation_ReservationId",
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
                name: "SecurityProfile",
                columns: table => new
                {
                    SecurityProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DisabledDrives = table.Column<int>(type: "integer", nullable: false),
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
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
                    BillProfileStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    NegativeSeconds = table.Column<double>(type: "double precision", nullable: false),
                    StartFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    MinimumFee = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    RatesTotal = table.Column<decimal>(type: "numeric(19,4)", precision: 19, scale: 4, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageSession", x => x.UsageSessionId);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
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
                        name: "FK_UserAttribute_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
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
                    table.ForeignKey(
                        name: "FK_UserCredential_UserOperator_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserOperator",
                        principalColumn: "UserId");
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
                        name: "FK_UserPermission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: true),
                    RegisterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Void", x => x.VoidId);
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
                    table.ForeignKey(
                        name: "FK_UserAgreementState_UserAgreement_UserAgreementId",
                        column: x => x.UserAgreementId,
                        principalTable: "UserAgreement",
                        principalColumn: "UserAgreementId",
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
                        name: "FK_UserMember_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMember_UserGroup_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroup",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Restrict);
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
                    IsJoined = table.Column<bool>(type: "boolean", nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    ReservedHostId = table.Column<int>(type: "integer", nullable: true),
                    ReservedSlot = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.UserSessionId);
                    table.ForeignKey(
                        name: "FK_UserSession_Host_HostId",
                        column: x => x.HostId,
                        principalTable: "Host",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSession_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserSession_UserMember_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMember",
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
                        name: "FK_UserSessionChange_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "UserId");
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
                });

            migrationBuilder.InsertData(
                table: "BillProfile",
                columns: new[] { "BillProfileId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Member Prices" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Guests Prices" }
                });

            migrationBuilder.InsertData(
                table: "HostLayoutGroup",
                columns: new[] { "HostLayoutGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Name" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, "Default" });

            migrationBuilder.InsertData(
                table: "MonetaryUnit",
                columns: new[] { "MonetaryUnitId", "CreatedById", "CreatedTime", "DisplayOrder", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, false, null, null, "1 Cent", 0.01m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "5 Cent", 0.05m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, null, null, "10 Cent", 0.10m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, null, "25 Cent", 0.25m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, false, null, null, "1 Dollar", 1.00m },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, false, null, null, "2 Dollar", 2.00m },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, false, null, null, "5 Dollar", 5.00m },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, false, null, null, "10 Dollar", 10.00m },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, false, null, null, "20 Dollar", 20.00m },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, false, null, null, "50 Dollar", 50.00m },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, false, null, null, "100 Dollar", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "PaymentMethodId", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "IsClient", "IsDeleted", "IsEnabled", "IsManager", "IsPortal", "ModifiedById", "ModifiedTime", "Name", "Options", "PaymentProvider", "Surcharge" },
                values: new object[,]
                {
                    { -4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, true, false, true, true, false, null, null, "Points", 0, null, 0m },
                    { -3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, true, false, true, true, false, null, null, "Deposit", 0, null, 0m },
                    { -2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, true, false, true, true, false, null, null, "Credit Card", 0, null, 0m },
                    { -1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, true, false, true, true, false, null, null, "Cash", 0, null, 0m }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSale",
                columns: new[] { "PresetTimeSaleId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 15 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 30 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 60 }
                });

            migrationBuilder.InsertData(
                table: "PresetTimeSaleMoney",
                columns: new[] { "PresetTimeSaleMoneyId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 2m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 5m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 10m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 20m }
                });

            migrationBuilder.InsertData(
                table: "ProductGroup",
                columns: new[] { "ProductGroupId", "CreatedById", "CreatedTime", "DisplayOrder", "Guid", "ModifiedById", "ModifiedTime", "Name", "ParentId", "SortOption" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70271"), null, null, "Time Offers", null, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70272"), null, null, "Food", null, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70273"), null, null, "Drinks", null, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("e798a7fb-448b-4825-8b32-c5ea6db70274"), null, null, "Sweets", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tax",
                columns: new[] { "TaxId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Name", "UseOrder", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "24%", 0, 23m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "16%", 1, 16m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "None", 2, 0m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "BirthDate", "City", "Country", "CreatedById", "CreatedTime", "FirstName", "Guid", "Identification", "IsDeleted", "IsDisabled", "LastName", "MobilePhone", "ModifiedById", "ModifiedTime", "Phone", "PostCode", "Sex", "SmartCardUID" },
                values: new object[,]
                {
                    { 2, null, null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("38753737-24f1-40d7-8ac4-ba61660d666a"), null, false, false, null, null, null, null, null, null, 0, null },
                    { 1, null, null, null, null, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("691ea8b4-d794-4096-84ae-bbdb7bcc0b02"), null, false, false, null, null, null, null, null, null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "BillRate",
                columns: new[] { "BillRateId", "BillProfileId", "ChargeAfter", "ChargeEvery", "IsDefault", "MinimumFee", "Options", "Rate", "StartFee" },
                values: new object[,]
                {
                    { 1, 1, 1, 5, true, 2m, 0, 2m, 1m },
                    { 2, 2, 1, 5, true, 2m, 0, 2m, 1m }
                });

            migrationBuilder.InsertData(
                table: "ProductBase",
                columns: new[] { "ProductId", "Barcode", "Cost", "CreatedById", "CreatedTime", "Description", "DisplayOrder", "Guid", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "OrderOptions", "Points", "PointsPrice", "Price", "ProductGroupId", "PurchaseOptions", "StockAlert", "StockOptions", "StockProductAmount", "StockProductId" },
                values: new object[,]
                {
                    { 1, null, 0.90m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba1"), false, null, null, "Mars Bar", 0, 10, null, 1.10m, 4, 0, 0m, 1, 0m, null },
                    { 2, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba2"), false, null, null, "Snickers Bar", 0, 15, null, 2.0m, 4, 0, 0m, 1, 0m, null },
                    { 3, null, 2.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba3"), false, null, null, "Pizza (Small)", 0, null, null, 6.0m, 2, 0, 0m, 0, 0m, null },
                    { 4, null, 1.20m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba4"), false, null, null, "Coca Cola (Can)", 0, null, null, 2.0m, 3, 0, 0m, 0, 0m, null },
                    { 5, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba5"), false, null, null, "Pizza and Cola", 0, 200, null, 3.40m, 2, 0, 0m, 1, 0m, null },
                    { 6, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba6"), false, null, null, "Six Hours (6)", 0, null, null, 12m, 1, 0, 0m, 0, 0m, null },
                    { 7, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new Guid("39a65689-65ae-49b4-80b9-ea0afb9daba7"), false, null, null, "Six Hours (6 Weekends)", 0, null, null, 16m, 1, 0, 0m, 0, 0m, null }
                });

            migrationBuilder.InsertData(
                table: "UserCredential",
                columns: new[] { "UserId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Password", "Salt" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new byte[] { 227, 190, 117, 189, 14, 131, 27, 251, 244, 196, 14, 55, 126, 183, 143, 152, 146, 122, 121, 195, 5, 57, 241, 24, 184, 41, 122, 231, 166, 174, 210, 155, 233, 8, 83, 128, 145, 208, 28, 139, 149, 46, 168, 246, 21, 38, 126, 197, 29, 147, 234, 81, 253, 218, 217, 136, 216, 237, 206, 196, 113, 231, 152, 52 }, new byte[] { 213, 217, 89, 164, 125, 194, 157, 170, 86, 35, 202, 5, 236, 165, 229, 151, 191, 209, 130, 41, 234, 120, 64, 104, 216, 200, 194, 9, 221, 163, 100, 236, 125, 143, 49, 114, 227, 161, 166, 20, 120, 7, 250, 81, 128, 236, 241, 116, 231, 235, 216, 208, 131, 155, 104, 218, 249, 75, 34, 190, 62, 160, 147, 82, 158, 78, 172, 74, 131, 17, 26, 236, 95, 7, 190, 245, 165, 235, 103, 17, 172, 55, 141, 182, 51, 96, 212, 209, 67, 164, 111, 234, 83, 101, 64, 224, 84, 84, 54, 4 } });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "UserGroupId", "AppGroupId", "BillProfileId", "BillingOptions", "CreatedById", "CreatedTime", "CreditLimit", "CreditLimitOptions", "Description", "IsAgeRatingEnabled", "IsDefault", "IsNegativeBalanceAllowed", "IsWaitingLinePriorityEnabled", "ModifiedById", "ModifiedTime", "Name", "Options", "Overrides", "Points", "PointsAwardOptions", "PointsMoneyRatio", "PointsTimeRatio", "RequiredUserInfo", "SecurityProfileId", "WaitingLinePriority" },
                values: new object[,]
                {
                    { 1, null, 1, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, true, false, false, null, null, "Members", 0, 0, null, 0, 0m, 0, 0, null, 0 },
                    { 2, null, 2, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 0, null, false, false, false, false, null, null, "Guests", 8, 0, null, 0, 0m, 0, 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserOperator",
                columns: new[] { "UserId", "Email", "ShiftOptions", "Username" },
                values: new object[] { 1, null, 0, "Admin" });

            migrationBuilder.InsertData(
                table: "UserPermission",
                columns: new[] { "UserPermissionId", "CreatedById", "CreatedTime", "ModifiedById", "ModifiedTime", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "*" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "CustomPrice" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "NonDefaultVat" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "PayLater" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidInvoices" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidUsedTimeInvoices" },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidClosedShiftInvoices" },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidOtherOperatorInvoices" },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidPastDaysInvoices" },
                    { 10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Deposit" },
                    { 11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "Withdraw" },
                    { 12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "VoidDeposits" },
                    { 13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ManualOpenCashDrawer" },
                    { 14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ModifyBillingOptions" },
                    { 15, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "AllowTimeCredit" },
                    { 16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewInvoices" },
                    { 17, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPaidInvoices" },
                    { 18, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysInvoices" },
                    { 19, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewDeposits" },
                    { 20, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysDeposits" },
                    { 21, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewRegisterTransactions" },
                    { 22, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "ViewPastDaysRegisterTransactions" },
                    { 23, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sale", 1, "DeleteTimePurchases" },
                    { 24, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Shift", 1, "ViewExpected" },
                    { 25, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "*" },
                    { 26, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "Manage" },
                    { 27, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewStockTransactions" },
                    { 28, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Stock", 1, "ViewPastDaysStockTransactions" },
                    { 29, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "*" },
                    { 30, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Tasks" },
                    { 31, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Processes" },
                    { 32, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Files" },
                    { 33, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Maintenance" },
                    { 34, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "Security" },
                    { 35, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "LockState" },
                    { 36, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Management", 1, "ModuleRestart" },
                    { 37, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Deployment", 1, "*" },
                    { 38, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Monitoring", 1, "*" },
                    { 39, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Reports", 1, "*" },
                    { 40, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Settings", 1, "*" },
                    { 41, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Apps", 1, "*" },
                    { 42, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "News", 1, "*" },
                    { 43, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserPasswordReset" },
                    { 44, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserEnable" },
                    { 45, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserDisable" },
                    { 46, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "UserManualLogin" },
                    { 47, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Add" },
                    { 48, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Delete" },
                    { 49, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserName" },
                    { 50, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "ChangeUserGroup" },
                    { 51, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "Edit" },
                    { 52, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "User", 1, "AccessStats" },
                    { 53, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "*" },
                    { 54, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Log", 1, "Clear" },
                    { 55, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "*" },
                    { 56, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WaitingLines", 1, "Manage" },
                    { 57, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayIn" },
                    { 58, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "RegisterTransactions", 1, "RegisterTransactionsPayOut" },
                    { 59, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "WebApi", 1, "*" }
                });

            migrationBuilder.InsertData(
                table: "HostGroup",
                columns: new[] { "HostGroupId", "AppGroupId", "CreatedById", "CreatedTime", "DefaultGuestGroupId", "ModifiedById", "ModifiedTime", "Name", "Options", "SecurityProfileId", "SkinName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Computers", 0, null, null },
                    { 2, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Endpoints", 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "ProductBaseExtended",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                });

            migrationBuilder.InsertData(
                table: "ProductPeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 7, null, 0, null });

            migrationBuilder.InsertData(
                table: "ProductTax",
                columns: new[] { "ProductTaxId", "CreatedById", "CreatedTime", "IsEnabled", "ModifiedById", "ModifiedTime", "ProductId", "TaxId", "UseOrder" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 1, 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 2, 1, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 3, 1, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 4, 1, 0 },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 5, 1, 0 },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 6, 1, 0 },
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, 7, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductTime",
                columns: new[] { "ProductId", "AppGroupId", "ExpirationOptions", "ExpireAfterType", "ExpireAtDayTimeMinute", "ExpireFromOptions", "ExpiresAfter", "Minutes", "UsageOptions", "UseOrder", "WeekDayMaxMinutes", "WeekEndMaxMinutes" },
                values: new object[,]
                {
                    { 6, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null },
                    { 7, null, 0, 0, 0, 0, 0, 360, 0, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserMember",
                columns: new[] { "UserId", "BillingOptions", "DisabledDate", "Email", "EnableDate", "IsNegativeBalanceAllowed", "IsPersonalInfoRequested", "UserGroupId", "Username" },
                values: new object[] { 2, null, null, null, null, null, false, 1, "User" });

            migrationBuilder.InsertData(
                table: "Host",
                columns: new[] { "HostId", "CreatedById", "CreatedTime", "Guid", "HostGroupId", "IconId", "IsDeleted", "ModifiedById", "ModifiedTime", "Name", "Number", "State" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803871"), 2, null, false, null, null, "XBOX-ONE-1", 1, 0 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803872"), 2, null, false, null, null, "XBOX-ONE-2", 2, 0 },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803873"), 2, null, false, null, null, "PS4-1", 3, 0 },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cd41aa25-ac1f-4da9-8c8e-075032803874"), 2, null, false, null, null, "WII-1", 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                column: "ProductId",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "ProductBundle",
                columns: new[] { "ProductId", "BundleStockOptions" },
                values: new object[] { 5, 0 });

            migrationBuilder.InsertData(
                table: "ProductPeriodDay",
                columns: new[] { "ProductPeriodDayId", "Day", "ProductPeriodId" },
                values: new object[,]
                {
                    { 7, 6, 7 },
                    { 8, 0, 7 }
                });

            migrationBuilder.InsertData(
                table: "ProductTimePeriod",
                columns: new[] { "ProductId", "EndDate", "Options", "StartDate" },
                values: new object[] { 6, null, 0, null });

            migrationBuilder.InsertData(
                table: "BundleProduct",
                columns: new[] { "BundleProductId", "CreatedById", "CreatedTime", "DisplayOrder", "ModifiedById", "ModifiedTime", "Options", "Price", "ProductBundleId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 1m, 5, 3, 1m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, 0, 2m, 5, 4, 1m }
                });

            migrationBuilder.InsertData(
                table: "HostEndpoint",
                columns: new[] { "HostId", "MaximumUsers" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 }
                });

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
                name: "IX_App_ModifiedById",
                table: "App",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_App_PublisherId",
                table: "App",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_App",
                table: "App",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_CreatedById",
                table: "AppCategory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_ModifiedById",
                table: "AppCategory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppCategory_ParentId",
                table: "AppCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_AppCategory",
                table: "AppCategory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_CreatedById",
                table: "AppEnterprise",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppEnterprise_ModifiedById",
                table: "AppEnterprise",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_AppEnterprise",
                table: "AppEnterprise",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_AppEnterprise",
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
                name: "IX_AppExeCdImage_AppExeId",
                table: "AppExeCdImage",
                column: "AppExeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_CreatedById",
                table: "AppExeCdImage",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeCdImage_ModifiedById",
                table: "AppExeCdImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_AppExeCdImage",
                table: "AppExeCdImage",
                column: "Guid",
                unique: true);

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
                name: "IX_AppExeMaxUser_CreatedById",
                table: "AppExeMaxUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppExeMaxUser_ModifiedById",
                table: "AppExeMaxUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_AppExeAppExeMode",
                table: "AppExeMaxUser",
                columns: new[] { "AppExeId", "Mode" },
                unique: true);

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
                name: "IX_AppGroup_ModifiedById",
                table: "AppGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_AppGroup",
                table: "AppGroup",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_AppGroup",
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
                name: "IX_AppLink_ModifiedById",
                table: "AppLink",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_AppLink",
                table: "AppLink",
                column: "Guid",
                unique: true);

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
                name: "IX_Asset_CreatedById",
                table: "Asset",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ModifiedById",
                table: "Asset",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Barcode_Asset",
                table: "Asset",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_SmartCardUID_Asset",
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
                name: "UQ_Name_AssetType",
                table: "AssetType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CreatedById",
                table: "Attribute",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ModifiedById",
                table: "Attribute",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Attribute",
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
                name: "UQ_Name_BillProfile",
                table: "BillProfile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillRate_BillProfileId",
                table: "BillRate",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_BillRatePeriodDay",
                table: "BillRatePeriodDay",
                columns: new[] { "BillRateId", "Day" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillRatePeriodDayTime_PeriodDayId",
                table: "BillRatePeriodDayTime",
                column: "PeriodDayId");

            migrationBuilder.CreateIndex(
                name: "UQ_BillRateMinute",
                table: "BillRateStep",
                columns: new[] { "BillRateId", "Minute" },
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
                name: "UQ_BundleProductUserGroup",
                table: "BundleProductUserPrice",
                columns: new[] { "BundleProductId", "UserGroupId" },
                unique: true);

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
                name: "IX_Deployment_CreatedById",
                table: "Deployment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deployment_ModifiedById",
                table: "Deployment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_Deployment",
                table: "Deployment",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Deployment",
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
                name: "IX_Device_CreatedById",
                table: "Device",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Device_ModifiedById",
                table: "Device",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Device_DeviceHdmi",
                table: "Device",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHdmi_DeviceId",
                table: "DeviceHdmi",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UniqueId",
                table: "DeviceHdmi",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_CreatedById",
                table: "DeviceHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_HostId",
                table: "DeviceHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHost_ModifiedById",
                table: "DeviceHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_HostDevice",
                table: "DeviceHost",
                columns: new[] { "DeviceId", "HostId" },
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
                name: "IX_FiscalReceipt_CreatedById",
                table: "FiscalReceipt",
                column: "CreatedById");

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
                name: "IX_Host_HostGroupId",
                table: "Host",
                column: "HostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_IconId",
                table: "Host",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Host_ModifiedById",
                table: "Host",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_Host_HostComputer_HostEndpoint",
                table: "Host",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostComputer_HostId",
                table: "HostComputer",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "UQ_MACAddress_HostComputer",
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
                name: "IX_HostGroup_SecurityProfileId",
                table: "HostGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_HostGroup",
                table: "HostGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupUserBillProfile_BillProfileId",
                table: "HostGroupUserBillProfile",
                column: "BillProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupUserBillProfile_UserGroupId",
                table: "HostGroupUserBillProfile",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_HostGroupUserBillProfile",
                table: "HostGroupUserBillProfile",
                columns: new[] { "HostGroupId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLine_CreatedById",
                table: "HostGroupWaitingLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostGroupWaitingLine_HosGroupId",
                table: "HostGroupWaitingLine",
                column: "HosGroupId",
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
                name: "IX_HostLayoutGroup_CreatedById",
                table: "HostLayoutGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HostLayoutGroup_ModifiedById",
                table: "HostLayoutGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_HostLayoutGroup",
                table: "HostLayoutGroup",
                column: "Name",
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
                name: "IX_HostLayoutGroupLayout_ModifiedById",
                table: "HostLayoutGroupLayout",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_HostLayoutGroupHost",
                table: "HostLayoutGroupLayout",
                columns: new[] { "HostLayoutGroupId", "HostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Icon_CreatedById",
                table: "Icon",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Icon_ModifiedById",
                table: "Icon",
                column: "ModifiedById");

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
                name: "UQ_FiscalReceipt",
                table: "InvoiceFiscalReceipt",
                column: "FiscalReceiptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_CreatedById",
                table: "InvoiceLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ModifiedById",
                table: "InvoiceLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_RegisterId",
                table: "InvoiceLine",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_ShiftId",
                table: "InvoiceLine",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_UserId",
                table: "InvoiceLine",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_PointsTransaction_InvoiceLine_InvoiceLineExtended_InvoiceLineProduct_InvoiceLineSession_InvoiceLineTime_InvoiceLineTimeFixed",
                table: "InvoiceLine",
                column: "PointsTransactionId",
                unique: true);

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
                name: "UQ_StockReturnTransaction_InvoiceLineExtended_InvoiceLineProduct_InvoiceLineTime",
                table: "InvoiceLineExtended",
                column: "StockReturnTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_StockTransaction_InvoiceLineExtended_InvoiceLineProduct_InvoiceLineTime",
                table: "InvoiceLineExtended",
                column: "StockTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineProduct_InvoiceLineId",
                table: "InvoiceLineProduct",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineProduct_ProductId",
                table: "InvoiceLineProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine_InvoiceLineProduct",
                table: "InvoiceLineProduct",
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
                name: "UQ_UsageSession",
                table: "InvoiceLineSession",
                column: "UsageSessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTime_InvoiceLineId",
                table: "InvoiceLineTime",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTime_ProductTimeId",
                table: "InvoiceLineTime",
                column: "ProductTimeId");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine_InvoiceLineTime",
                table: "InvoiceLineTime",
                column: "OrderLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineTimeFixed_InvoiceLineId",
                table: "InvoiceLineTimeFixed",
                column: "InvoiceLineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_OrderLine_InvoiceLineTimeFixed",
                table: "InvoiceLineTimeFixed",
                column: "OrderLineId",
                unique: true);

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
                name: "IX_License_ModifiedById",
                table: "License",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_License",
                table: "License",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_License",
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
                name: "IX_LicenseKey_LicenseId",
                table: "LicenseKey",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKey_ModifiedById",
                table: "LicenseKey",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_LicenseKey",
                table: "LicenseKey",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category",
                table: "Log",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_HostNumber",
                table: "Log",
                column: "HostNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MessageType",
                table: "Log",
                column: "MessageType");

            migrationBuilder.CreateIndex(
                name: "IX_Time",
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
                name: "UQ_MountPoint",
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
                name: "UQ_Name_MonetaryUnit",
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
                name: "IX_Note_CreatedById",
                table: "Note",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ModifiedById",
                table: "Note",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreatedById",
                table: "Payment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ModifiedById",
                table: "Payment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId");

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
                name: "UQ_DepositTransaction",
                table: "Payment",
                column: "DepositTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PointsTransaction_Payment",
                table: "Payment",
                column: "PointTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_CreatedById",
                table: "PaymentIntent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_ModifiedById",
                table: "PaymentIntent",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_PaymentMethodId",
                table: "PaymentIntent",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntent_UserId",
                table: "PaymentIntent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_PaymentIntent_PaymentIntentDeposit_PaymentIntentOrder",
                table: "PaymentIntent",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentDeposit_PaymentIntentId",
                table: "PaymentIntentDeposit",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment_PaymentIntentDeposit",
                table: "PaymentIntentDeposit",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_PaymentIntentId",
                table: "PaymentIntentOrder",
                column: "PaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentIntentOrder_ProductOrderId",
                table: "PaymentIntentOrder",
                column: "ProductOrderId");

            migrationBuilder.CreateIndex(
                name: "UQ_InvoicePayment_PaymentIntentOrder",
                table: "PaymentIntentOrder",
                column: "InvoicePaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_CreatedById",
                table: "PaymentMethod",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_ModifiedById",
                table: "PaymentMethod",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_PaymentMethod",
                table: "PaymentMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_CreatedById",
                table: "PersonalFile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalFile_ModifiedById",
                table: "PersonalFile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_PersonalFile",
                table: "PersonalFile",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_PersonalFile",
                table: "PersonalFile",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginLibrary_CreatedById",
                table: "PluginLibrary",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PluginLibrary_ModifiedById",
                table: "PluginLibrary",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_FileName",
                table: "PluginLibrary",
                column: "FileName",
                unique: true);

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
                name: "IX_Product_ProductId",
                table: "Product",
                column: "ProductId",
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
                name: "IX_ProductBase_ProductGroupId",
                table: "ProductBase",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBase_StockProductId",
                table: "ProductBase",
                column: "StockProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_Barcode_Product_ProductBase_ProductBaseExtended_ProductBundle_ProductTime",
                table: "ProductBase",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Product_ProductBase_ProductBaseExtended_ProductBundle_ProductTime",
                table: "ProductBase",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBaseExtended_ProductId",
                table: "ProductBaseExtended",
                column: "ProductId",
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
                name: "IX_ProductBundleUserPrice_UserGroupId",
                table: "ProductBundleUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductBundlePriceUserGroup",
                table: "ProductBundleUserPrice",
                columns: new[] { "ProductBundleId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_CreatedById",
                table: "ProductGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ModifiedById",
                table: "ProductGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ParentId",
                table: "ProductGroup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_ProductGroup",
                table: "ProductGroup",
                column: "Name",
                unique: true);

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
                name: "UQ_ProductHostGroup",
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
                name: "IX_ProductOrder_PreferedPaymentMethodId",
                table: "ProductOrder",
                column: "PreferedPaymentMethodId");

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
                name: "IX_ProductPeriod_ProductId",
                table: "ProductPeriod",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductPeriodDay",
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
                name: "IX_ProductTax_TaxId",
                table: "ProductTax",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "UQ_TaxProduct",
                table: "ProductTax",
                columns: new[] { "ProductId", "TaxId" },
                unique: true);

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
                name: "UQ_ProductTimeHostGroup",
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
                name: "UQ_ProductTimePeriodDay",
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
                name: "IX_ProductUserDisallowed_UserGroupId",
                table: "ProductUserDisallowed",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductUserGroup_ProductUserDisallowed",
                table: "ProductUserDisallowed",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_CreatedById",
                table: "ProductUserPrice",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_ModifiedById",
                table: "ProductUserPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUserPrice_UserGroupId",
                table: "ProductUserPrice",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "UQ_ProductUserGroup_ProductUserPrice",
                table: "ProductUserPrice",
                columns: new[] { "ProductId", "UserGroupId" },
                unique: true);

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
                name: "IX_RefundDepositPayment_FiscalReceiptId",
                table: "RefundDepositPayment",
                column: "FiscalReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundDepositPayment_RefundId",
                table: "RefundDepositPayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment_RefundDepositPayment",
                table: "RefundDepositPayment",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundInvoicePayment_InvoiceId",
                table: "RefundInvoicePayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundInvoicePayment_RefundId",
                table: "RefundInvoicePayment",
                column: "RefundId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_InvoicePayment_RefundInvoicePayment",
                table: "RefundInvoicePayment",
                column: "InvoicePaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_CreatedById",
                table: "Register",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Register_ModifiedById",
                table: "Register",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_MACAddress_Register",
                table: "Register",
                column: "MacAddress",
                unique: true);

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
                name: "IX_Reservation_CreatedById",
                table: "Reservation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ModifiedById",
                table: "Reservation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Pin",
                table: "Reservation",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_CreatedById",
                table: "ReservationHost",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_HostId",
                table: "ReservationHost",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationHost_PreferedUserId",
                table: "ReservationHost",
                column: "PreferedUserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Reservation_Host",
                table: "ReservationHost",
                columns: new[] { "ReservationId", "HostId" },
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
                name: "IX_ReservationUser_UserId",
                table: "ReservationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_Reservation_User",
                table: "ReservationUser",
                columns: new[] { "ReservationId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfile_CreatedById",
                table: "SecurityProfile",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityProfile_ModifiedById",
                table: "SecurityProfile",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_SecurityProfile",
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
                name: "UQ_SecurityProfilePolicyType",
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
                name: "UQ_NameGroup",
                table: "Setting",
                columns: new[] { "Name", "GroupName" },
                unique: true);

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
                name: "UQ_ShiftCountPaymentMethod",
                table: "ShiftCount",
                columns: new[] { "ShiftId", "PaymentMethodId" },
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
                name: "IX_TaskBase_CreatedById",
                table: "TaskBase",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_ModifiedById",
                table: "TaskBase",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBase_TaskId",
                table: "TaskBase",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_TaskBase_TaskJunction_TaskNotification_TaskProcess_TaskScript",
                table: "TaskBase",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_TaskBase_TaskJunction_TaskNotification_TaskProcess_TaskScript",
                table: "TaskBase",
                column: "Name",
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
                name: "UQ_Name_Tax",
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
                name: "UQ_Value",
                table: "Token",
                column: "Value",
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
                name: "IX_UsageRate_UsageId",
                table: "UsageRate",
                column: "UsageId",
                unique: true);

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
                name: "IX_User_CreatedById",
                table: "User",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_User_ModifiedById",
                table: "User",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_Guid_User_UserGuest_UserMember_UserOperator",
                table: "User",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Identification_User_UserGuest_UserMember_UserOperator",
                table: "User",
                column: "Identification",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_SmartCardUID_User_UserGuest_UserMember_UserOperator",
                table: "User",
                column: "SmartCardUID",
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
                name: "IX_UserAgreementState_UserId",
                table: "UserAgreementState",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserAgreementState",
                table: "UserAgreementState",
                columns: new[] { "UserAgreementId", "UserId" },
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
                name: "UQ_UserAttribute",
                table: "UserAttribute",
                columns: new[] { "UserId", "AttributeId" },
                unique: true);

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
                name: "IX_UserGroup_ModifiedById",
                table: "UserGroup",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_SecurityProfileId",
                table: "UserGroup",
                column: "SecurityProfileId");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_UserGroup",
                table: "UserGroup",
                column: "Name",
                unique: true);

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
                name: "UQ_UserGroupHostGroup",
                table: "UserGroupHostDisallowed",
                columns: new[] { "UserGroupId", "HostGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGuest_UserId",
                table: "UserGuest",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UserGuestHostSlot",
                table: "UserGuest",
                columns: new[] { "ReservedHostId", "ReservedSlot" },
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
                name: "UQ_Email_UserGuest_UserMember",
                table: "UserMember",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Username_UserGuest_UserMember",
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
                name: "IX_UserOperator_UserId",
                table: "UserOperator",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Email_UserOperator",
                table: "UserOperator",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Username_UserOperator",
                table: "UserOperator",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_CreatedById",
                table: "UserPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_ModifiedById",
                table: "UserPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UQ_UserPermission",
                table: "UserPermission",
                columns: new[] { "UserId", "Type", "Value" },
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
                name: "UQ_Name_Variable",
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
                name: "IX_VoidDepositPayment_VoidId",
                table: "VoidDepositPayment",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DepositPayment_VoidDepositPayment",
                table: "VoidDepositPayment",
                column: "DepositPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoidInvoice_VoidId",
                table: "VoidInvoice",
                column: "VoidId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Invoice",
                table: "VoidInvoice",
                column: "InvoiceId",
                unique: true);

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
                name: "FK_HostGroupWaitingLineEntry_User_ModifiedById",
                table: "HostGroupWaitingLineEntry",
                column: "ModifiedById",
                principalTable: "User",
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
                name: "FK_PaymentIntent_UserMember_UserId",
                table: "PaymentIntent",
                column: "UserId",
                principalTable: "UserMember",
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
                name: "FK_Reservation_User_CreatedById",
                table: "Reservation",
                column: "CreatedById",
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
                name: "FK_Reservation_UserMember_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "UserMember",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_CreatedById",
                table: "ReservationHost",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_User_ModifiedById",
                table: "ReservationHost",
                column: "ModifiedById",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationHost_UserMember_PreferedUserId",
                table: "ReservationHost",
                column: "PreferedUserId",
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
                name: "FK_ReservationUser_UserMember_UserId",
                table: "ReservationUser",
                column: "UserId",
                principalTable: "UserMember",
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
                principalColumn: "UserId");

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
        }

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
                name: "FK_Usage_UserMember_UserId",
                table: "Usage");

            migrationBuilder.DropForeignKey(
                name: "FK_UsageSession_UserMember_UserId",
                table: "UsageSession");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroup_BillProfile_BillProfileId",
                table: "UserGroup");

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
                name: "FK_HostGroup_UserGroup_DefaultGuestGroupId",
                table: "HostGroup");

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
                name: "FK_ProductOrder_Host_HostId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_ProductOrder_ProductOrderId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOL_ProductOrder_ProductOrderId",
                table: "ProductOL");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_Invoice_InvoiceId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLine_PointTransaction_PointsTransactionId",
                table: "InvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineExtended_InvoiceLine_InvoiceLineId",
                table: "InvoiceLineExtended");

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
                name: "Feed");

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
                name: "InvoiceFiscalReceipt");

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
                name: "News");

            migrationBuilder.DropTable(
                name: "PaymentIntentDeposit");

            migrationBuilder.DropTable(
                name: "PaymentIntentOrder");

            migrationBuilder.DropTable(
                name: "PluginLibrary");

            migrationBuilder.DropTable(
                name: "PresetTimeSale");

            migrationBuilder.DropTable(
                name: "PresetTimeSaleMoney");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductBundleUserPrice");

            migrationBuilder.DropTable(
                name: "ProductHostHidden");

            migrationBuilder.DropTable(
                name: "ProductImage");

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
                name: "RefundDepositPayment");

            migrationBuilder.DropTable(
                name: "RefundInvoicePayment");

            migrationBuilder.DropTable(
                name: "RegisterTransaction");

            migrationBuilder.DropTable(
                name: "ReservationHost");

            migrationBuilder.DropTable(
                name: "ReservationUser");

            migrationBuilder.DropTable(
                name: "SecurityProfilePolicy");

            migrationBuilder.DropTable(
                name: "SecurityProfileRestriction");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ShiftCount");

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
                name: "UserAttribute");

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
                name: "UserPermission");

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
                name: "PersonalFile");

            migrationBuilder.DropTable(
                name: "AppExe");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "BillRatePeriodDay");

            migrationBuilder.DropTable(
                name: "BundleProduct");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "HostGroupWaitingLine");

            migrationBuilder.DropTable(
                name: "HostLayoutGroup");

            migrationBuilder.DropTable(
                name: "ProductOLSession");

            migrationBuilder.DropTable(
                name: "HostComputer");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "PaymentIntent");

            migrationBuilder.DropTable(
                name: "ProductPeriodDay");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "ProductTimePeriodDay");

            migrationBuilder.DropTable(
                name: "InvoicePayment");

            migrationBuilder.DropTable(
                name: "Refund");

            migrationBuilder.DropTable(
                name: "Reservation");

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
                name: "ProductPeriod");

            migrationBuilder.DropTable(
                name: "ProductTimePeriod");

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
                name: "Payment");

            migrationBuilder.DropTable(
                name: "AppCategory");

            migrationBuilder.DropTable(
                name: "AppEnterprise");

            migrationBuilder.DropTable(
                name: "ProductTime");

            migrationBuilder.DropTable(
                name: "DepositTransaction");

            migrationBuilder.DropTable(
                name: "UserOperator");

            migrationBuilder.DropTable(
                name: "AppGroup");

            migrationBuilder.DropTable(
                name: "UserMember");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BillProfile");

            migrationBuilder.DropTable(
                name: "ProductBase");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Host");

            migrationBuilder.DropTable(
                name: "HostGroup");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropTable(
                name: "SecurityProfile");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "PointTransaction");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "InvoiceLineProduct");

            migrationBuilder.DropTable(
                name: "InvoiceLineExtended");

            migrationBuilder.DropTable(
                name: "StockTransaction");

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
