using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVersionRealization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId_CoursesVersion~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_TaskBaseEntity_Id",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_Pages_PageId_PageVersion",
                table: "ContentBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Modules_ModuleId_ModuleVersionOrder",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Pages_PageEntityId_PageEntityVersionOrder",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseId_CourseVersion",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Pages_PageId_PageVersionOrder",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_TaskBaseEntity_Id",
                table: "TestTask");

            migrationBuilder.DropTable(
                name: "TaskBaseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseId_CourseVersion",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_PageId_PageVersionOrder",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ModuleId_ModuleVersionOrder",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_PageEntityId_PageEntityVersionOrder",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageId_PageVersion",
                table: "ContentBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEntityCourseEntity_CoursesId_CoursesVersionOrder",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseVersion",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ModuleVersionOrder",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "PageEntityVersionOrder",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IntroductionPageVersionOrder",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PageVersion",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "CoursesVersionOrder",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Modules",
                newName: "CourseEntityId");

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

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
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
                name: "DeletedByUserId",
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
                nullable: false,
                defaultValue: "");

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
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageEntityId",
                table: "ContentBlocks",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
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
                name: "DeletedByUserId",
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

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CategoriesTypeName", "CategoriesValueName" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_CreatedByUserId",
                table: "TestTask",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_DeletedByUserId",
                table: "TestTask",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_LessonId",
                table: "TestTask",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_PageId",
                table: "TestTask",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_UpdatedByUserId",
                table: "TestTask",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseEntityId",
                table: "Modules",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_PageId",
                table: "Modules",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleId",
                table: "Lessons",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PageEntityId",
                table: "Lessons",
                column: "PageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IntroductionPageId",
                table: "Courses",
                column: "IntroductionPageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageEntityId",
                table: "ContentBlocks",
                column: "PageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_CreatedByUserId",
                table: "CodingTasks",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_DeletedByUserId",
                table: "CodingTasks",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_LessonId",
                table: "CodingTasks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_PageId",
                table: "CodingTasks",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_UpdatedByUserId",
                table: "CodingTasks",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityCourseEntity_CategoriesTypeName_CategoriesVal~",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CategoriesTypeName", "CategoriesValueName" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId",
                table: "CategoryEntityCourseEntity",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_CreatedByUserId",
                table: "CodingTasks",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_DeletedByUserId",
                table: "CodingTasks",
                column: "DeletedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_UpdatedByUserId",
                table: "CodingTasks",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Lessons_LessonId",
                table: "CodingTasks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Pages_PageEntityId",
                table: "ContentBlocks",
                column: "PageEntityId",
                principalTable: "Pages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Pages_IntroductionPageId",
                table: "Courses",
                column: "IntroductionPageId",
                principalTable: "Pages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Modules_ModuleId",
                table: "Lessons",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Pages_PageEntityId",
                table: "Lessons",
                column: "PageEntityId",
                principalTable: "Pages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseEntityId",
                table: "Modules",
                column: "CourseEntityId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Pages_PageId",
                table: "Modules",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedByUserId",
                table: "TestTask",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedByUserId",
                table: "TestTask",
                column: "DeletedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedByUserId",
                table: "TestTask",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_Lessons_LessonId",
                table: "TestTask",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_CreatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_DeletedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_AspNetUsers_UpdatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Lessons_LessonId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_Pages_PageEntityId",
                table: "ContentBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Pages_IntroductionPageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Modules_ModuleId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Pages_PageEntityId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseEntityId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Pages_PageId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedByUserId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Lessons_LessonId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_DeletedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_LessonId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_PageId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseEntityId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_PageId",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ModuleId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_PageEntityId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_IntroductionPageId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageEntityId",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_CreatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_DeletedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_LessonId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_PageId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_UpdatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEntityCourseEntity_CategoriesTypeName_CategoriesVal~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
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
                name: "Name",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "PageEntityId",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
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
                name: "Name",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "CodingTasks");

            migrationBuilder.RenameColumn(
                name: "CourseEntityId",
                table: "Modules",
                newName: "Tag");

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Pages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseVersion",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModuleVersionOrder",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageEntityVersionOrder",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntroductionPageVersionOrder",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageVersion",
                table: "ContentBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoursesVersionOrder",
                table: "CategoryEntityCourseEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CategoriesTypeName", "CategoriesValueName", "CoursesId", "CoursesVersionOrder" });

            migrationBuilder.CreateTable(
                name: "TaskBaseEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: false),
                    DeletedByUserId = table.Column<string>(type: "text", nullable: true),
                    LessonId = table.Column<string>(type: "text", nullable: false),
                    PageId = table.Column<string>(type: "text", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    LessonVersion = table.Column<int>(type: "integer", nullable: false),
                    PageVersionOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DifficultyCategory = table.Column<string>(type: "text", nullable: false),
                    DifficultyPoints = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    VersionOrder = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_CourseId_ModuleOrder",
                table: "Modules",
                columns: new[] { "CourseId", "ModuleOrder", "VersionOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId_CourseVersion",
                table: "Modules",
                columns: new[] { "CourseId", "CourseVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_PageId_PageVersionOrder",
                table: "Modules",
                columns: new[] { "PageId", "PageVersionOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleId_ModuleVersionOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "ModuleVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PageEntityId_PageEntityVersionOrder",
                table: "Lessons",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "LessonOrder", "VersionOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Courses",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks",
                columns: new[] { "PageId", "Order", "PageVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageId_PageVersion",
                table: "ContentBlocks",
                columns: new[] { "PageId", "PageVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityCourseEntity_CoursesId_CoursesVersionOrder",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CoursesVersionOrder" });

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
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId_CoursesVersion~",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CoursesVersionOrder" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_TaskBaseEntity_Id",
                table: "CodingTasks",
                column: "Id",
                principalTable: "TaskBaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Pages_PageId_PageVersion",
                table: "ContentBlocks",
                columns: new[] { "PageId", "PageVersion" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Courses",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Modules_ModuleId_ModuleVersionOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "ModuleVersionOrder" },
                principalTable: "Modules",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Pages_PageEntityId_PageEntityVersionOrder",
                table: "Lessons",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseId_CourseVersion",
                table: "Modules",
                columns: new[] { "CourseId", "CourseVersion" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Pages_PageId_PageVersionOrder",
                table: "Modules",
                columns: new[] { "PageId", "PageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_TaskBaseEntity_Id",
                table: "TestTask",
                column: "Id",
                principalTable: "TaskBaseEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
