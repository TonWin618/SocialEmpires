using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFieldTypeLocalizationStringName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_en",
                table: "LocalizationStrings");

            migrationBuilder.DropColumn(
                name: "Name_zh",
                table: "LocalizationStrings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LocalizationStrings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LocalizationStrings");

            migrationBuilder.AddColumn<string>(
                name: "Name_en",
                table: "LocalizationStrings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_zh",
                table: "LocalizationStrings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
