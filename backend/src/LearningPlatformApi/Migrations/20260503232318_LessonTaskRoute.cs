using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class LessonTaskRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Lessons_LessonId_LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Pages_PageId_PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedBy",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedBy",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Lessons_LessonId_LessonVersionOrder",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Pages_PageId_PageVersionOrder",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_CreatedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_DeletedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_LessonId_LessonVersionOrder",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_PageId_PageVersionOrder",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_LessonId_LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_PageId_PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DifficultyCategory",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DifficultyPoints",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "LessonVersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DifficultyCategory",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DifficultyPoints",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TaskBaseEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    DifficultyCategory = table.Column<string>(type: "text", nullable: false),
                    DifficultyPoints = table.Column<int>(type: "integer", nullable: false),
                    LessonId = table.Column<string>(type: "text", nullable: false),
                    LessonVersion = table.Column<int>(type: "integer", nullable: false),
                    PageId = table.Column<string>(type: "text", nullable: false),
                    PageVersionOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedByUserId = table.Column<string>(type: "text", nullable: true),
                    VersionOrder = table.Column<int>(type: "integer", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskBaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskBaseEntity_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskBaseEntity_AspNetUsers_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskBaseEntity_AspNetUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskBaseEntity_Lessons_LessonId_LessonVersion",
                        columns: x => new { x.LessonId, x.LessonVersion },
                        principalTable: "Lessons",
                        principalColumns: new[] { "Id", "VersionOrder" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskBaseEntity_Pages_PageId_PageVersionOrder",
                        columns: x => new { x.PageId, x.PageVersionOrder },
                        principalTable: "Pages",
                        principalColumns: new[] { "Id", "VersionOrder" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseEntity_CreatedByUserId",
                table: "TaskBaseEntity",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseEntity_DeletedByUserId",
                table: "TaskBaseEntity",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseEntity_LessonId_LessonVersion",
                table: "TaskBaseEntity",
                columns: new[] { "LessonId", "LessonVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseEntity_PageId_PageVersionOrder",
                table: "TaskBaseEntity",
                columns: new[] { "PageId", "PageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskBaseEntity_UpdatedByUserId",
                table: "TaskBaseEntity",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_TaskBaseEntity_Id",
                table: "CodingTasks",
                column: "Id",
                principalTable: "TaskBaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_TaskBaseEntity_Id",
                table: "TestTask",
                column: "Id",
                principalTable: "TaskBaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_TaskBaseEntity_Id",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_TaskBaseEntity_Id",
                table: "TestTask");

            migrationBuilder.DropTable(
                name: "TaskBaseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks");

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DifficultyCategory",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyPoints",
                table: "TestTask",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonVersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TestTask",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "CodingTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "CodingTasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DifficultyCategory",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DifficultyPoints",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonId",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LessonVersionOrder",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "CodingTasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_CreatedBy",
                table: "TestTask",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_DeletedBy",
                table: "TestTask",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_LessonId_LessonVersionOrder",
                table: "TestTask",
                columns: new[] { "LessonId", "LessonVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_PageId_PageVersionOrder",
                table: "TestTask",
                columns: new[] { "PageId", "PageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_UpdatedBy",
                table: "TestTask",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_CreatedBy",
                table: "CodingTasks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_DeletedBy",
                table: "CodingTasks",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_LessonId_LessonVersionOrder",
                table: "CodingTasks",
                columns: new[] { "LessonId", "LessonVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_PageId_PageVersionOrder",
                table: "CodingTasks",
                columns: new[] { "PageId", "PageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_UpdatedBy",
                table: "CodingTasks",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_CreatedBy",
                table: "CodingTasks",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_DeletedBy",
                table: "CodingTasks",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_UpdatedBy",
                table: "CodingTasks",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Lessons_LessonId_LessonVersionOrder",
                table: "CodingTasks",
                columns: new[] { "LessonId", "LessonVersionOrder" },
                principalTable: "Lessons",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Pages_PageId_PageVersionOrder",
                table: "CodingTasks",
                columns: new[] { "PageId", "PageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedBy",
                table: "TestTask",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedBy",
                table: "TestTask",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedBy",
                table: "TestTask",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_Lessons_LessonId_LessonVersionOrder",
                table: "TestTask",
                columns: new[] { "LessonId", "LessonVersionOrder" },
                principalTable: "Lessons",
                principalColumns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_Pages_PageId_PageVersionOrder",
                table: "TestTask",
                columns: new[] { "PageId", "PageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });
        }
    }
}
