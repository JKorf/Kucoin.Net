using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converters
{
    internal class MarginModeConverter : BaseConverter<KucoinMarginMode>
    {
        public MarginModeConverter() : this(true) { }
        public MarginModeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinMarginMode, string>> Mapping => new List<KeyValuePair<KucoinMarginMode, string>>
        {
            new KeyValuePair<KucoinMarginMode, string>(KucoinMarginMode.CrossMode, "cross"),
            new KeyValuePair<KucoinMarginMode, string>(KucoinMarginMode.IsolatedMode, "isolated"),
        };
    }
}
