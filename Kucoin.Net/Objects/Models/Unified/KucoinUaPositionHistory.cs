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
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaPositionHistoryEntry[] Items { get; set; } = [];
        /// <summary>
        /// ["<c>lastId</c>"] Last id
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>closeId</c>"] Close id
        /// </summary>
        [JsonPropertyName("closeId")]
        public long CloseId { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Position side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>closePrice</c>"] Close price
        /// </summary>
        [JsonPropertyName("closePrice")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>maxSize</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("maxSize")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>avgClosePrice</c>"] Average close price
        /// </summary>
        [JsonPropertyName("avgClosePrice")]
        public decimal AverageClosePrice { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>realizedPnL</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedPnL")]
        public decimal RealizedPnL { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>tax</c>"] Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal Tax { get; set; }
        /// <summary>
        /// ["<c>fundingFee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// ["<c>closingTime</c>"] Close time
        /// </summary>
        [JsonPropertyName("closingTime")]
        public DateTime CloseTime { get; set; }
        /// <summary>
        /// ["<c>creationTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("creationTime")]
        public DateTime CreateTime { get; set; }
    }


}
