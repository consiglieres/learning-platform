using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class ModuleCourseBind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseEntityId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseEntityVersionOrder",
                table: "Modules");

            migrationBuilder.AddColumn<int>(
                name: "CourseVersion",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules",
                columns: new[] { "CourseId", "ModuleOrder", "VersionOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseId_CourseVersion",
                table: "Modules",
                columns: new[] { "CourseId", "CourseVersion" });

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseId_CourseVersion",
                table: "Modules",
                columns: new[] { "CourseId", "CourseVersion" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseId_CourseVersion",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseId_CourseVersion",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "CourseVersion",
                table: "Modules");

            migrationBuilder.AddColumn<string>(
                name: "CourseEntityId",
                table: "Modules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseEntityVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseId_ModuleOrder",
                table: "Modules",
                columns: new[] { "CourseId", "ModuleOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules",
                columns: new[] { "CourseEntityId", "CourseEntityVersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseEntityId_CourseEntityVersionOrder",
                table: "Modules",
                columns: new[] { "CourseEntityId", "CourseEntityVersionOrder" },
                principalTable: "Courses",
                principalColumns: new[] { "Id", "VersionOrder" });
        }
    }
}
