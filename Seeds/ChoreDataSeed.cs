using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Seeds
{
    public class ChoreDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public ChoreDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Chores.Any())
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

            var chore = new Chore();

            chore.Categories = new("en", config["categories"].ToJsonString(jsonSerializerOptions));
            chore.Images = config["images"].ToJsonString(jsonSerializerOptions);
            chore.Globals = config["globals"].ToJsonString(jsonSerializerOptions);
            chore.UnitsCollectionsCategories = config["units_collections_categories"].ToJsonString(jsonSerializerOptions);
            chore.TournamentType = config["tournament_type"].ToJsonString(jsonSerializerOptions);

            _appDbContext.Chores.Add(chore);
            _appDbContext.SaveChanges();
        }
    }
}
