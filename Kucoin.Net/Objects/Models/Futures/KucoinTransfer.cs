using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record KucoinTransfer
    {
        /// <summary>
        /// Apply id
        /// </summary>
        [JsonPropertyName("applyId")]
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// Quantity of the transfer
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Reason if failed
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public long Offset { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// User remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Receive account tx remark
        /// </summary>
        [JsonPropertyName("recRemark")]
        public string? ReceiveRemark { get; set; }
        /// <summary>
        /// Receive system
        /// </summary>
        [JsonPropertyName("recSystem")]
        public string? ReceiveSystem { get; set; }
    }
}
