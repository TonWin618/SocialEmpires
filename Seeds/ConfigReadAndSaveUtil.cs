using AutoMapper;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Seeds
{
    public static class ConfigReadAndSaveUtil
    {
        public static void ReadAndSave<TEntity, TDto>(string key, AppDbContext appDbContext, IMapper mapper)
        {
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
            var dtos = config[key].Deserialize<List<TDto>>(jsonSerializerOptions);

            var entities = dtos!.Select(mapper.Map<TDto, TEntity>).ToArray();

            foreach (var entity in entities)
            {
                appDbContext.Add(entity);
            }
            //appDbContext.AddRange(entities);
            appDbContext.SaveChanges();
        }
    }
}
