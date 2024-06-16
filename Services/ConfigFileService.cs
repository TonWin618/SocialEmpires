using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Services
{
    public class ConfigFileService
    {
        private const string enConfigFile = "Assets/config/game_config_en.json";
        private const string zhConfigFile = "Assets/config/game_config_zh.json";

        private readonly ILogger<ConfigFileService> _logger;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly JsonNode _config;
        public IEnumerable<Item>? Items;
        private IEnumerable<Mission> configMissions;
        private IEnumerable<Level> configLevels;
        public List<ExpansionPrice> ExpansionPrices { get; private set; }

        public ConfigFileService(ILogger<ConfigFileService> logger)
        {
            _logger = logger;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            };

            using (var stream = File.OpenRead(zhConfigFile))
            {
                _config = JsonNode.Parse(stream)??throw new InvalidOperationException();
            }
        }

        public async Task Save()
        {
            var jsonString = JsonSerializer.Serialize(Items, jsonSerializerOptions);
            _config["items"] = JsonNode.Parse(jsonString);
            await File.WriteAllTextAsync(zhConfigFile, _config.ToJsonString(jsonSerializerOptions));
        }

        #region Items
        public async Task<Item?> GetItem(int id)
        {
            return await GetItem(id.ToString());
        }

        public async Task<Item?> GetItem(string id)
        {
            return Items?.FirstOrDefault(_ => _.Id == id);
        }

        public Task<(int pageCount,IEnumerable<Item>? items)> GetItems(int pageIndex, int pageSize) 
        {
            return Task.FromResult(PageHelper.Page(pageIndex,pageSize, Items));
        }

        public void LoadItems()
        {
            try
            {
                Items = JsonSerializer.Deserialize<IEnumerable<Item>>(
                    _config["items"],
                    jsonSerializerOptions);

                if (Items == null)
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
        #endregion

        #region Missions
        public Task<Mission> GetMission(int missionId)
        {
            var mission = configMissions.FirstOrDefault(_ => _.Id == missionId);
            return Task.FromResult(mission);
        }

        public void LoadMissions()
        {
            try
            {
                configMissions = JsonSerializer.Deserialize<IEnumerable<Mission>>(
                    _config["missions"],
                    jsonSerializerOptions);

                if (Items == null)
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
        #endregion

        #region Levels
        public Task<IEnumerable<Level>> GetLevels()
        {
            return Task.FromResult(configLevels);
        }

        public Task<Level> GetLevel(int levelId)
        {
            var index = Math.Max(0, levelId - 1);
            var level = configLevels.ElementAtOrDefault(index);
            return Task.FromResult(level);
        }

        public void LoadLevels()
        {
            try
            {
                configLevels = JsonSerializer.Deserialize<IEnumerable<Level>>(
                    _config["levels"],
                    jsonSerializerOptions);

                if (Items == null)
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
        #endregion

        #region Expansion Price
        public void LoadExpansionPrices()
        {
            try
            {
                ExpansionPrices = JsonSerializer.Deserialize<List<ExpansionPrice>>(
                    _config["levels"],
                    jsonSerializerOptions);

                if (Items == null)
                {
                    throw new InvalidDataException();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
        #endregion
    }
}
