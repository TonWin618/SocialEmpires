using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Text.Json;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageJsonModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            request.EnableBuffering();

            try
            {
                var result = await JsonSerializer.DeserializeAsync(
                    request.Body, 
                    bindingContext.ModelType, 
                    new JsonSerializerOptions().WithLanguage(CultureInfo.CurrentCulture.Name));

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (Exception ex) 
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
