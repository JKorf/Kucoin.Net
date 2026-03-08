using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    [SerializationModel]
    public record KucoinWithdrawal
    {
        /// <summary>
        /// ["<c>id</c>"] The id of the withdrawal
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("withdrawalId")]
        internal string WithdrawalId { set => Id = value; get => Id; }

        /// <summary>
        /// ["<c>address</c>"] The address the withdrawal is to
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>memo</c>"] The memo for the withdrawal
        /// </summary>
        [JsonPropertyName("memo")]
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] The remark for the withdrawal
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] The asset of the withdrawal
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the withdrawal
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] The fee of the withdrawal
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>walletTxId</c>"] The wallet transaction id
        /// </summary>
        [JsonPropertyName("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isInner</c>"] Whether it is an internal withdrawal
        /// </summary>
        [JsonPropertyName("isInner")]
        public bool IsInner { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the converter
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] The time the withdrawal was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedAt</c>"] The time the withdrawal was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// ["<c>reason</c>"] Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
        /// <summary>
        /// ["<c>chain</c>"] The chain
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
    }
}
