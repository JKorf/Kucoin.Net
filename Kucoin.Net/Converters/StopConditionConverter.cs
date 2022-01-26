using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Converters
{
    internal class StopConditionConverter : BaseConverter<StopCondition>
    {
        public StopConditionConverter() : this(true) { }
        public StopConditionConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<StopCondition, string>> Mapping => new List<KeyValuePair<StopCondition, string>>
        {
            new KeyValuePair<StopCondition, string>(StopCondition.None, ""),
            new KeyValuePair<StopCondition, string>(StopCondition.Entry, "entry"),
            new KeyValuePair<StopCondition, string>(StopCondition.Entry, "up"),
            new KeyValuePair<StopCondition, string>(StopCondition.Loss, "loss"),
            new KeyValuePair<StopCondition, string>(StopCondition.Loss, "down"),
        };

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            object? valueObject = base.ReadJson(reader, objectType, existingValue, serializer);
            if (valueObject == null)
                return StopCondition.None;
            return valueObject;
        }
    }
}
