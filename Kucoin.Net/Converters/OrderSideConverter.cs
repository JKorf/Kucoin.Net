using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<KucoinOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinOrderSide, string>> Mapping => new List<KeyValuePair<KucoinOrderSide, string>>
        {
            new KeyValuePair<KucoinOrderSide, string>(KucoinOrderSide.Buy, "buy"),
            new KeyValuePair<KucoinOrderSide, string>(KucoinOrderSide.Sell, "sell")
        };
    }
}
