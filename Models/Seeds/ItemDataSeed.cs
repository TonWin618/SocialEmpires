using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Models.Seeds
{
    public class ItemDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public ItemDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Items.Any())
            {
                return;
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            }.WithLanguage("en");

            JsonNode config;
            using (var stream = File.OpenRead("./Models/Seeds/game_config_en.json"))
            {
                config = JsonNode.Parse(stream) ?? throw new InvalidOperationException();
            }
            var itemDtos = JsonSerializer.Deserialize<List<ItemDto>>(config["items"], jsonSerializerOptions);

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemDto, Item>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
                    .ForMember(dest => dest.Xp, opt => opt.MapFrom(src => int.Parse(src.Xp)));
            }).CreateMapper();

            var items = itemDtos!.Select(mapper.Map<Item>);

            _appDbContext.AddRange(items);
            _appDbContext.SaveChanges();
        }

        private record ItemDto(string Id, string InStore, MultiLanguageString Name, string Type,
            string Cost, string CostType, string Xp, string? Groups,
            string Trains, string UpgradesTo, string DisplayOrder,
            string Activation, string Expiration,
            string Collect, string CollectType, string CollectXp,
            string CategoryId, string SubcategoryId, string SubcatFunctional,
            string MinLevel, string Width, string Height, string MaxFrame,
            string Giftable, string ImgName, string Elevation, string UnitCapacity,
            string Attack, string Defense, string Life, string Velocity, string AttackRange, string AttackInterval,
            string NewItem, string Population, string GiftLevel, string CostUnitCash, string Race, string Flying,
            string Protect, string Potion, string Achievement, MultiLanguageString AchievementDesc, string UnitsLimit,
            string? StoreGroups, string StoreLevel, string Size,
            string ShowOnMobile, string ShowOnMobileStore, string OnlyMobile, string? IphoneAdjustments);
    }
}
