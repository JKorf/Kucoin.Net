using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Position change caused by mark price
    /// </summary>
    [SerializationModel]
    public record KucoinPositionMarkPriceUpdate
    {
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Mark value
        /// </summary>
        [JsonPropertyName("markValue")]
        public decimal MarkValue { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Real leverage
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Unrealized ROE
        /// </summary>
        [JsonPropertyName("unrealisedRoePcnt")]
        public decimal UnrealizedRoePercentage { get; set; }
        /// <summary>
        /// Unrealized profit and loss percentage
        /// </summary>
        [JsonPropertyName("unrealisedPnlPcnt")]
        public decimal UnrealizedPnlPercentage { get; set; }
        /// <summary>
        /// Adl ranking percentile
        /// </summary>
        [JsonPropertyName("delevPercentage")]
        public decimal DeleveragePercentage { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("currentTimestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Currency used to clear and settle the trades
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
