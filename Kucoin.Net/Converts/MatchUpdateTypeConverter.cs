using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    internal class MatchUpdateTypeConverter : BaseConverter<KucoinMatchUpdateType>
    {
        public MatchUpdateTypeConverter() : this(true) { }
        public MatchUpdateTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinMatchUpdateType, string>> Mapping => new List<KeyValuePair<KucoinMatchUpdateType, string>>
        {
            new KeyValuePair<KucoinMatchUpdateType, string>(KucoinMatchUpdateType.Match, "match"),
            new KeyValuePair<KucoinMatchUpdateType, string>(KucoinMatchUpdateType.Open, "open"),
            new KeyValuePair<KucoinMatchUpdateType, string>(KucoinMatchUpdateType.Canceled, "canceled"),
            new KeyValuePair<KucoinMatchUpdateType, string>(KucoinMatchUpdateType.Filled, "filled"),
            new KeyValuePair<KucoinMatchUpdateType, string>(KucoinMatchUpdateType.Update, "update"),
        };
    }
}
