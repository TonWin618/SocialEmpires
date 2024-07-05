using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageStringConverter : JsonConverter<MultiLanguageString>
    {
        public override MultiLanguageString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }

        public override void Write(Utf8JsonWriter writer, MultiLanguageString value, JsonSerializerOptions options)
        {
            int validContentCount = 0;

            if (value.Current != null)
            {
                writer.WriteStringValue(value.GetCurrent());
            }
            else
            {
                writer.WriteStartObject();

                writer.WriteString(SupportLanguages.Zh, value.Zh);
                writer.WriteString(SupportLanguages.En, value.En);
                //Add more languages...

                writer.WriteEndObject();
            }
        }
    }
}
