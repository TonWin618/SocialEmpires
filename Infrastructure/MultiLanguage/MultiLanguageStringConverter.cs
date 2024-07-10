using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageStringConverter : JsonConverter<MultiLanguageString>
    {
        private readonly string _language;

        public MultiLanguageStringConverter():this(CultureInfo.CurrentCulture.Name)
        {
        }

        public MultiLanguageStringConverter(string language)
        {
            SupportLanguages.ThrowIfUnsupported(language);
            _language = language;
        }

        public override MultiLanguageString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var result = new MultiLanguageString();

            result.Set(_language, reader.GetString());
            return result;
        }

        public override void Write(Utf8JsonWriter writer, MultiLanguageString value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Get(_language));
        }
    }
}
