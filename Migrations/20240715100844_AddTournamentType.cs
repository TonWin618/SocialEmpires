using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ResourceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    NumPlayers = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLevel = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<long>(type: "bigint", nullable: false),
                    WeeklyTournaments = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentOpponents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentOpponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentOpponents_TournamentTypes_TournamentTypeId",
                        column: x => x.TournamentTypeId,
                        principalTable: "TournamentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TournamentPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    G = table.Column<int>(type: "int", nullable: false),
                    U = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentPrices_TournamentTypes_TournamentTypeId",
                        column: x => x.TournamentTypeId,
                        principalTable: "TournamentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentOpponents_TournamentTypeId",
                table: "TournamentOpponents",
                column: "TournamentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPrices_TournamentTypeId",
                table: "TournamentPrices",
                column: "TournamentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentOpponents");

            migrationBuilder.DropTable(
                name: "TournamentPrices");

            migrationBuilder.DropTable(
                name: "TournamentTypes");
        }
    }
}
