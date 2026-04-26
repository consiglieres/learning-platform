using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionToPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId",
                table: "CategoryEntityCourseEntity");

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
                name: "FK_Modules_Pages_IntroductionPageId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Lessons_LessonId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_LessonId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_PageId",
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
                name: "IX_Modules_IntroductionPageId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_LessonId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_PageId",
                table: "CodingTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEntityCourseEntity_CategoriesTypeName_CategoriesVal~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "TestTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LessonId",
                table: "TestTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "LessonVersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseEntityVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntroductionPageVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "IntroductionPageVersionOrder",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageEntityVersionOrder",
                table: "ContentBlocks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonVersionOrder",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "CodingTasks",
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
                name: "PK_TestTask",
                table: "TestTask",
                columns: new[] { "Id", "VersionOrder" });

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
                name: "PK_CodingTasks",
                table: "CodingTasks",
                columns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CategoriesTypeName", "CategoriesValueName", "CoursesId", "CoursesVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_LessonId_LessonVersionOrder",
                table: "TestTask",
                columns: new[] { "LessonId", "LessonVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_PageId_PageVersionOrder",
                table: "TestTask",
                columns: new[] { "PageId", "PageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules",
                columns: new[] { "CourseEntityId", "CourseEntityVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Modules",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ModuleId_ModuleVersionOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "ModuleVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PageEntityId_PageEntityVersionOrder",
                table: "Lessons",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Courses",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_LessonId_LessonVersionOrder",
                table: "CodingTasks",
                columns: new[] { "LessonId", "LessonVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_PageId_PageVersionOrder",
                table: "CodingTasks",
                columns: new[] { "PageId", "PageVersionOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEntityCourseEntity_CoursesId_CoursesVersionOrder",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CoursesVersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId_CoursesVersion~",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CoursesVersionOrder" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ContentBlocks_Pages_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });

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
                name: "FK_Modules_Courses_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules",
                columns: new[] { "CourseEntityId", "CourseEntityVersionOrder" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Modules",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCourseEntity_Courses_CoursesId_CoursesVersion~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Lessons_LessonId_LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Pages_PageId_PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_Pages_PageEntityId_PageEntityVersionOrder",
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
                name: "FK_Modules_Courses_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Modules");

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
                name: "IX_TestTask_LessonId_LessonVersionOrder",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_PageId_PageVersionOrder",
                table: "TestTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_IntroductionPageId_IntroductionPageVersionOrder",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodingTasks",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_LessonId_LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_PageId_PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEntityCourseEntity_CoursesId_CoursesVersionOrder",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropColumn(
                name: "LessonVersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IntroductionPageVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ModuleVersionOrder",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "PageEntityVersionOrder",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IntroductionPageVersionOrder",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PageEntityVersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "LessonVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CoursesVersionOrder",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LessonId",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestTask",
                table: "TestTask",
                column: "Id");

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
                name: "PK_CodingTasks",
                table: "CodingTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntityCourseEntity",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CoursesId", "CategoriesTypeName", "CategoriesValueName" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_LessonId",
                table: "TestTask",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_PageId",
                table: "TestTask",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseEntityId",
                table: "Modules",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IntroductionPageId",
                table: "Modules",
                column: "IntroductionPageId");

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
                name: "IX_CodingTasks_LessonId",
                table: "CodingTasks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_PageId",
                table: "CodingTasks",
                column: "PageId");

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
                name: "FK_Modules_Pages_IntroductionPageId",
                table: "Modules",
                column: "IntroductionPageId",
                principalTable: "Pages",
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
    }
}
