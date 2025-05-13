using CryptoExchange.Net.Converters.SystemTextJson;
using System;


namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Balance update info
    /// </summary>
    [SerializationModel]
    public record KucoinBalanceUpdate
    {
        /// <summary>
        /// The total balance
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// The available balance
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// The change in available balance
        /// </summary>
        [JsonPropertyName("availableChange")]
        public decimal AvailableChange { get; set; }
        /// <summary>
        /// The asset this update is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The quantity currently in hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// The change in holding
        /// </summary>
        [JsonPropertyName("holdChange")]
        public decimal HoldChange { get; set; }
        /// <summary>
        /// The event that caused the change
        /// </summary>
        [JsonPropertyName("relationEvent")]
        public string RelationEvent { get; set; } = string.Empty;
        /// <summary>
        /// The cause id
        /// </summary>
        [JsonPropertyName("relationEventId")]
        public string RelationEventId { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp of the update
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The id of the changed account
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
    }
}
