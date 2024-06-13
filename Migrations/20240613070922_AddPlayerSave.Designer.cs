﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialEmpires.Models;

#nullable disable

namespace SocialEmpires.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240613070922_AddPlayerSave")]
    partial class AddPlayerSave
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SocialEmpires.Models.EmpireMap", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("TEXT");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Expansions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExpirableUnitsTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Food")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdCurrentTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IncreasedPopulation")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastQuestTimes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerSavePid")
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestTimes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceivedAssists")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourcesTraded")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Skin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stone")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Timestamp")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimestampLastTreasure")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UniversAttackWin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Wood")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Xp")
                        .HasColumnType("INTEGER");

                    b.HasKey("Pid");

                    b.HasIndex("PlayerSavePid");

                    b.ToTable("EmpireMaps");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerInfo", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("TEXT");

                    b.Property<int>("Cash")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompletedTutorial")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DefaultMap")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LastLoggedIn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MapNames")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MapSizes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WorldId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Pid");

                    b.ToTable("PlayerInfos");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSave", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerInfoPid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PrivateStatePid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Pid");

                    b.HasIndex("PlayerInfoPid");

                    b.HasIndex("PrivateStatePid");

                    b.ToTable("PlayerSaves");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerState", b =>
                {
                    b.Property<string>("Pid")
                        .HasColumnType("TEXT");

                    b.Property<string>("ArrayAnimals")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AttacksSent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BonusNextId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BoughtUnits")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CompletedMissions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CountTimePacket")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DartsBalloonsShot")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("DartsGotExtra")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DartsHasFree")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DartsRandomSeed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DragonNestActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DragonNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gifts")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("InfoShowed")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("KompuCompleted")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("KompuLastTimeStamp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("KompuSpells")
                        .HasColumnType("INTEGER");

                    b.Property<string>("KompuSteps")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpgrades")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Magics")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Mana")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MonsterNestActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MonsterNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NeighborAssists")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Potion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestsRank")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RewardedMissions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RiderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RiderStepNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long>("RiderTimeStamp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StepMonsterNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StepNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Strategy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Teams")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("TimeStampDartsNewFree")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStampDartsReset")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStampHeavySiegeAttack")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStampHeavySiegePeriod")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStampTakeCare")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimeStampTakeCareMonster")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TimestampLastBonus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnitCollectionsCompleted")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UnlockedEarlyBuildings")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnlockedQuestIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnlockedSkins")
                        .IsRequired()
                        .HasColumnType("TEXT");

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

            modelBuilder.Entity("SocialEmpires.Models.EmpireMap", b =>
                {
                    b.HasOne("SocialEmpires.Models.PlayerSave", null)
                        .WithMany("Maps")
                        .HasForeignKey("PlayerSavePid");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSave", b =>
                {
                    b.HasOne("SocialEmpires.Models.PlayerInfo", "PlayerInfo")
                        .WithMany()
                        .HasForeignKey("PlayerInfoPid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialEmpires.Models.PlayerState", "PrivateState")
                        .WithMany()
                        .HasForeignKey("PrivateStatePid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerInfo");

                    b.Navigation("PrivateState");
                });

            modelBuilder.Entity("SocialEmpires.Models.PlayerSave", b =>
                {
                    b.Navigation("Maps");
                });
#pragma warning restore 612, 618
        }
    }
}
