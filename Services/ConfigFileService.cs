using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

namespace SocialEmpires.Services
{
    public class ConfigFileService
    {
        private const string enConfigFile = "Assets/config/game_config_en.json";
        private const string zhConfigFile = "Assets/config/game_config_zh.json";

        private readonly ILogger<ConfigFileService> _logger;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly JsonNode _config;
        private IEnumerable<Item>? configItems; 

        public ConfigFileService(ILogger<ConfigFileService> logger)
        {
            _logger = logger;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            };

            using (var reader = new StreamReader(File.OpenRead(zhConfigFile)))
            {
                _config = JsonNode.Parse(reader.BaseStream)??throw new InvalidOperationException();
            }
        }

        public async Task Save()
        {
            var jsonString = JsonSerializer.Serialize(configItems, jsonSerializerOptions);
            _config["items"] = JsonNode.Parse(jsonString);
            await File.WriteAllTextAsync(zhConfigFile, _config.ToJsonString(jsonSerializerOptions));
        }

        public async Task<Item?> GetItemAsync(string id)
        {
            return configItems?.FirstOrDefault(_ => _.Id == id);
        }

        public async Task<(int pageCount,IEnumerable<Item>? items)> GetItemsAsync(int pageIndex, int pageSize) 
        {
            if(configItems == null)
            {
                using (var reader = new StreamReader(File.OpenRead(zhConfigFile)))
                {
                    try
                    {
                        configItems = JsonSerializer.Deserialize<IEnumerable<Item>>(
                            _config["items"],
                            jsonSerializerOptions);

                        if (configItems == null)
                        {
                            throw new InvalidDataException();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex.Message);
                        return PageHelper.Page<Item>();
                    }
                }
            }
            return PageHelper.Page(pageIndex,pageSize,configItems);
        }
    }
}
