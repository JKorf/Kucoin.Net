using System;
using System.IO;
using Kucoin.Net.Objects.Spot;
using Newtonsoft.Json;

#pragma warning disable CS8764
namespace Kucoin.Net.Converters
{
    internal class AccountActivityContextConverter : JsonConverter<KucoinAccountActivityContext>
    {

        public override KucoinAccountActivityContext? ReadJson(JsonReader reader, Type objectType, KucoinAccountActivityContext? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.TokenType switch
            {
                JsonToken.StartObject => (KucoinAccountActivityContext?)serializer!.Deserialize(reader, typeof(KucoinAccountActivityContext)),
                JsonToken.String => (KucoinAccountActivityContext?)new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(reader.Value!.ToString())), typeof(KucoinAccountActivityContext)),
                _ => null,
            };
        }

        public override void WriteJson(JsonWriter writer, KucoinAccountActivityContext? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
#pragma warning restore CS8764 
