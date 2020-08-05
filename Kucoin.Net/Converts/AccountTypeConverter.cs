using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    internal class AccountTypeConverter : BaseConverter<KucoinAccountType>
    {
	    private readonly bool _useCaps;
        public AccountTypeConverter() : this(true) { }

        public AccountTypeConverter(bool quotes, bool useCaps = false) : base(quotes)
        {
	        _useCaps = useCaps;
        }
        protected override List<KeyValuePair<KucoinAccountType, string>> Mapping => new List<KeyValuePair<KucoinAccountType, string>>
        {
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Main, _useCaps ? "MAIN" : "main"),
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Trade, _useCaps ? "TRADE" : "trade")
        };
    }
}
