using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class LessonIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "LessonOrder", "VersionOrder" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleId_LessonOrder",
                table: "Lessons",
                columns: new[] { "ModuleId", "LessonOrder" },
                unique: true);
        }
    }
}
