using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class StopConditionConverter : BaseConverter<KucoinStopCondition>
    {
        public StopConditionConverter() : this(true) { }
        public StopConditionConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinStopCondition, string>> Mapping => new List<KeyValuePair<KucoinStopCondition, string>>
        {
            new KeyValuePair<KucoinStopCondition, string>(KucoinStopCondition.None, ""),
            new KeyValuePair<KucoinStopCondition, string>(KucoinStopCondition.Entry, "entry"),
            new KeyValuePair<KucoinStopCondition, string>(KucoinStopCondition.Entry, "up"),
            new KeyValuePair<KucoinStopCondition, string>(KucoinStopCondition.Loss, "loss"),
            new KeyValuePair<KucoinStopCondition, string>(KucoinStopCondition.Loss, "down"),
        };
    }
}
