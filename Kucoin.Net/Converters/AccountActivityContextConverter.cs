using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Converters
{
    internal class AccountActivityContextConverter : JsonConverter<KucoinAccountActivityContext>
    {
        public override KucoinAccountActivityContext? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return new KucoinAccountActivityContext();

            var str = reader.GetString()!;
            if (string.IsNullOrEmpty(str))
                return new KucoinAccountActivityContext();

            var doc = JsonDocument.Parse(str);
            return doc.Deserialize<KucoinAccountActivityContext>((JsonTypeInfo<KucoinAccountActivityContext>)options.GetTypeInfo(typeof(KucoinAccountActivityContext)));
        }

        public override void Write(Utf8JsonWriter writer, KucoinAccountActivityContext value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
                JsonSerializer.Serialize(writer, value, (JsonTypeInfo<KucoinAccountActivityContext>)options.GetTypeInfo(typeof(KucoinAccountActivityContext)));
        }
    }
}
