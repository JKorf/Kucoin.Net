using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record KucoinFuturesUserTrade: KucoinTradeBase
    {
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonConverter(typeof(OrderTypeConverter))]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Trade type
        /// </summary>
        [JsonConverter(typeof(FuturesTradeTypeConverter))]
        public FuturesTradeType TradeType { get; set; }

        /// <summary>
        /// Order value
        /// </summary>
        [JsonProperty("value")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Fixed fee
        /// </summary>
        public decimal FixFee { get; set; }

        /// <summary>
        /// Trade time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Settlement asset
        /// </summary>
        [JsonProperty("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// Opening transaction fee
        /// </summary>
        public decimal? OpenFeePay { get; set; }
        /// <summary>
        /// Closing transaction fee
        /// </summary>
        public decimal? CloseFeePay { get; set; }
        /// <summary>
        /// Whether to force processing as a taker
        /// </summary>
        [JsonProperty("forceTaker")]
        public bool ForceTaker { get; set; }
    }
}
