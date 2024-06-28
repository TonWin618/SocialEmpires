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
                name: "EmpireMaps");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PlayerSaves");

            migrationBuilder.DropTable(
                name: "PlayerInfos");

            migrationBuilder.DropTable(
                name: "PlayerStates");
        }
    }
}
