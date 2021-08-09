using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Objects;

namespace Kucoin.Net.Converters
{
    internal class KlineIntervalConverter : BaseConverter<KucoinKlineInterval>
    {
        public KlineIntervalConverter() : this(true) { }
        public KlineIntervalConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KucoinKlineInterval, string>> Mapping => new List<KeyValuePair<KucoinKlineInterval, string>>
        {
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.OneMinute, "1min"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.ThreeMinutes, "3min"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.FiveMinutes, "5min"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.FifteenMinutes, "15min"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.ThirtyMinutes, "30min"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.OneHour, "1hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.TwoHours, "2hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.FourHours, "4hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.SixHours, "6hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.EightHours, "8hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.TwelfHours, "12hour"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.OneDay, "1day"),
            new KeyValuePair<KucoinKlineInterval, string>(KucoinKlineInterval.OneWeek, "1week"),
        };
    }
}
