using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesUserTrade: KucoinTradeBase
    {
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public FuturesTradeType TradeType { get; set; }

        /// <summary>
        /// Order value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// Fixed fee
        /// </summary>
        [JsonPropertyName("fixFee")]
        public decimal FixFee { get; set; }

        /// <summary>
        /// Trade time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// Opening transaction fee
        /// </summary>
        [JsonPropertyName("openFeePay")]
        public decimal? OpenFeePay { get; set; }
        /// <summary>
        /// Closing transaction fee
        /// </summary>
        [JsonPropertyName("closeFeePay")]
        public decimal? CloseFeePay { get; set; }
        /// <summary>
        /// Whether to force processing as a taker
        /// </summary>
        [JsonPropertyName("forceTaker")]
        public bool ForceTaker { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
    }
}
