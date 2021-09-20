using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
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
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Trade, _useCaps ? "TRADE" : "trade"),
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Margin, _useCaps ? "MARGIN" : "margin"),
            new KeyValuePair<KucoinAccountType, string>(KucoinAccountType.Pool, _useCaps ? "POOL" : "pool"),
        };
    }
}
