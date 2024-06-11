using SocialEmpires.Models;
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

        public ConfigFileService(ILogger<ConfigFileService> logger)
        {
            _logger = logger;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };
        }

        public async Task<IEnumerable<Item>?> GetAllItemsAsync(string language) 
        {
            using (var reader = new StreamReader(File.OpenRead(zhConfigFile)))
            {
                try
                {
                    var config = await JsonNode.ParseAsync(reader.BaseStream);
                    if (config == null) 
                    {
                        throw new FileNotFoundException(); 
                    }

                    var items = JsonSerializer.Deserialize<IEnumerable<Item>>(
                        config["items"], 
                        jsonSerializerOptions);

                    if (items == null)
                    {
                        throw new InvalidDataException();
                    }
                    return items;
                }
                catch(Exception ex) 
                {
                    _logger.LogWarning(ex.Message);
                    return null;
                }
            }
        }
    }
}
