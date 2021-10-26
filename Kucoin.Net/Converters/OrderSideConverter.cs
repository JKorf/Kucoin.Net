using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<OrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<OrderSide, string>> Mapping => new List<KeyValuePair<OrderSide, string>>
        {
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell")
        };
    }
}
