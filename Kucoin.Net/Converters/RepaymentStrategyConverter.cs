using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converters
{
    internal class RepaymentStrategyConverter : BaseConverter<KucoinRepaymentStrategy>
    {
        public RepaymentStrategyConverter() : this(true) { }

        public RepaymentStrategyConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<KucoinRepaymentStrategy, string>> Mapping => new List<KeyValuePair<KucoinRepaymentStrategy, string>>
        {
            new KeyValuePair<KucoinRepaymentStrategy, string>(KucoinRepaymentStrategy.RecentlyExpireFirst, "RECENTLY_EXPIRE_FIRST"),
            new KeyValuePair<KucoinRepaymentStrategy, string>(KucoinRepaymentStrategy.HighestRateFirst, "HIGHEST_RATE_FIRST"),
        };
    }
}
