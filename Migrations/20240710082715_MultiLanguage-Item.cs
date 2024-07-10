using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class MultiLanguageItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_Zh",
                table: "Items",
                newName: "Name_zh");

            migrationBuilder.RenameColumn(
                name: "Name_En",
                table: "Items",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "AchievementDesc_Zh",
                table: "Items",
                newName: "AchievementDesc_zh");

            migrationBuilder.RenameColumn(
                name: "AchievementDesc_En",
                table: "Items",
                newName: "AchievementDesc_en");

            migrationBuilder.RenameColumn(
                name: "HtmlContent_Zh",
                table: "Bulletins",
                newName: "HtmlContent_zh");

            migrationBuilder.RenameColumn(
                name: "HtmlContent_En",
                table: "Bulletins",
                newName: "HtmlContent_en");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_zh",
                table: "Items",
                newName: "Name_Zh");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Items",
                newName: "Name_En");

            migrationBuilder.RenameColumn(
                name: "AchievementDesc_zh",
                table: "Items",
                newName: "AchievementDesc_Zh");

            migrationBuilder.RenameColumn(
                name: "AchievementDesc_en",
                table: "Items",
                newName: "AchievementDesc_En");

            migrationBuilder.RenameColumn(
                name: "HtmlContent_zh",
                table: "Bulletins",
                newName: "HtmlContent_Zh");

            migrationBuilder.RenameColumn(
                name: "HtmlContent_en",
                table: "Bulletins",
                newName: "HtmlContent_En");
        }
    }
}
