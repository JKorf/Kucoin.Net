using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Kucoin.Net.Objects.Models.Spot;

namespace Kucoin.Net.Converters
{
    internal class AccountActivityContextConverter : JsonConverter<KucoinAccountActivityContext>
    {
#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
#endif
        public override KucoinAccountActivityContext? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return new KucoinAccountActivityContext();

            var str = reader.GetString()!;
            var doc = JsonDocument.Parse(str);
            return doc.Deserialize<KucoinAccountActivityContext>(options);
        }

        public override void Write(Utf8JsonWriter writer, KucoinAccountActivityContext value, JsonSerializerOptions options)
        {
            if (value == null)
                writer.WriteNullValue();
            else
                JsonSerializer.Serialize(writer, value, options);
        }
    }
}
