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
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>markValue</c>"] Mark value
        /// </summary>
        [JsonPropertyName("markValue")]
        public decimal MarkValue { get; set; }
        /// <summary>
        /// ["<c>maintMargin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintMargin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>realLeverage</c>"] Real leverage
        /// </summary>
        [JsonPropertyName("realLeverage")]
        public decimal RealLeverage { get; set; }
        /// <summary>
        /// ["<c>unrealisedPnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>unrealisedRoePcnt</c>"] Unrealized ROE
        /// </summary>
        [JsonPropertyName("unrealisedRoePcnt")]
        public decimal UnrealizedRoePercentage { get; set; }
        /// <summary>
        /// ["<c>unrealisedPnlPcnt</c>"] Unrealized profit and loss percentage
        /// </summary>
        [JsonPropertyName("unrealisedPnlPcnt")]
        public decimal UnrealizedPnlPercentage { get; set; }
        /// <summary>
        /// ["<c>delevPercentage</c>"] Adl ranking percentile
        /// </summary>
        [JsonPropertyName("delevPercentage")]
        public decimal DeleveragePercentage { get; set; }
        /// <summary>
        /// ["<c>currentTimestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("currentTimestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Currency used to clear and settle the trades
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
