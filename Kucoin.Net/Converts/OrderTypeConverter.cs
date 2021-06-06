using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    internal class OrderTypeConverter : BaseConverter<KucoinOrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinOrderType, string>> Mapping => new List<KeyValuePair<KucoinOrderType, string>>
        {
            new KeyValuePair<KucoinOrderType, string>(KucoinOrderType.Limit, "limit"),
            new KeyValuePair<KucoinOrderType, string>(KucoinOrderType.Market, "market"),
            new KeyValuePair<KucoinOrderType, string>(KucoinOrderType.LimitStop, "limit_stop"),
            new KeyValuePair<KucoinOrderType, string>(KucoinOrderType.MarketStop, "market_stop"),
            new KeyValuePair<KucoinOrderType, string>(KucoinOrderType.Stop, "stop"),
        };
    }
}
