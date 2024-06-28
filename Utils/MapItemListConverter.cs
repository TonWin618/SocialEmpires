using SocialEmpires.Models.PlayerSaves;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Utils
{
    public class MapItemListConverter : JsonConverter<List<MapItem>>
    {
        public override List<MapItem> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }

        public override void Write(Utf8JsonWriter writer, List<MapItem> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                writer.WriteStartArray();
                writer.WriteNumberValue(item.Id);
                writer.WriteNumberValue(item.X);
                writer.WriteNumberValue(item.Y);
                writer.WriteNumberValue(item.Orientation);
                writer.WriteNumberValue(item.Timestamp);
                writer.WriteNumberValue(item.Level);
                JsonSerializer.Serialize(writer, item.Units, options);
                writer.WriteStartObject();
                if (item.Attributes != null)
                {
                    foreach (var attribute in item.Attributes)
                    {
                        JsonSerializer.Serialize(writer, attribute, options);
                    }
                }
                writer.WriteEndObject();
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
        }
    }
}
