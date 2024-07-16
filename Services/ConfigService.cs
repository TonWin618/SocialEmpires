using Microsoft.EntityFrameworkCore;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;

namespace SocialEmpires.Services
{
    public class ConfigService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ConfigService> _logger;

        public List<LocalizationString> LocalizationStrings { get; private set; }
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
        public List<SocialItem> SocialItems { get; private set; }
        public List<Magic> Magics { get; private set; }
        public List<LevelRankingReward> LevelRankingReward { get; private set; }
        public List<DartsItem> DartsItems { get; private set; }
        public List<GlobalSetting> GlobalSettings { get; private set; }
        public List<TournamentType> TournamentTypes { get; private set; }
        public List<Image> Images { get; private set; }
        public List<Category> Categories { get; private set; }
        public List<UnitsCollectionsCategory> UnitsCollectionsCategories { get; set; }

        public ConfigService(
            AppDbContext appDbContext,
            ILogger<ConfigService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;

            //load game config from database.
            LocalizationStrings = _appDbContext.LocalizationStrings.ToList();
            Items = _appDbContext.Items.ToList();
            ExpansionPrices = _appDbContext.ExpansionPrices.ToList();
            Levels = _appDbContext.Levels.ToList();
            HonorLevels = _appDbContext.HonorLevels.ToList();
            NeighborAssists = _appDbContext.NeighborAssists.ToList();
            TownPrices = _appDbContext.TownPrices.ToList();
            MapPrices = _appDbContext.MapPrices.ToList();
            FindableItems = _appDbContext.FindableItems.ToList();
            Missions = _appDbContext.Missions.ToList();
            OfferPacks = _appDbContext.OfferPacks.ToList();
            SocialItems = _appDbContext.SocialItems.ToList();
            Magics = _appDbContext.Magics.ToList();
            LevelRankingReward = _appDbContext.LevelRankingRewards.ToList();
            DartsItems = _appDbContext.DartsItems.ToList();

            GlobalSettings = _appDbContext.GlobalSettings.ToList();
            Categories = _appDbContext.Categories.Include(_ => _.Sub).ToList();
            UnitsCollectionsCategories = _appDbContext.UnitsCollectionsCategories.ToList();
            TournamentTypes = _appDbContext.TournamentTypes.Include(_ => _.WeeklyOpponent).Include(_=>_.Prize).ToList();
            Images = _appDbContext.Images.ToList();
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
