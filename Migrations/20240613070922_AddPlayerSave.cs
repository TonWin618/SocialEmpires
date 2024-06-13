using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialEmpires.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerSave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerInfos",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Pic = table.Column<string>(type: "TEXT", nullable: false),
                    Cash = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedTutorial = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultMap = table.Column<int>(type: "INTEGER", nullable: false),
                    MapNames = table.Column<string>(type: "TEXT", nullable: false),
                    MapSizes = table.Column<string>(type: "TEXT", nullable: false),
                    WorldId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastLoggedIn = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerInfos", x => x.Pid);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStates",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "TEXT", nullable: false),
                    Gifts = table.Column<string>(type: "TEXT", nullable: false),
                    NeighborAssists = table.Column<string>(type: "TEXT", nullable: false),
                    CompletedMissions = table.Column<string>(type: "TEXT", nullable: false),
                    RewardedMissions = table.Column<string>(type: "TEXT", nullable: false),
                    BonusNextId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimestampLastBonus = table.Column<long>(type: "INTEGER", nullable: false),
                    AttacksSent = table.Column<string>(type: "TEXT", nullable: false),
                    UnlockedEarlyBuildings = table.Column<string>(type: "TEXT", nullable: false),
                    Potion = table.Column<int>(type: "INTEGER", nullable: false),
                    KompuSpells = table.Column<int>(type: "INTEGER", nullable: false),
                    KompuLastTimeStamp = table.Column<long>(type: "INTEGER", nullable: false),
                    KompuSteps = table.Column<string>(type: "TEXT", nullable: false),
                    KompuCompleted = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpgrades = table.Column<string>(type: "TEXT", nullable: false),
                    UnlockedSkins = table.Column<string>(type: "TEXT", nullable: false),
                    UnlockedQuestIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestsRank = table.Column<string>(type: "TEXT", nullable: false),
                    Magics = table.Column<string>(type: "TEXT", nullable: false),
                    Mana = table.Column<int>(type: "INTEGER", nullable: false),
                    BoughtUnits = table.Column<string>(type: "TEXT", nullable: false),
                    UnitCollectionsCompleted = table.Column<string>(type: "TEXT", nullable: false),
                    DragonNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampTakeCare = table.Column<long>(type: "INTEGER", nullable: false),
                    DragonNestActive = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    StepMonsterNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeStampTakeCareMonster = table.Column<long>(type: "INTEGER", nullable: false),
                    MonsterNestActive = table.Column<int>(type: "INTEGER", nullable: false),
                    RiderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RiderStepNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    RiderTimeStamp = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeStampHeavySiegePeriod = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeStampHeavySiegeAttack = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeStampDartsReset = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeStampDartsNewFree = table.Column<long>(type: "INTEGER", nullable: false),
                    DartsBalloonsShot = table.Column<string>(type: "TEXT", nullable: false),
                    DartsRandomSeed = table.Column<int>(type: "INTEGER", nullable: false),
                    DartsHasFree = table.Column<bool>(type: "INTEGER", nullable: false),
                    DartsGotExtra = table.Column<bool>(type: "INTEGER", nullable: false),
                    CountTimePacket = table.Column<string>(type: "TEXT", nullable: false),
                    InfoShowed = table.Column<string>(type: "TEXT", nullable: false),
                    Teams = table.Column<string>(type: "TEXT", nullable: false),
                    ArrayAnimals = table.Column<string>(type: "TEXT", nullable: false),
                    Strategy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStates", x => x.Pid);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSaves",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerInfoPid = table.Column<string>(type: "TEXT", nullable: false),
                    PrivateStatePid = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "EmpireMaps",
                columns: table => new
                {
                    Pid = table.Column<string>(type: "TEXT", nullable: false),
                    Expansions = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    Coins = table.Column<int>(type: "INTEGER", nullable: false),
                    Xp = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Stone = table.Column<int>(type: "INTEGER", nullable: false),
                    Wood = table.Column<int>(type: "INTEGER", nullable: false),
                    Food = table.Column<int>(type: "INTEGER", nullable: false),
                    Race = table.Column<string>(type: "TEXT", nullable: false),
                    Skin = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCurrentTreasure = table.Column<int>(type: "INTEGER", nullable: false),
                    TimestampLastTreasure = table.Column<long>(type: "INTEGER", nullable: false),
                    ResourcesTraded = table.Column<string>(type: "TEXT", nullable: false),
                    ReceivedAssists = table.Column<string>(type: "TEXT", nullable: false),
                    IncreasedPopulation = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpirableUnitsTime = table.Column<string>(type: "TEXT", nullable: false),
                    UniversAttackWin = table.Column<string>(type: "TEXT", nullable: false),
                    QuestTimes = table.Column<string>(type: "TEXT", nullable: false),
                    LastQuestTimes = table.Column<string>(type: "TEXT", nullable: false),
                    Items = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerSavePid = table.Column<string>(type: "TEXT", nullable: true)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpireMaps");

            migrationBuilder.DropTable(
                name: "PlayerSaves");

            migrationBuilder.DropTable(
                name: "PlayerInfos");

            migrationBuilder.DropTable(
                name: "PlayerStates");
        }
    }
}
