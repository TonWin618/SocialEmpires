using SocialEmpires.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Utils
{
    public class MultiLanguageStringConverter : JsonConverter<MultiLanguageString>
    {
        private readonly string _acceptLanguage;

        public MultiLanguageStringConverter(IHttpContextAccessor accessor)
        {
            _acceptLanguage = accessor.HttpContext.Request.Headers.AcceptLanguage;
        }

        public override MultiLanguageString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }

        public override void Write(Utf8JsonWriter writer, MultiLanguageString value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Get(_acceptLanguage));
        }
    }
}
