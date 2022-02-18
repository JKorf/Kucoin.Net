using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;

namespace Kucoin.Net.Converters
{
    internal class SelfTradePreventionConverter : BaseConverter<SelfTradePrevention>
    {
        public SelfTradePreventionConverter() : this(true) { }
        public SelfTradePreventionConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<SelfTradePrevention, string>> Mapping => new List<KeyValuePair<SelfTradePrevention, string>>
        {
            new KeyValuePair<SelfTradePrevention, string>(SelfTradePrevention.None, ""),
            new KeyValuePair<SelfTradePrevention, string>(SelfTradePrevention.DecreaseAndCancel, "DC"),
            new KeyValuePair<SelfTradePrevention, string>(SelfTradePrevention.CancelOldest, "CO"),
            new KeyValuePair<SelfTradePrevention, string>(SelfTradePrevention.CancelNewest, "CN"),
            new KeyValuePair<SelfTradePrevention, string>(SelfTradePrevention.CancelBoth, "CB"),
        };
    }
}
