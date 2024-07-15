using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialEmpires.Models.Bulletins;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.PlayerSaves;
using SocialEmpires.Utils;
using System.Text.Json;

namespace SocialEmpires.Models
{
    public class AppDbContext : IdentityDbContext
    {
        //Player
        public DbSet<PlayerSave> PlayerSaves { get; set; }
        public DbSet<EmpireMap> EmpireMaps { get; set; }
        public DbSet<PlayerState> PlayerStates { get; set; }
        public DbSet<PlayerInfo> PlayerInfos { get; set; }

        //Bulletin
        public DbSet<Bulletin> Bulletins { get; set; }

        //Config
        public DbSet<Item> Items { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<ExpansionPrice> ExpansionPrices { get; set; }
        public DbSet<FindableItem> FindableItems { get; set; }
        public DbSet<LocalizationString> LocalizationStrings { get; set; }
        public DbSet<NeighborAssist> NeighborAssists { get; set; }
        public DbSet<HonorLevel> HonorLevels { get; set; }
        public DbSet<OfferPack> OfferPacks { get; set; }
        public DbSet<Magic> Magics { get; set; }
        public DbSet<MapPrice> MapPrices {  get; set; }
        public DbSet<TownPrice> TownPrices { get; set; }
        public DbSet<SocialItem> SocialItems { get; set; }
        public DbSet<DartsItem> DartsItems { get; set; }
        public DbSet<LevelRankingReward> LevelRankingRewards { get; set; }
        public DbSet<Chore> Chores {  get; set; }

        public DbSet<UnitsCollectionsCategory> UnitsCollectionsCategories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var jsonSerializeOptions = new JsonSerializerOptions();

            var mapItemListConverter = new ValueConverter<List<MapItem>, string>(
                v => JsonSerializer.Serialize(
                    v, jsonSerializeOptions),
                v => JsonSerializer.Deserialize<List<MapItem>>(
                    v, jsonSerializeOptions)!
            );

            var dictionaryComparer = new ValueComparer<Dictionary<string, int>>(
                (c1, c2) => c1.NullRespectingSequenceEqual(c2),
                c => c.Aggregate(0, (a, p) => HashCode.Combine(a, p.GetHashCode())),
                c => c.ToDictionary(e => e.Key, e => e.Value)
            );

            var mapItemListComparer = new ValueComparer<List<MapItem>>(
                (list1, list2) => list1.NullRespectingSequenceEqual(list2),
                list => list.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                list => list.ToList());

            builder.Entity<PlayerSave>().HasKey(x => x.Pid);

            builder.Entity<EmpireMap>().HasKey(x => x.Pid);
            builder.Entity<EmpireMap>()
                .Property(_ => _.Items)
                .HasConversion(
                v => JsonSerializer.Serialize(v, jsonSerializeOptions),
                v => JsonSerializer.Deserialize<List<MapItem>>(v, jsonSerializeOptions)!,
                new ValueComparer<List<MapItem>>(
                    (list1, list2) => list1.NullRespectingSequenceEqual(list2),
                    list => list.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                    list => list.Select(_ => new MapItem(_.Id, _.X, _.Y, _.Orientation, _.Timestamp, _.Level, _.Units, _.Attributes)).ToList()));

            builder.Entity<EmpireMap>().Property(_ => _.ExpirableUnitsTime)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<EmpireMap>().Property(_ => _.ReceivedAssists)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<EmpireMap>().Property(_ => _.ResourcesTraded)
                .Metadata.SetValueComparer(dictionaryComparer);

            builder.Entity<PlayerState>().HasKey(x => x.Pid);
            builder.Entity<PlayerState>().Property(_ => _.ArrayAnimals)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.Magics)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.NeighborAssists)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.QuestsRank)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.Teams)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.UnlockedEarlyBuildings)
                .Metadata.SetValueComparer(dictionaryComparer);
            builder.Entity<PlayerState>().Property(_ => _.UnlockedSkins)
                .Metadata.SetValueComparer(dictionaryComparer);

            builder.Entity<PlayerInfo>().HasKey(x => x.Pid);

            builder.Entity<Bulletin>().HasKey(x => x.Id);

            builder.Entity<Item>().HasKey(x => x.Id);

            builder.Entity<Level>().HasKey(x => x.Id);

            builder.Entity<Mission>().HasKey(x => x.Id);
            builder.Entity<Mission>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<ExpansionPrice>().HasKey(x => x.Id);

            builder.Entity<FindableItem>().HasKey(x => x.Id);
            builder.Entity<FindableItem>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<LocalizationString>().HasKey(x => x.Id);
            builder.Entity<LocalizationString>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<NeighborAssist>().HasKey(x => x.Id);

            builder.Entity<HonorLevel>().HasKey(x => x.Id);
            builder.Entity<HonorLevel>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<OfferPack>().HasKey(x => x.Id);
            builder.Entity<OfferPack>().Property(x => x.Id).ValueGeneratedNever();

            var OfferPackJsonSerializeOptions = new JsonSerializerOptions();
            OfferPackJsonSerializeOptions.Converters.Add(new IntListOrIntListListConverter());
            builder.Entity<OfferPack>()
                .Property(x => x.Items)
                .HasConversion(
                v => JsonSerializer.Serialize(v, OfferPackJsonSerializeOptions),
                v => JsonSerializer.Deserialize<List<object>>(v, OfferPackJsonSerializeOptions)!,
                new ValueComparer<List<object>>(
                    (list1, list2) => list1.NullRespectingSequenceEqual(list2),
                    list => list.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                    list => new List<object>(list))
                );

            builder.Entity<Magic>().HasKey(x => x.Id);
            builder.Entity<Magic>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<MapPrice>().HasKey(x => x.Id);

            builder.Entity<TownPrice>().HasKey(x => x.Id);

            builder.Entity<SocialItem>().HasKey(x => x.Id);
            builder.Entity<SocialItem>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<DartsItem>().HasKey(x => x.Id);
            builder.Entity<DartsItem>().Property(x => x.Id).ValueGeneratedNever();

            builder.Entity<LevelRankingReward>().HasKey(x => x.Id);
            builder.Entity<LevelRankingReward>().Property(_ => _.Units)
                .Metadata.SetValueComparer(dictionaryComparer);

            builder.Entity<Chore>().HasKey(x => x.Id);

            builder.Entity<UnitsCollectionsCategory>().HasKey(x => x.Id);
            builder.Entity<UnitsCollectionsCategory>().Property(x => x.Id).ValueGeneratedNever();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder
                .Properties<Dictionary<string, int>>()
                .HaveConversion<DictionaryConverter>();
        }

        public class DictionaryConverter : ValueConverter<Dictionary<string, int>, string>
        {
            private static readonly JsonSerializerOptions jsonSerializeOptions = new();

            public DictionaryConverter() : base(
                v => JsonSerializer.Serialize(
                    v, jsonSerializeOptions),
                v => JsonSerializer.Deserialize<Dictionary<string, int>>(
                    v, jsonSerializeOptions)!)
            {

            }
        }
    }
}
