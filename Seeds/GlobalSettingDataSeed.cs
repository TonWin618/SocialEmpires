using AutoMapper;
using SocialEmpires.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class GlobalSettingDataSeed :IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GlobalSettingDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.GlobalSettings.Any())
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
            using (var stream = File.OpenRead("./Seeds/game_config_en.json"))
            {
                config = JsonNode.Parse(stream) ?? throw new InvalidOperationException();
            }

            var globalSettingEntities = new List<GlobalSetting>();
            var globalSettings = config["globals"].AsObject();
            foreach (var setting in globalSettings)
            {
                globalSettingEntities.Add(new GlobalSetting() { Key = setting.Key, Value = setting.Value.ToJsonString() });
            }

            _appDbContext.AddRange(globalSettingEntities);
            _appDbContext.SaveChanges();
        }
    }
}
