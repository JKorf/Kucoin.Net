using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;
using System.Collections.Generic;

namespace Kucoin.Net.Converts
{
    public class LiquidityTypeConverter : BaseConverter<KucoinLiquidityType>
    {
        public LiquidityTypeConverter() : this(true) { }
        public LiquidityTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinLiquidityType, string>> Mapping => new List<KeyValuePair<KucoinLiquidityType, string>>
        {
            new KeyValuePair<KucoinLiquidityType, string>(KucoinLiquidityType.Maker, "maker"),
            new KeyValuePair<KucoinLiquidityType, string>(KucoinLiquidityType.Taker, "taker"),
        };
    }
}
