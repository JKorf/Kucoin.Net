using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    public class AccountTypeConverter : BaseConverter<KucoinAccountType>
    {
        public AccountTypeConverter() : this(true) { }
        public AccountTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinAccountType, string>> Mapping => new List<KeyValuePair<KucoinAccountType, string>>
        {
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Main, "main"),
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Trade, "trade")
        };
    }
}
