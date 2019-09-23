using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    internal class MatchUpdateReasonConverter : BaseConverter<KucoinMatchUpdateReason>
    {
        public MatchUpdateReasonConverter() : this(true) { }
        public MatchUpdateReasonConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinMatchUpdateReason, string>> Mapping => new List<KeyValuePair<KucoinMatchUpdateReason, string>>
        {
            new KeyValuePair<KucoinMatchUpdateReason, string>(KucoinMatchUpdateReason.Cancelled, "canceled"),
            new KeyValuePair<KucoinMatchUpdateReason, string>(KucoinMatchUpdateReason.Filled, "filled")
        };
    }
}
