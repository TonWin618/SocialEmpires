﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialEmpires.Utils
{
    public class BoolToStringConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value == "1";
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? "1" : "0");
        }
    }
}
