using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Utils
{
    public class StringIntDictionaryConverter : JsonConverter<Dictionary<string, int>>
    {
        public override Dictionary<string, int>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            var dictionary = new Dictionary<string, int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                string key = reader.GetString();

                reader.Read();

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException("Expected Number token");
                }

                int value = reader.GetInt32();

                dictionary.Add(key, value);
            }

            throw new JsonException("Expected EndObject token");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, int> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var kvp in value)
            {
                writer.WriteNumber(kvp.Key, kvp.Value);
            }

            writer.WriteEndObject();
        }
    }
}
