using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Converts
{
    internal class StopPriceTypeConverter : BaseConverter<StopPriceType>
    {
        public StopPriceTypeConverter() : this(true) { }
        public StopPriceTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<StopPriceType, string>> Mapping => new List<KeyValuePair<StopPriceType, string>>
        {
            new KeyValuePair<StopPriceType, string>(StopPriceType.IndexPrice, "IP"),
            new KeyValuePair<StopPriceType, string>(StopPriceType.MarkPrice, "MP"),
            new KeyValuePair<StopPriceType, string>(StopPriceType.TradePrice, "TP"),
        };
    }
}
