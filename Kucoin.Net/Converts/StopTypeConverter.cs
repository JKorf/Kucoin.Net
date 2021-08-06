using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Converts
{
    internal class StopTypeConverter : BaseConverter<StopType>
    {
        public StopTypeConverter() : this(true) { }
        public StopTypeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<StopType, string>> Mapping => new List<KeyValuePair<StopType, string>>
        {
            new KeyValuePair<StopType, string>(StopType.Up, "up"),
            new KeyValuePair<StopType, string>(StopType.Down, "down"),
        };
    }
}
