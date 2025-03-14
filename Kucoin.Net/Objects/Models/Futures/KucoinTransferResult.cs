using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Transfer result
    /// </summary>
    [SerializationModel]
    public record KucoinTransferResult
    {
        /// <summary>
        /// Request id
        /// </summary>
        [JsonPropertyName("applyId")]
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// Business number
        /// </summary>
        [JsonPropertyName("bizNo")]
        public string BusinessNumber { get; set; } = string.Empty;
        /// <summary>
        /// Pay account type
        /// </summary>
        [JsonPropertyName("payAccountType")]
        public string PayAccountType { get; set; } = string.Empty;
        /// <summary>
        /// Pay tag
        /// </summary>
        [JsonPropertyName("payTag")]
        public string PayTag { get; set; } = string.Empty;
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("recAccountType")]
        public AccountType? ReceiveAccountType { get; set; }
        /// <summary>
        /// Receive tag
        /// </summary>
        [JsonPropertyName("recTag")]
        public string ReceiveTag { get; set; } = string.Empty;
        /// <summary>
        /// Receive remark
        /// </summary>
        [JsonPropertyName("recRemark")]
        public string ReceiveRemark { get; set; } = string.Empty;
        /// <summary>
        /// Receive system
        /// </summary>
        [JsonPropertyName("recSystem")]
        public string ReceiveSystem { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Serial number
        /// </summary>
        [JsonPropertyName("sn")]
        public long? SerialNumber { get; set; }
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Updated time
        /// </summary>
        [JsonPropertyName("updatedAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
    }
}
