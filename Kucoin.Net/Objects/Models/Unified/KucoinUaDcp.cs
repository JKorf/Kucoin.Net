using Kucoin.Net.Enums;
using System;

namespace Kucoin.Net.Objects.Models.Unified
{
    /// <summary>
    /// DCP configuration
    /// </summary>
    public record KucoinUaDcp
    {
        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedSimpleAccountType TradeType { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbol")]
        public string[]? Symbols { get; set; }
        /// <summary>
        /// ["<c>systemTime</c>"] System time
        /// </summary>
        [JsonPropertyName("systemTime")]
        public DateTime SystemTime { get; set; }
        /// <summary>
        /// ["<c>triggerTime</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime TriggerTime { get; set; }
    }


}
