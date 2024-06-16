using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace SocialEmpires.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<PlayerSave> PlayerSaves { get; set; }

        public DbSet<EmpireMap> EmpireMaps { get; set; }

        public DbSet<PlayerState> PlayerStates { get; set; }

        public DbSet<PlayerInfo> PlayerInfos { get; set; }

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

            var mapItemListConverter = new ValueConverter<List<MapItem>, string>(
                v => JsonSerializer.Serialize(
                    v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<MapItem>>(
                    v, new JsonSerializerOptions())!
            );

            var dictionaryComparer = new ValueComparer<Dictionary<string, int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, p) => HashCode.Combine(a, p.GetHashCode())),
                c => c.ToDictionary(e => e.Key, e => e.Value)
            );

            var mapItemListComparer = new ValueComparer<List<MapItem>>(
                (list1, list2) => list1.SequenceEqual(list2),
                list => list.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                list => list.ToList());

            builder.Entity<PlayerSave>().HasKey(x => x.Pid);

            builder.Entity<EmpireMap>().HasKey(x => x.Pid);
            builder.Entity<EmpireMap>().Property(_ => _.Items)
                .HasConversion(mapItemListConverter)
                .Metadata.SetValueComparer(mapItemListComparer);

            builder.Entity<PlayerState>().HasKey(x => x.Pid);

            builder.Entity<PlayerInfo>().HasKey(x => x.Pid);


        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<Dictionary<string, int>>().HaveConversion<DictionaryConverter>();
        }

        public class DictionaryConverter : ValueConverter<Dictionary<string, int>, string>
        {
            public DictionaryConverter(): base(
                v => JsonSerializer.Serialize(
                    v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<Dictionary<string, int>>(
                    v, new JsonSerializerOptions())!)
            {

            }
        }
    }
}
