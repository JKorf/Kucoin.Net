using System;
using System.IO;
using Kucoin.Net.Objects.Models.Spot;
using Newtonsoft.Json;

namespace Kucoin.Net.Converters
{
    internal class HexStringConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if ((reader.Value == null) || ((string)reader.Value == ""))
                return "\"\"";

            var obj = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader("\"" + reader.Value.ToString() + "\"")), typeof(string));
            if (obj == null)
                return "\"\"";
            else
                return "\"" + obj + "\"";
        }

        public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
        {
            if (value == null)
                serializer.Serialize(writer, "");
            else
                serializer.Serialize(writer, value);
        }
    }
    //internal class AccountActivityContextConverter : JsonConverter<KucoinAccountActivityContext>
    //{
    //    public override KucoinAccountActivityContext ReadJson(JsonReader reader, Type objectType, KucoinAccountActivityContext? existingValue, bool hasExistingValue, JsonSerializer serializer)
    //    {
    //        //if (reader.Value == null)
    //        //    return new KucoinAccountActivityContext();

    //        //if ((string)reader.Value == "")
    //        //    return new KucoinAccountActivityContext();

    //        //if (!((string)reader.Value).Contains("{"))
    //        //    return new KucoinAccountActivityContext((string)reader.Value);

    //        try
    //        {
    //            var obj = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(reader.Value.ToString())), typeof(KucoinAccountActivityContext));
    //            if (obj == null)
    //                return new KucoinAccountActivityContext();
    //            else
    //                return (KucoinAccountActivityContext)obj;
    //        }
    //        catch (Exception)
    //        {
    //            System.Diagnostics.Debugger.Break();
    //            throw;
    //        }
    //    }

    //    public override void WriteJson(JsonWriter writer, KucoinAccountActivityContext? value, JsonSerializer serializer)
    //    {
    //        if (value == null) return;

    //        serializer.Serialize(writer, value);
    //    }
    //}
}
