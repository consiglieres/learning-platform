using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class NullabeFKPageid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TestTask",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "TestTask",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TestTask",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Pages",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Pages",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Pages",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Modules",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Modules",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Modules",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Lessons",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Lessons",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Lessons",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Courses",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Courses",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Courses",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ContentBlocks",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ContentBlocks",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContentBlocks",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "CodingTasks",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "CodingTasks",
                type: "character varying(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CodingTasks",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_CreatedBy",
                table: "TestTask",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_DeletedBy",
                table: "TestTask",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_UpdatedBy",
                table: "TestTask",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CreatedBy",
                table: "Pages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_DeletedBy",
                table: "Pages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_UpdatedBy",
                table: "Pages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CreatedBy",
                table: "Modules",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_DeletedBy",
                table: "Modules",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_UpdatedBy",
                table: "Modules",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CreatedBy",
                table: "Lessons",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_DeletedBy",
                table: "Lessons",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_UpdatedBy",
                table: "Lessons",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DeletedBy",
                table: "Courses",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UpdatedBy",
                table: "Courses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_CreatedBy",
                table: "ContentBlocks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_DeletedBy",
                table: "ContentBlocks",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_UpdatedBy",
                table: "ContentBlocks",
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
                name: "IX_CodingTasks_UpdatedBy",
                table: "CodingTasks",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_CreatedBy",
                table: "CodingTasks",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_DeletedBy",
                table: "CodingTasks",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingTasks_AspNetUsers_UpdatedBy",
                table: "CodingTasks",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_AspNetUsers_CreatedBy",
                table: "ContentBlocks",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_AspNetUsers_DeletedBy",
                table: "ContentBlocks",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_AspNetUsers_UpdatedBy",
                table: "ContentBlocks",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_CreatedBy",
                table: "Courses",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_DeletedBy",
                table: "Courses",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UpdatedBy",
                table: "Courses",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_CreatedBy",
                table: "Lessons",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_DeletedBy",
                table: "Lessons",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_UpdatedBy",
                table: "Lessons",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_AspNetUsers_CreatedBy",
                table: "Modules",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_AspNetUsers_DeletedBy",
                table: "Modules",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_AspNetUsers_UpdatedBy",
                table: "Modules",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_CreatedBy",
                table: "Pages",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_DeletedBy",
                table: "Pages",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_UpdatedBy",
                table: "Pages",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedBy",
                table: "TestTask",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedBy",
                table: "TestTask",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedBy",
                table: "TestTask",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_ContentBlocks_AspNetUsers_CreatedBy",
                table: "ContentBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_AspNetUsers_DeletedBy",
                table: "ContentBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_AspNetUsers_UpdatedBy",
                table: "ContentBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_CreatedBy",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_DeletedBy",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UpdatedBy",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_CreatedBy",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_DeletedBy",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_UpdatedBy",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_AspNetUsers_CreatedBy",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_AspNetUsers_DeletedBy",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_AspNetUsers_UpdatedBy",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_CreatedBy",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_DeletedBy",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_UpdatedBy",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_CreatedBy",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedBy",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_CreatedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_DeletedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_UpdatedBy",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_Pages_CreatedBy",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_DeletedBy",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_UpdatedBy",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CreatedBy",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_DeletedBy",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_UpdatedBy",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CreatedBy",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_DeletedBy",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_UpdatedBy",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CreatedBy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DeletedBy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UpdatedBy",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_CreatedBy",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_DeletedBy",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_UpdatedBy",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "TestTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "TestTask",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "TestTask",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Pages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Pages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Pages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Modules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Modules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Modules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Lessons",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Courses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "ContentBlocks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ContentBlocks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ContentBlocks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "CodingTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "CodingTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)");
        }
    }
}
