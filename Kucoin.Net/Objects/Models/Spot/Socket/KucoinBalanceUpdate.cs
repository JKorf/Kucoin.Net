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
        /// ["<c>total</c>"] The total balance
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>available</c>"] The available balance
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>availableChange</c>"] The change in available balance
        /// </summary>
        [JsonPropertyName("availableChange")]
        public decimal AvailableChange { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] The asset this update is for
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hold</c>"] The quantity currently in hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// ["<c>holdChange</c>"] The change in holding
        /// </summary>
        [JsonPropertyName("holdChange")]
        public decimal HoldChange { get; set; }
        /// <summary>
        /// ["<c>relationEvent</c>"] The event that caused the change
        /// </summary>
        [JsonPropertyName("relationEvent")]
        public string RelationEvent { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>relationEventId</c>"] The cause id
        /// </summary>
        [JsonPropertyName("relationEventId")]
        public string RelationEventId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time</c>"] The timestamp of the update
        /// </summary>
        [JsonPropertyName("time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>accountId</c>"] The id of the changed account
        /// </summary>
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
    }
}
