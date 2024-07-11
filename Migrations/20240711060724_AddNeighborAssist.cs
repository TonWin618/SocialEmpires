using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddNeighborAssist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NeighborAssists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rnd = table.Column<int>(type: "int", nullable: false),
                    Action_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notification_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notification_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reward_Cash = table.Column<int>(type: "int", nullable: false),
                    Reward_Coins = table.Column<int>(type: "int", nullable: false),
                    Reward_Xp = table.Column<int>(type: "int", nullable: false),
                    Task_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Task_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeighborAssists", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeighborAssists");
        }
    }
}
