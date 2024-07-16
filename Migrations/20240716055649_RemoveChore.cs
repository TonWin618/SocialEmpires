using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Globals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsCollectionsCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chores", x => x.Id);
                });
        }
    }
}
