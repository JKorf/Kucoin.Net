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
        /// ["<c>currentTime</c>"] Current time
        /// </summary>
        [JsonPropertyName("currentTime")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// ["<c>triggerTime</c>"] Trigger time
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
        /// ["<c>timeout</c>"] Timeout in seconds
        /// </summary>
        [JsonPropertyName("timeout")]
        public int Timeout { get; set; }
        /// <summary>
        /// ["<c>symbols</c>"] Symbols, comma separated
        /// </summary>
        [JsonPropertyName("symbols")]
        public string Symbols { get; set; } = string.Empty;
    }
}
