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
        private readonly JsonNode _config;
        private IEnumerable<Item>? configItems; 

        public ConfigFileService(ILogger<ConfigFileService> logger)
        {
            _logger = logger;

            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            using (var reader = new StreamReader(File.OpenRead(zhConfigFile)))
            {
                _config = JsonNode.Parse(reader.BaseStream)??throw new InvalidOperationException();
            }
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
                        return NullPage<Item>();
                    }
                }
            }
            return Page(pageIndex,pageSize,configItems);
        }

        private (int pageCount, IEnumerable<T>? data) NullPage<T>()
        {
            return (0, null);
        }

        private (int pageCount, IEnumerable<T>? data) Page<T>(int pageIndex, int pageSize, IEnumerable<T> data)
        {
            var pageCount = ((int)Math.Ceiling((double)data.Count() / pageSize));
            var items = data.Skip(pageIndex * pageSize).Take(pageSize);
            return (pageCount, items);
        }
    }
}
