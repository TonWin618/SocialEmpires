using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DartsItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DartsItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpansionPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Neighbors = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpansionPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FindableItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindableItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "HonorLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Rank_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HonorLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    InStore = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Groups = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trains = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpgradesTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Activation = table.Column<float>(type: "real", nullable: false),
                    Expiration = table.Column<int>(type: "int", nullable: false),
                    Collect = table.Column<int>(type: "int", nullable: false),
                    CollectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectXp = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubcatFunctional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLevel = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    MaxFrame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Giftable = table.Column<bool>(type: "bit", nullable: false),
                    ImgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elevation = table.Column<int>(type: "int", nullable: false),
                    UnitCapacity = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Life = table.Column<int>(type: "int", nullable: false),
                    Velocity = table.Column<int>(type: "int", nullable: false),
                    AttackRange = table.Column<int>(type: "int", nullable: false),
                    AttackInterval = table.Column<int>(type: "int", nullable: false),
                    NewItem = table.Column<bool>(type: "bit", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    GiftLevel = table.Column<int>(type: "int", nullable: false),
                    CostUnitCash = table.Column<int>(type: "int", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flying = table.Column<bool>(type: "bit", nullable: false),
                    Protect = table.Column<bool>(type: "bit", nullable: false),
                    Potion = table.Column<int>(type: "int", nullable: false),
                    Achievement = table.Column<bool>(type: "bit", nullable: false),
                    UnitsLimit = table.Column<int>(type: "int", nullable: false),
                    StoreGroups = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreLevel = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ShowOnMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowOnMobileStore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnlyMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IphoneAdjustments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementDesc_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementDesc_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LevelRankingRewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelRankingRewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToLevel = table.Column<int>(type: "int", nullable: false),
                    RewardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpRequired = table.Column<int>(type: "int", nullable: false),
                    RewardAmount = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationStrings", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "MapPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Hint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reward = table.Column<int>(type: "int", nullable: false),
                    Description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_zh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtmlContent_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtmlContent_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferPacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateTable(
                name: "PlayerInfos",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    CompletedTutorial = table.Column<int>(type: "int", nullable: false),
                    DefaultMap = table.Column<int>(type: "int", nullable: false),
                    MapNames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapSizes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorldId = table.Column<int>(type: "int", nullable: false),
                    LastLoggedIn = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInfos", x => x.Pid);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStates",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gifts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeighborAssists = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletedMissions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RewardedMissions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BonusNextId = table.Column<int>(type: "int", nullable: false),
                    TimestampLastBonus = table.Column<long>(type: "bigint", nullable: false),
                    AttacksSent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlockedEarlyBuildings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Potion = table.Column<int>(type: "int", nullable: false),
                    KompuSpells = table.Column<int>(type: "int", nullable: false),
                    KompuLastTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    KompuSteps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KompuCompleted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpgrades = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlockedSkins = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnlockedQuestIndex = table.Column<int>(type: "int", nullable: false),
                    QuestsRank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Magics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    BoughtUnits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitCollectionsCompleted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DragonNumber = table.Column<int>(type: "int", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    TimeStampTakeCare = table.Column<long>(type: "bigint", nullable: false),
                    DragonNestActive = table.Column<int>(type: "int", nullable: false),
                    MonsterNumber = table.Column<int>(type: "int", nullable: false),
                    StepMonsterNumber = table.Column<int>(type: "int", nullable: false),
                    TimeStampTakeCareMonster = table.Column<long>(type: "bigint", nullable: false),
                    MonsterNestActive = table.Column<int>(type: "int", nullable: false),
                    RiderNumber = table.Column<int>(type: "int", nullable: false),
                    RiderStepNumber = table.Column<int>(type: "int", nullable: false),
                    RiderTimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    TimeStampHeavySiegePeriod = table.Column<long>(type: "bigint", nullable: false),
                    TimeStampHeavySiegeAttack = table.Column<long>(type: "bigint", nullable: false),
                    TimeStampDartsReset = table.Column<long>(type: "bigint", nullable: false),
                    TimeStampDartsNewFree = table.Column<long>(type: "bigint", nullable: false),
                    DartsBalloonsShot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DartsRandomSeed = table.Column<int>(type: "int", nullable: false),
                    DartsHasFree = table.Column<bool>(type: "bit", nullable: false),
                    DartsGotExtra = table.Column<bool>(type: "bit", nullable: false),
                    CountTimePacket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoShowed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teams = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrayAnimals = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strategy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStates", x => x.Pid);
                });

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
                name: "TownPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitsCollectionsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryLangId = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rewards = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Costs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsCollectionsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Parent = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_zh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerSaves",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerInfoPid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrivateStatePid = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSaves", x => x.Pid);
                    table.ForeignKey(
                        name: "FK_PlayerSaves_PlayerInfos_PlayerInfoPid",
                        column: x => x.PlayerInfoPid,
                        principalTable: "PlayerInfos",
                        principalColumn: "Pid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSaves_PlayerStates_PrivateStatePid",
                        column: x => x.PrivateStatePid,
                        principalTable: "PlayerStates",
                        principalColumn: "Pid",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "EmpireMaps",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Expansions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Xp = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Stone = table.Column<int>(type: "int", nullable: false),
                    Wood = table.Column<int>(type: "int", nullable: false),
                    Food = table.Column<int>(type: "int", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skin = table.Column<int>(type: "int", nullable: false),
                    IdCurrentTreasure = table.Column<int>(type: "int", nullable: false),
                    TimestampLastTreasure = table.Column<long>(type: "bigint", nullable: false),
                    ResourcesTraded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedAssists = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncreasedPopulation = table.Column<int>(type: "int", nullable: false),
                    ExpirableUnitsTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversAttackWin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestTimes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastQuestTimes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerSavePid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpireMaps", x => x.Pid);
                    table.ForeignKey(
                        name: "FK_EmpireMaps_PlayerSaves_PlayerSavePid",
                        column: x => x.PlayerSavePid,
                        principalTable: "PlayerSaves",
                        principalColumn: "Pid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmpireMaps_PlayerSavePid",
                table: "EmpireMaps",
                column: "PlayerSavePid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSaves_PlayerInfoPid",
                table: "PlayerSaves",
                column: "PlayerInfoPid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSaves_PrivateStatePid",
                table: "PlayerSaves",
                column: "PrivateStatePid");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DartsItems");

            migrationBuilder.DropTable(
                name: "EmpireMaps");

            migrationBuilder.DropTable(
                name: "ExpansionPrices");

            migrationBuilder.DropTable(
                name: "FindableItems");

            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.DropTable(
                name: "HonorLevels");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "LevelRankingRewards");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "LocalizationStrings");

            migrationBuilder.DropTable(
                name: "Magics");

            migrationBuilder.DropTable(
                name: "MapPrices");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "NeighborAssists");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OfferPacks");

            migrationBuilder.DropTable(
                name: "SocialItems");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "TournamentOpponents");

            migrationBuilder.DropTable(
                name: "TournamentPrices");

            migrationBuilder.DropTable(
                name: "TownPrices");

            migrationBuilder.DropTable(
                name: "UnitsCollectionsCategories");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PlayerSaves");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TournamentTypes");

            migrationBuilder.DropTable(
                name: "PlayerInfos");

            migrationBuilder.DropTable(
                name: "PlayerStates");
        }
    }
}
