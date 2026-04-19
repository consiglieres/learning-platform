using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class UniqIndexConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "PageId", "Order" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks");
        }
    }
}
