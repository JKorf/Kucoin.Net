using CryptoExchange.Net.Converters.SystemTextJson;

using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Cancel after setting
    /// </summary>
    [SerializationModel]
    public record KucoinCancelAfter
    {
        /// <summary>
        /// Current time
        /// </summary>
        [JsonPropertyName("currentTime")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("triggerTime")]
        public DateTime TriggerTime { get; set; }
    }

    /// <summary>
    /// Cancel after status
    /// </summary>
    [SerializationModel]
    public record KucoinCancelAfterStatus : KucoinCancelAfter
    {
        /// <summary>
        /// Timeout in seconds
        /// </summary>
        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }
        /// <summary>
        /// Symbols, comma separated
        /// </summary>
        [JsonPropertyName("symbols")]
        public string Symbols { get; set; } = string.Empty;
    }
}
