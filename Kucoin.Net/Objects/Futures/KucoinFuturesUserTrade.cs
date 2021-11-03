using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class KucoinFuturesUserTrade: KucoinTradeBase
    {
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public KucoinOrderType OrderType { get; set; }

        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(FuturesTradeTypeConverter))]
        public FuturesTradeType TradeType { get; set; }

        /// <summary>
        /// Order value
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Fixed fee
        /// </summary>
        public decimal FixFee { get; set; }

        /// <summary>
        /// Trade time
        /// </summary>
        [JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Settlement currency
        /// </summary>
        public string SettleCurrency { get; set; } = string.Empty;
    }
}
