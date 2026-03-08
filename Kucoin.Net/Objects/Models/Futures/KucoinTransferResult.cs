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
        /// ["<c>applyId</c>"] Request id
        /// </summary>
        [JsonPropertyName("applyId")]
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bizNo</c>"] Business number
        /// </summary>
        [JsonPropertyName("bizNo")]
        public string BusinessNumber { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payAccountType</c>"] Pay account type
        /// </summary>
        [JsonPropertyName("payAccountType")]
        public string PayAccountType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payTag</c>"] Pay tag
        /// </summary>
        [JsonPropertyName("payTag")]
        public string PayTag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>recAccountType</c>"] Remark
        /// </summary>
        [JsonPropertyName("recAccountType")]
        public AccountType? ReceiveAccountType { get; set; }
        /// <summary>
        /// ["<c>recTag</c>"] Receive tag
        /// </summary>
        [JsonPropertyName("recTag")]
        public string ReceiveTag { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>recRemark</c>"] Receive remark
        /// </summary>
        [JsonPropertyName("recRemark")]
        public string ReceiveRemark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>recSystem</c>"] Receive system
        /// </summary>
        [JsonPropertyName("recSystem")]
        public string ReceiveSystem { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>sn</c>"] Serial number
        /// </summary>
        [JsonPropertyName("sn")]
        public long? SerialNumber { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] Create time
        /// </summary>
        [JsonPropertyName("createdAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedAt</c>"] Updated time
        /// </summary>
        [JsonPropertyName("updatedAt"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
    }
}
