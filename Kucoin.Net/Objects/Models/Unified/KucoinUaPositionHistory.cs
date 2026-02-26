using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Position history
    /// </summary>
    public record KucoinUaPositionHistory
    {
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaPositionHistoryEntry[] Items { get; set; } = [];
        /// <summary>
        /// Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
    }

    /// <summary>
    /// History entry
    /// </summary>
    public record KucoinUaPositionHistoryEntry
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Close id
        /// </summary>
        [JsonPropertyName("closeId")]
        public long CloseId { get; set; }
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("closePrice")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("maxSize")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// Average close price
        /// </summary>
        [JsonPropertyName("avgClosePrice")]
        public decimal AverageClosePrice { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnL")]
        public decimal RealizedPnL { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonPropertyName("closingTime")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("creationTime")]
        public DateTime CreateTime { get; set; }
    }


}
