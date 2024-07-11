using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddOfferPack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferPacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CostCash = table.Column<int>(type: "int", nullable: false),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Stone = table.Column<int>(type: "int", nullable: false),
                    Food = table.Column<int>(type: "int", nullable: false),
                    Wood = table.Column<int>(type: "int", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    PackType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPacks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferPacks");
        }
    }
}
