using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    internal class AccountDirectionConverter : BaseConverter<KucoinAccountDirection>
    {
        public AccountDirectionConverter() : this(true) { }
        public AccountDirectionConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinAccountDirection, string>> Mapping => new List<KeyValuePair<KucoinAccountDirection, string>>
        {
            new KeyValuePair<KucoinAccountDirection, string>(KucoinAccountDirection.In, "in"),
            new KeyValuePair<KucoinAccountDirection, string>(KucoinAccountDirection.Out, "out")
        };
    }
}
