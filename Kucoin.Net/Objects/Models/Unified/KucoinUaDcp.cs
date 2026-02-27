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
        /// Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public UnifiedSimpleAccountType TradeType { get; set; }
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbol")]
        public string[]? Symbols { get; set; }
        /// <summary>
        /// System time
        /// </summary>
        [JsonPropertyName("systemTime")]
        public DateTime SystemTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime TriggerTime { get; set; }
    }


}
