using AutoMapper;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using SocialEmpires.Infrastructure.MultiLanguage;

namespace SocialEmpires.Seeds
{
    public class ImageDataSeed: IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ImageDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.Images.Any())
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

            var imageEntities = new List<Image>();
            var images = config["images"].AsObject();
            foreach(var image in images)
            {
                imageEntities.Add(new Image() { Key = image.Key, Value = (string?)image.Value });
            }
            
            _appDbContext.AddRange(imageEntities);
            _appDbContext.SaveChanges();
        }
    }
}
