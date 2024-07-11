namespace SocialEmpires.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class ListOfIntConverter : JsonConverter<List<object>>
    {
        public override List<object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                var root = jsonDoc.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    var jsonArray = root.EnumerateArray();

                    List<object> resultList = new List<object>();

                    foreach (var item in jsonArray)
                    {
                        if (item.ValueKind == JsonValueKind.Array)
                        {
                            var nestedList = ParseJsonArray<List<int>>(item, options);
                            resultList.Add(nestedList);
                        }
                        else if (item.ValueKind == JsonValueKind.Number)
                        {
                            var flatList = new List<int> { item.GetInt32() };
                            resultList.Add(flatList);
                        }
                        else
                        {
                            throw new JsonException("Unexpected JSON token type");
                        }
                    }

                    return resultList;
                }
                else
                {
                    throw new JsonException("Expected JSON array");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, List<object> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                if (item is List<int> intList)
                {
                    writer.WriteStartArray();

                    foreach (var num in intList)
                    {
                        writer.WriteNumberValue(num);
                    }

                    writer.WriteEndArray();
                }
                else
                {
                    throw new JsonException("Unexpected item type in List<object>");
                }
            }

            writer.WriteEndArray();
        }

        private T ParseJsonArray<T>(JsonElement element, JsonSerializerOptions options)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }

}
