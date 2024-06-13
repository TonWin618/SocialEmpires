using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPlayerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmpireMaps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmpireMaps");
        }
    }
}
