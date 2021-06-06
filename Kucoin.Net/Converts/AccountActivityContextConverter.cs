using Kucoin.Net.Objects;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Kucoin.Net.Converts
{
    internal class AccountActivityContextConverter : JsonConverter<KucoinAccountActivityContext>
    {
        public override KucoinAccountActivityContext ReadJson(JsonReader reader, Type objectType, KucoinAccountActivityContext? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return new KucoinAccountActivityContext();

            var obj = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(reader.Value.ToString())), typeof(KucoinAccountActivityContext));
            if (obj == null)
                return new KucoinAccountActivityContext();
            else
                return (KucoinAccountActivityContext)obj;
        }

        public override void WriteJson(JsonWriter writer, KucoinAccountActivityContext? value, JsonSerializer serializer)
        {
            if (value == null) return;

            serializer.Serialize(writer, value);
        }
    }
}
