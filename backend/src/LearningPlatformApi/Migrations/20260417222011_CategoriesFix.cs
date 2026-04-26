using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEntityCourseEntity_Categories_CategoriesTypeCode_Ca~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseId",
                table: "Modules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "TypeCode",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ValueCode",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoriesValueCode",
                table: "CategoryEntityCourseEntity",
                newName: "CategoriesValueName");

            migrationBuilder.RenameColumn(
                name: "CategoriesTypeCode",
                table: "CategoryEntityCourseEntity",
                newName: "CategoriesTypeName");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryEntityCourseEntity_CategoriesTypeCode_CategoriesVal~",
                table: "CategoryEntityCourseEntity",
                newName: "IX_CategoryEntityCourseEntity_CategoriesTypeName_CategoriesVal~");

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseEntityId",
                table: "Modules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishedByUserId",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByUserId",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                columns: new[] { "TypeName", "ValueName" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseEntityId",
                table: "Modules",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PublishedByUserId",
                table: "Courses",
                column: "PublishedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubmittedByUserId",
                table: "Courses",
                column: "SubmittedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCourseEntity_Categories_CategoriesTypeName_Ca~",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CategoriesTypeName", "CategoriesValueName" },
                principalTable: "Categories",
                principalColumns: new[] { "TypeName", "ValueName" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_PublishedByUserId",
                table: "Courses",
                column: "PublishedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_SubmittedByUserId",
                table: "Courses",
                column: "SubmittedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseEntityId",
                table: "Modules",
                column: "CourseEntityId",
                principalTable: "Courses",
                principalColumn: "Id");

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
                name: "FK_CategoryEntityCourseEntity_Categories_CategoriesTypeName_Ca~",
                table: "CategoryEntityCourseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_PublishedByUserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_SubmittedByUserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseEntityId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseEntityId",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PublishedByUserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubmittedByUserId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CourseEntityId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "PublishedByUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubmittedByUserId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CategoriesValueName",
                table: "CategoryEntityCourseEntity",
                newName: "CategoriesValueCode");

            migrationBuilder.RenameColumn(
                name: "CategoriesTypeName",
                table: "CategoryEntityCourseEntity",
                newName: "CategoriesTypeCode");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryEntityCourseEntity_CategoriesTypeName_CategoriesVal~",
                table: "CategoryEntityCourseEntity",
                newName: "IX_CategoryEntityCourseEntity_CategoriesTypeCode_CategoriesVal~");

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "TestTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "ContentBlocks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "ContentBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "CodingTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "TypeCode",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValueCode",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                columns: new[] { "TypeCode", "ValueCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId",
                table: "Modules",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEntityCourseEntity_Categories_CategoriesTypeCode_Ca~",
                table: "CategoryEntityCourseEntity",
                columns: new[] { "CategoriesTypeCode", "CategoriesValueCode" },
                principalTable: "Categories",
                principalColumns: new[] { "TypeCode", "ValueCode" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_Pages_PageId",
                table: "CodingTasks",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_Pages_PageId",
                table: "TestTask",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }
    }
}
