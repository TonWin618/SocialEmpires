using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json;

namespace SocialEmpires.Services
{
    public class ConfigService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ConfigService> _logger;

        public List<LocalizationString> LocalizationStrings { get; private set; }
        public JsonElement Categories { get; private set; }
        public List<Item> Items { get; private set; }
        public List<ExpansionPrice> ExpansionPrices { get; private set; }
        public List<HonorLevel> HonorLevels { get; private set; }
        public List<Level> Levels { get; private set; }
        public List<NeighborAssist> NeighborAssists { get; private set; }
        public List<TownPrice> TownPrices { get; private set; }
        public List<MapPrice> MapPrices { get; private set; }
        public List<FindableItem> FindableItems { get; private set; }
        public List<Mission> Missions { get; private set; }
        public List<OfferPack> OfferPacks { get; private set; }
        public JsonElement Images { get; private set; }
        public List<SocialItem> SocialItems { get; private set; }
        public JsonElement Globals { get; private set; }
        public List<Magic> Magics { get; private set; }
        public JsonElement UnitsCollectionsCategories { get; private set; }
        public List<LevelRankingReward> LevelRankingReward { get; private set; }
        public JsonElement TournamentType { get; private set; }
        public List<DartsItem> DartsItems { get; private set; }

        public ConfigService(
            AppDbContext appDbContext,
            ILogger<ConfigService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            Load();
        }

        public void Load()
        {
            var language = "en"; //TODO: 
            LocalizationStrings = _appDbContext.LocalizationStrings.WithLanguage(language).ToList();
            Categories = JsonDocument.Parse(_appDbContext.Chores.First().Categories.en).RootElement;
            Items = _appDbContext.Items.WithLanguage(language).ToList();
            ExpansionPrices = _appDbContext.ExpansionPrices.WithLanguage(language).ToList();
            Levels = _appDbContext.Levels.WithLanguage(language).ToList();
            HonorLevels = _appDbContext.HonorLevels.WithLanguage(language).ToList();
            NeighborAssists = _appDbContext.NeighborAssists.WithLanguage(language).ToList();
            TownPrices = _appDbContext.TownPrices.WithLanguage(language).ToList();
            MapPrices = _appDbContext.MapPrices.WithLanguage(language).ToList();
            FindableItems = _appDbContext.FindableItems.WithLanguage(language).ToList();
            Missions = _appDbContext.Missions.WithLanguage(language).ToList();
            OfferPacks = _appDbContext.OfferPacks.WithLanguage(language).ToList();
            Images = JsonDocument.Parse(_appDbContext.Chores.First().Images).RootElement;
            SocialItems = _appDbContext.SocialItems.WithLanguage(language).ToList();
            Globals = JsonDocument.Parse(_appDbContext.Chores.First().Globals).RootElement;
            Magics = _appDbContext.Magics.WithLanguage(language).ToList();
            UnitsCollectionsCategories = JsonDocument.Parse(_appDbContext.Chores.First().UnitsCollectionsCategories).RootElement;
            LevelRankingReward = _appDbContext.LevelRankingRewards.WithLanguage(language).ToList();
            TournamentType = JsonDocument.Parse(_appDbContext.Chores.First().TournamentType).RootElement;
            DartsItems = _appDbContext.DartsItems.WithLanguage(language).ToList();
        }

        public Item? GetItem(int id)
        {
            return GetItem(id.ToString());
        }

        public Item? GetItem(string id)
        {
            return Items?.FirstOrDefault(_ => _.Id == int.Parse(id));
        }

        public (int pageCount, IEnumerable<Item>? items) GetItems(int pageIndex, int pageSize)
        {
            return PageHelper.Page(pageIndex, pageSize, Items);
        }
    }
}
