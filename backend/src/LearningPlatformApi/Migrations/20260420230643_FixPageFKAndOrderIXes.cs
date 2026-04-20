using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPageFKAndOrderIXes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_Pages_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "PageEntityId",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "PageEntityVersionOrder",
                table: "ContentBlocks");

            migrationBuilder.AddColumn<int>(
                name: "PageVersion",
                table: "ContentBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules",
                columns: new[] { "CourseId", "ModuleOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "LessonOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks",
                columns: new[] { "PageId", "Order", "PageVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageId_PageVersion",
                table: "ContentBlocks",
                columns: new[] { "PageId", "PageVersion" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Pages_PageId_PageVersion",
                table: "ContentBlocks",
                columns: new[] { "PageId", "PageVersion" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentBlocks_Pages_PageId_PageVersion",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageId_PageVersion",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "PageVersion",
                table: "ContentBlocks");

            migrationBuilder.AddColumn<string>(
                name: "PageEntityId",
                table: "ContentBlocks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageEntityVersionOrder",
                table: "ContentBlocks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContentBlocks_Pages_PageEntityId_PageEntityVersionOrder",
                table: "ContentBlocks",
                columns: new[] { "PageEntityId", "PageEntityVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });
        }
    }
}
