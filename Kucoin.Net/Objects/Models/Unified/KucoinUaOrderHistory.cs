using Kucoin.Net.Enums;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// Order history
    /// </summary>
    public record KucoinUaOrderHistory
    {
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedAccountType TradeType { get; set; }
        /// <summary>
        /// Last id
        /// </summary>
        [JsonPropertyName("lastId")]
        public long? LastId { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinUaOrder[] Items { get; set; } = [];
    }
}
