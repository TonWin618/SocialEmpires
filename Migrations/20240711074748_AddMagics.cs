using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddMagics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Magics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Gold = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    ImgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    Description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Magics");
        }
    }
}
