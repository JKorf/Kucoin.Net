using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converters
{
    internal class LentOrderStatusConverter: BaseConverter<KucoinLentOrderStatus>
    {
        public LentOrderStatusConverter() : this(true) { }

        public LentOrderStatusConverter(bool quotes): base(quotes) { }

        protected override List<KeyValuePair<KucoinLentOrderStatus, string>> Mapping => new List<KeyValuePair<KucoinLentOrderStatus, string>>
        {
            new KeyValuePair<KucoinLentOrderStatus, string>(KucoinLentOrderStatus.Filled, "FILLED"),
            new KeyValuePair<KucoinLentOrderStatus, string>(KucoinLentOrderStatus.Canceled, "CANCELED"),
        };
    }
}
