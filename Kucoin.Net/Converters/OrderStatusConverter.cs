using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class OrderStatusConverter : BaseConverter<KucoinOrderStatus>
    {
        public OrderStatusConverter() : this(true) { }
        public OrderStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinOrderStatus, string>> Mapping => new List<KeyValuePair<KucoinOrderStatus, string>>
        {
            new KeyValuePair<KucoinOrderStatus, string>(KucoinOrderStatus.Active, "active"),
            new KeyValuePair<KucoinOrderStatus, string>(KucoinOrderStatus.Done, "done"),
        };
    }
}
