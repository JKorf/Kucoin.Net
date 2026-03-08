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
        /// ["<c>applyId</c>"] Apply id
        /// </summary>
        [JsonPropertyName("applyId")]
        public string ApplyId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status of the transfer
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity of the transfer
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>reason</c>"] Reason if failed
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public long Offset { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] User remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>recRemark</c>"] Receive account tx remark
        /// </summary>
        [JsonPropertyName("recRemark")]
        public string? ReceiveRemark { get; set; }
        /// <summary>
        /// ["<c>recSystem</c>"] Receive system
        /// </summary>
        [JsonPropertyName("recSystem")]
        public string? ReceiveSystem { get; set; }
    }
}
