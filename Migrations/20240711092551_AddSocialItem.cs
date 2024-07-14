using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddSocialItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkerCost = table.Column<int>(type: "int", nullable: false),
                    Workers_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workers_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialItems");
        }
    }
}
