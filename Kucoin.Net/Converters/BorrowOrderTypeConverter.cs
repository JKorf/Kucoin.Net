using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class BorrowOrderTypeConverter : BaseConverter<KucoinBorrowOrderType>
    {
        public BorrowOrderTypeConverter() : this(true) { }
        public BorrowOrderTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinBorrowOrderType, string>> Mapping => new List<KeyValuePair<KucoinBorrowOrderType, string>>
        {
            new KeyValuePair<KucoinBorrowOrderType, string>(KucoinBorrowOrderType.FOK, "FOK"),
            new KeyValuePair<KucoinBorrowOrderType, string>(KucoinBorrowOrderType.IOC, "IOC")
        };
    }
}
