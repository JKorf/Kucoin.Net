using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Account transaction info
    /// </summary>
    [SerializationModel]
    public record KucoinAccountTransaction
    {
        /// <summary>
        /// ["<c>time</c>"] Event time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type of the transaction
        /// </summary>
        [JsonPropertyName("type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity of the transaction
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee of the transaction
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// ["<c>accountEquity</c>"] Account equity
        /// </summary>
        [JsonPropertyName("accountEquity")]
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status 
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] Ticker of the contract
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>offset</c>"] Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
