using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InStore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Groups = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trains = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpgradesTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Collect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectXp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcatFunctional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxFrame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Giftable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elevation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitCapacity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Defense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Life = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Velocity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttackRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttackInterval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Population = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiftLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostUnitCash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flying = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Protect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Potion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achievement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsLimit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreGroups = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowOnMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowOnMobileStore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlyMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IphoneAdjustments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementDesc_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementDesc_Zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
