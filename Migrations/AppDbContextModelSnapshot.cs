﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialEmpires.Models;

#nullable disable

namespace SocialEmpires.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SocialEmpires.Models.Bulletins.Bulletin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublishedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PublisherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("HtmlContent", "SocialEmpires.Models.Bulletins.Bulletin.HtmlContent#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Bulletins");
                });

            modelBuilder.Entity("SocialEmpires.Models.Configs.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Achievement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Activation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Attack")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttackInterval")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttackRange")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Collect")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollectXp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostUnitCash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Defense")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Elevation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expiration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flying")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GiftLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Giftable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Groups")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InStore")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IphoneAdjustments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Life")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaxFrame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewItem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlyMobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Population")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Potion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Protect")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShowOnMobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShowOnMobileStore")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreGroups")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubcatFunctional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubcategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trains")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitCapacity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitsLimit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpgradesTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Velocity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Width")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Xp")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("AchievementDesc", "SocialEmpires.Models.Configs.Item.AchievementDesc#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SocialEmpires.Models.Configs.Item.Name#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SocialEmpires.Models.Configs.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExpRequired")
                        .HasColumnType("int");

                    b.Property<int>("RewardAmount")
                        .HasColumnType("int");

                    b.Property<string>("RewardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToLevel")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "SocialEmpires.Models.Configs.Level.Name#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("SocialEmpires.Models.Configs.Mission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Hint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Reward")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "SocialEmpires.Models.Configs.Mission.Description#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "SocialEmpires.Models.Configs.Mission.Title#MultiLanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("en")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("zh")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.EmpireMap", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<string>("Expansions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpirableUnitsTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Food")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("IdCurrentTreasure")
                        .HasColumnType("int");

                    b.Property<int>("IncreasedPopulation")
                        .HasColumnType("int");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastQuestTimes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("PlayerSavePid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuestTimes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceivedAssists")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesTraded")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Skin")
                        .HasColumnType("int");

                    b.Property<int>("Stone")
                        .HasColumnType("int");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<long>("TimestampLastTreasure")
                        .HasColumnType("bigint");

                    b.Property<string>("UniversAttackWin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wood")
                        .HasColumnType("int");

                    b.Property<int>("Xp")
                        .HasColumnType("int");

                    b.HasKey("Pid");

                    b.HasIndex("PlayerSavePid");

                    b.ToTable("EmpireMaps");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.PlayerInfo", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cash")
                        .HasColumnType("int");

                    b.Property<int>("CompletedTutorial")
                        .HasColumnType("int");

                    b.Property<int>("DefaultMap")
                        .HasColumnType("int");

                    b.Property<long>("LastLoggedIn")
                        .HasColumnType("bigint");

                    b.Property<string>("MapNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapSizes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorldId")
                        .HasColumnType("int");

                    b.HasKey("Pid");

                    b.ToTable("PlayerInfos");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.PlayerSave", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlayerInfoPid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PrivateStatePid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Pid");

                    b.HasIndex("PlayerInfoPid");

                    b.HasIndex("PrivateStatePid");

                    b.ToTable("PlayerSaves");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.PlayerState", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArrayAnimals")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttacksSent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BonusNextId")
                        .HasColumnType("int");

                    b.Property<string>("BoughtUnits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompletedMissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountTimePacket")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DartsBalloonsShot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DartsGotExtra")
                        .HasColumnType("bit");

                    b.Property<bool>("DartsHasFree")
                        .HasColumnType("bit");

                    b.Property<int>("DartsRandomSeed")
                        .HasColumnType("int");

                    b.Property<int>("DragonNestActive")
                        .HasColumnType("int");

                    b.Property<int>("DragonNumber")
                        .HasColumnType("int");

                    b.Property<string>("Gifts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoShowed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KompuCompleted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("KompuLastTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<int>("KompuSpells")
                        .HasColumnType("int");

                    b.Property<string>("KompuSteps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpgrades")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Magics")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mana")
                        .HasColumnType("int");

                    b.Property<int>("MonsterNestActive")
                        .HasColumnType("int");

                    b.Property<int>("MonsterNumber")
                        .HasColumnType("int");

                    b.Property<string>("NeighborAssists")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Potion")
                        .HasColumnType("int");

                    b.Property<string>("QuestsRank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RewardedMissions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RiderNumber")
                        .HasColumnType("int");

                    b.Property<int>("RiderStepNumber")
                        .HasColumnType("int");

                    b.Property<long>("RiderTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<int>("StepMonsterNumber")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.Property<int>("Strategy")
                        .HasColumnType("int");

                    b.Property<string>("Teams")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TimeStampDartsNewFree")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeStampDartsReset")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeStampHeavySiegeAttack")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeStampHeavySiegePeriod")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeStampTakeCare")
                        .HasColumnType("bigint");

                    b.Property<long>("TimeStampTakeCareMonster")
                        .HasColumnType("bigint");

                    b.Property<long>("TimestampLastBonus")
                        .HasColumnType("bigint");

                    b.Property<string>("UnitCollectionsCompleted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnlockedEarlyBuildings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnlockedQuestIndex")
                        .HasColumnType("int");

                    b.Property<string>("UnlockedSkins")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Pid");

                    b.ToTable("PlayerStates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.EmpireMap", b =>
                {
                    b.HasOne("SocialEmpires.Models.PlayerSaves.PlayerSave", null)
                        .WithMany("Maps")
                        .HasForeignKey("PlayerSavePid");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.PlayerSave", b =>
                {
                    b.HasOne("SocialEmpires.Models.PlayerSaves.PlayerInfo", "PlayerInfo")
                        .WithMany()
                        .HasForeignKey("PlayerInfoPid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialEmpires.Models.PlayerSaves.PlayerState", "PrivateState")
                        .WithMany()
                        .HasForeignKey("PrivateStatePid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerInfo");

                    b.Navigation("PrivateState");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSaves.PlayerSave", b =>
                {
                    b.Navigation("Maps");
                });
#pragma warning restore 612, 618
        }
    }
}
