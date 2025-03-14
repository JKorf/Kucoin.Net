using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Isolated margin position update
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginPositionUpdate
    {
        /// <summary>
        /// Tag
        /// </summary>
        [JsonPropertyName("tag")]
        public string Tag { get; set; } = string.Empty;
        /// <summary>
        /// Accumelated principal
        /// </summary>
        [JsonPropertyName("accumulatedPrincipal")]
        public decimal AccumelatedPrincipal { get; set; }
        /// <summary>
        /// Data timestamp
        /// </summary>
        [JsonPropertyName("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Position status
        /// </summary>
        [JsonPropertyName("status")]
        public IsolatedPositionStatus Status { get; set; }
        /// <summary>
        /// Changed assets
        /// </summary>
        [JsonPropertyName("changeAssets")]
        public Dictionary<string, KucoinIsolatedMarginAsset> ChangedAssets { get; set; } = new Dictionary<string, KucoinIsolatedMarginAsset>();

    }

    /// <summary>
    /// Isolated margin asset info
    /// </summary>
    [SerializationModel]
    public record KucoinIsolatedMarginAsset
    {
        /// <summary>
        /// Total asset
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// Frozen asset
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// Liability principal
        /// </summary>
        [JsonPropertyName("liabilityPrincipal")]
        public decimal LiabilityPrincipal { get; set; }
        /// <summary>
        /// Liability interest
        /// </summary>
        [JsonPropertyName("liabilityInterest")]
        public decimal LiabilityInterest { get; set; }
    }
}
