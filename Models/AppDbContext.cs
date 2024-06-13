using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var mapItemConverter = new ValueConverter<List<MapItem>, string>(
                v => JsonSerializer.Serialize(
                    v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<MapItem>>(
                    v, new JsonSerializerOptions())!
            );

            builder.Entity<PlayerSave>().HasKey(x => x.Pid);

            builder.Entity<EmpireMap>().HasKey(x => x.Pid);
            builder.Entity<EmpireMap>().Property(_ => _.Items)
                .HasConversion(mapItemConverter);

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
