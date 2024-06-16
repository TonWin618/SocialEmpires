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

        public List<Item> Items { get; private set; }
        public List<Mission> Missions { get; private set; }
        public List<Level> Levels { get; private set; }
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

        public void Load()
        {
            Items = Load<Item>("items");
            Missions = Load<Mission>("missions");
            Levels = Load<Level>("levels");
            ExpansionPrices = Load<ExpansionPrice>("expansion_prices");
        }

        private List<T> Load<T>(string key)
        {
            var result = JsonSerializer.Deserialize<List<T>>(
                    _config[key],
                    jsonSerializerOptions);
            if(result == null)
            {
                _logger.LogError($"Error reading JSON game configuration file with Key [{key}].");
                throw new InvalidOperationException();
            }
            return result;
        }

        public async Task Save()
        {
            var jsonString = JsonSerializer.Serialize(Items, jsonSerializerOptions);
            _config["items"] = JsonNode.Parse(jsonString);
            await File.WriteAllTextAsync(zhConfigFile, _config.ToJsonString(jsonSerializerOptions));
        }

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
    }
}
