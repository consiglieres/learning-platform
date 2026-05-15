using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class TestTaskBaseRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_TestTask_AspNetUsers_CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_DeletedByUserId",
                table: "TestTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TestTask_AspNetUsers_UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_DeletedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_TestTask_UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_CreatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_DeletedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_UpdatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "CodingTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "CodingTasks");

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
                name: "IX_CodingTasks_CreatedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_DeletedBy",
                table: "CodingTasks");

            migrationBuilder.DropIndex(
                name: "IX_CodingTasks_UpdatedBy",
                table: "CodingTasks");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "TestTask",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "TestTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "CodingTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "CodingTasks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_CreatedByUserId",
                table: "TestTask",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_DeletedByUserId",
                table: "TestTask",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_UpdatedByUserId",
                table: "TestTask",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_CreatedByUserId",
                table: "CodingTasks",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_DeletedByUserId",
                table: "CodingTasks",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CodingTasks_UpdatedByUserId",
                table: "CodingTasks",
                column: "UpdatedByUserId");

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
        }
    }
}
