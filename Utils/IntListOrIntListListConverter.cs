namespace SocialEmpires.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class IntListOrIntListListConverter : JsonConverter<List<object>>
    {
        public override List<object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = new List<object>();

            if(reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                if(reader.TokenType == JsonTokenType.Number)
                {
                    do
                    {
                        result.Add(reader.GetInt32());
                        reader.Read();
                    } 
                    while (reader.TokenType == JsonTokenType.Number);
                }
                else if(reader.TokenType == JsonTokenType.StartArray)
                {
                    do
                    {
                        var intList = new List<int>();

                        reader.Read();

                        while (reader.TokenType == JsonTokenType.Number)
                        {
                            intList.Add(reader.GetInt32());
                            reader.Read();
                        }

                        result.Add(intList);
                        reader.Read();
                    } while (reader.TokenType != JsonTokenType.EndArray);
                }
                else
                {
                    throw new JsonException("Unexpected JSON token type");
                }
            }

            return result;
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
                else if(item is int number)
                {
                    writer.WriteNumberValue(number);
                }
                else
                {
                    throw new JsonException("Unexpected JSON token type");
                }
            }

            writer.WriteEndArray();
        }
    }

}
