using System;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Account activity info
    /// </summary>
    [SerializationModel]
    public record KucoinAccountActivity
    {
        /// <summary>
        /// ["<c>createdAt</c>"] Creation timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the activity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] The remaining balance after the activity
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] The fee of the activity
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>bizType</c>"] The type of activity
        /// </summary>
        [JsonPropertyName("bizType")]
        public BizType BizType { get; set; } = default!;
        /// <summary>
        /// ["<c>accountType</c>"] The type of activity
        /// </summary>
        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; } = default!;
        /// <summary>
        /// ["<c>id</c>"] The unique key for this activity 
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>context</c>"] Additional info for this activity
        /// </summary>
        [JsonConverter(typeof(AccountActivityContextConverter))]
        [JsonPropertyName("context")]
        public KucoinAccountActivityContext Context { get; set; } = default!;
        /// <summary>
        /// ["<c>currency</c>"] The asset of the activity
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>direction</c>"] The direction of the activity
        /// </summary>
        [JsonPropertyName("direction")]
        public AccountDirection Direction { get; set; }
    }

    /// <summary>
    /// Account activity details
    /// </summary>
    [SerializationModel]
    public record KucoinAccountActivityContext
    {
        /// <summary>
        /// ["<c>orderId</c>"] The id for the order
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tradeId</c>"] The id for the trade (for trades)
        /// </summary>
        [JsonPropertyName("tradeId")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] The symbol of the order (for trades)
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactionId</c>"] The transaction id (for withdrawal/deposit)
        /// </summary>
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>txId</c>"] The txId (for orders)
        /// </summary>
        [JsonPropertyName("txId")]
        public string TxId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>description</c>"] The Description (for pool-x staking rewards)
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
