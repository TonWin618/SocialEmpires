using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Text.Json;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageJsonModelBinder : IModelBinder
    {
        private static readonly Dictionary<string, JsonSerializerOptions> jsonSerializeroptions = CreateJsonSerializerOptionsList();

        public static Dictionary<string, JsonSerializerOptions> CreateJsonSerializerOptionsList()
        {
            var jsonSerializeroptionsList = new Dictionary<string, JsonSerializerOptions>();
            foreach (var language in SupportLanguages.List)
            {
                jsonSerializeroptionsList.Add(language, 
                    new JsonSerializerOptions() 
                    {
                        
                    }
                    .WithLanguage(language));
            }
            return jsonSerializeroptionsList;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            request.EnableBuffering();
            request.Body.Position = 0;

            var result = await JsonSerializer.DeserializeAsync(
                request.Body, 
                bindingContext.ModelType,
                jsonSerializeroptions[CultureInfo.CurrentCulture.Name ?? SupportLanguages.Default]);

            bindingContext.Result = ModelBindingResult.Success(result);
        }
    }
}
