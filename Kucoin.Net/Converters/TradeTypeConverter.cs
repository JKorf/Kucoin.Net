using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class TradeTypeConverter : BaseConverter<KucoinTradeType>
    {
        public TradeTypeConverter() : this(true) { }

        public TradeTypeConverter(bool quotes) : base(quotes)
        {
        }

        protected override List<KeyValuePair<KucoinTradeType, string>> Mapping => new List<KeyValuePair<KucoinTradeType, string>>
        {
            new KeyValuePair<KucoinTradeType, string>(KucoinTradeType.MarginTrade, "MARGIN_TRADE"),
            new KeyValuePair<KucoinTradeType, string>(KucoinTradeType.SpotTrade, "TRADE")
        };
    }
}
