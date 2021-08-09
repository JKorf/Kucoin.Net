using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class NewOrderTypeConverter : BaseConverter<KucoinNewOrderType>
    {
        public NewOrderTypeConverter() : this(true) { }
        public NewOrderTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinNewOrderType, string>> Mapping => new List<KeyValuePair<KucoinNewOrderType, string>>
        {
            new KeyValuePair<KucoinNewOrderType, string>(KucoinNewOrderType.Limit, "limit"),
            new KeyValuePair<KucoinNewOrderType, string>(KucoinNewOrderType.Market, "market")
        };
    }
}
