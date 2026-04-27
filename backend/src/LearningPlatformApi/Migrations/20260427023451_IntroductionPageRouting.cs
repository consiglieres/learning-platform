using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatformApi.Migrations
{
    /// <inheritdoc />
    public partial class IntroductionPageRouting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IntroductionPageId",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IntroductionPageVersionOrder",
                table: "Modules");

            migrationBuilder.AddColumn<int>(
                name: "PageVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_PageId_PageVersionOrder",
                table: "Modules",
                columns: new[] { "PageId", "PageVersionOrder" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Pages_PageId_PageVersionOrder",
                table: "Modules",
                columns: new[] { "PageId", "PageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Pages_PageId_PageVersionOrder",
                table: "Modules");

            migrationBuilder.DropIndex(
                name: "IX_Modules_PageId_PageVersionOrder",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "PageVersionOrder",
                table: "Modules");

            migrationBuilder.AddColumn<string>(
                name: "IntroductionPageId",
                table: "Modules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntroductionPageVersionOrder",
                table: "Modules",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modules_IntroductionPageId_IntroductionPageVersionOrder",
                table: "Modules",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" });

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Pages_IntroductionPageId_IntroductionPageVersionOrd~",
                table: "Modules",
                columns: new[] { "IntroductionPageId", "IntroductionPageVersionOrder" },
                principalTable: "Pages",
                principalColumns: new[] { "Id", "VersionOrder" });
        }
    }
}
