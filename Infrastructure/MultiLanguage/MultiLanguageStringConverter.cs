using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Infrastructure.MultiLanguage
{
    public class MultiLanguageStringConverter : JsonConverter<MultiLanguageString>
    {
        public override MultiLanguageString? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                if(reader.TokenType == JsonTokenType.String)
                {
                    return new MultiLanguageString(SupportLanguages.Default, reader.GetString()) { Current = SupportLanguages.Default };
                }
                else
                {
                    throw new JsonException();
                }
            }

            string? en = null;
            string? zh = null;
            //add more languages...

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString() ?? throw new JsonException();
                reader.Read();

                switch (propertyName)
                {
                    case nameof(SupportLanguages.En):
                        en = reader.GetString();
                        break;
                    case nameof(SupportLanguages.Zh):
                        zh = reader.GetString();
                        break;
                    //add more languages...
                    default:
                        reader.Skip();
                        break;
                }
            }

            var result = new MultiLanguageString();
            result.Set(SupportLanguages.En, en);
            result.Set(SupportLanguages.Zh, zh);
            //add more languages...
            return result;
        }

        public override void Write(Utf8JsonWriter writer, MultiLanguageString value, JsonSerializerOptions options)
        {
            if (value.Current != null)
            {
                writer.WriteStringValue(value.GetCurrent());
            }
            else
            {
                writer.WriteStartObject();

                writer.WriteString(nameof(value.Zh), value.Zh);
                writer.WriteString(nameof(value.En), value.En);
                //Add more languages...

                writer.WriteEndObject();
            }
        }
    }
}
