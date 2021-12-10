using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converters
{
    internal class BorrowStatusConverter : BaseConverter<KucoinBorrowStatus>
    {
        public BorrowStatusConverter() : this(true) { }
        public BorrowStatusConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinBorrowStatus, string>> Mapping => new List<KeyValuePair<KucoinBorrowStatus, string>>
        {
            new KeyValuePair<KucoinBorrowStatus, string>(KucoinBorrowStatus.Processing, "Processing"),
            new KeyValuePair<KucoinBorrowStatus, string>(KucoinBorrowStatus.Done, "Done"),
        };
    }
}
