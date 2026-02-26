using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Cancel result
    /// </summary>
    public record KucoinUaBatchCancelResult
    {
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaCancelResult[] Items { get; set; } = [];
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    public record KucoinUaCancelResult
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string? OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOid")]
        public string? ClientOrderId { get; set; }
    }

}
