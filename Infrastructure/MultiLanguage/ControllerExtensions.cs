using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public static class ControllerExtensions
    {
        public static JsonResult JsonWithLanguage(this Controller controller, object? data, string language)
        {
            return controller.Json(data, new JsonSerializerOptions().WithLanguage(language));
        }

        public static JsonResult JsonWithLanguage(this Controller controller, object? data)
        {
            return controller.Json(data, new JsonSerializerOptions().WithLanguage(CultureInfo.CurrentCulture.Name));
        }
    }
}
