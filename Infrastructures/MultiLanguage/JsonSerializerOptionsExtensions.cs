using System.Globalization;
using System.Text.Json;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public static class JsonSerializerOptionsExtensions
    {
        public static JsonSerializerOptions WithLanguage(this JsonSerializerOptions options, string language)
        {
            SupportLanguages.ThrowIfUnsupported(language);
            options.Converters.Add(new MultiLanguageStringConverter(language));
            return options;
        }

        public static JsonSerializerOptions WithLanguage(this JsonSerializerOptions options)
        {
            SupportLanguages.ThrowIfUnsupported(CultureInfo.CurrentCulture.Name);
            options.Converters.Add(new MultiLanguageStringConverter(CultureInfo.CurrentCulture.Name));
            return options;
        }
    }
}
