using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Deposit info
    /// </summary>
    [SerializationModel]
    public record KucoinDeposit
    {
        /// <summary>
        /// ["<c>address</c>"] The deposit address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>memo</c>"] A memo for this deposit
        /// </summary>
        [JsonPropertyName("memo")]
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>remark</c>"] A remark for this deposit
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chain</c>"] The chain
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the deposit
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] The fee of the deposit
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] The asset of the deposit
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>isInner</c>"] Whether it is an internal deposit
        /// </summary>
        [JsonPropertyName("isInner")]
        public bool IsInner { get; set; }
        /// <summary>
        /// ["<c>walletTxId</c>"] The wallet transaction id
        /// </summary>
        [JsonPropertyName("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The deposit status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>createdAt</c>"] When the deposit was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedAt</c>"] When the deposit was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Deposit id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subStatus</c>"] Additional status info
        /// </summary>
        [JsonPropertyName("subStatus")]
        public string? SubStatus { get; set; }
        /// <summary>
        /// ["<c>url</c>"] Url
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        /// <summary>
        /// ["<c>needSubmitWht</c>"] Need submit WHT
        /// </summary>
        [JsonPropertyName("needSubmitWht")]
        public string? NeedSubmitWht { get; set; }
        /// <summary>
        /// ["<c>arrears</c>"] Is debt. A quick rollback will cause the deposit to fail. If the deposit fails, you will need to repay the balance.
        /// </summary>
        [JsonPropertyName("arrears")]
        public bool Arrears { get; set; }
        /// <summary>
        /// ["<c>preConfirmationCount</c>"] Pre-confirmation count
        /// </summary>
        [JsonPropertyName("preConfirmationCount")]
        public int? PreConfirmations { get; set; }
        /// <summary>
        /// ["<c>confirmationCount</c>"] Confirmation count
        /// </summary>
        [JsonPropertyName("confirmationCount")]
        public int? ConfirmationCount { get; set; }
        /// <summary>
        /// ["<c>confirmation</c>"] Confirmations
        /// </summary>
        [JsonPropertyName("confirmation")]
        public int? Confirmations { get; set; }
        /// <summary>
        /// ["<c>failureReason</c>"] Failure reason
        /// </summary>
        [JsonPropertyName("failureReason")]
        public string? FailureReason { get; set; }
    }
}
