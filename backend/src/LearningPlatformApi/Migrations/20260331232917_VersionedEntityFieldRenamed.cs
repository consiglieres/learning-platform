using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class VersionedEntityFieldRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Modules",
                newName: "VersionOrder");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Lessons",
                newName: "VersionOrder");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Courses",
                newName: "VersionOrder");

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "TestTask",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "ContentBlocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionOrder",
                table: "CodingTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "ContentBlocks");

            migrationBuilder.DropColumn(
                name: "VersionOrder",
                table: "CodingTasks");

            migrationBuilder.RenameColumn(
                name: "VersionOrder",
                table: "Modules",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "VersionOrder",
                table: "Lessons",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "VersionOrder",
                table: "Courses",
                newName: "Order");
        }
    }
}
