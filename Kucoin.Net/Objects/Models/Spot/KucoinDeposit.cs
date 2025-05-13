using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The deposit address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// A memo for this deposit
        /// </summary>
        [JsonPropertyName("memo")]
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// A remark for this deposit
        /// </summary>
        [JsonPropertyName("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// The chain
        /// </summary>
        [JsonPropertyName("chain")]
        public string? Network { get; set; }
        /// <summary>
        /// The quantity of the deposit
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The fee of the deposit
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset of the deposit
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Whether it is an internal deposit
        /// </summary>
        [JsonPropertyName("isInner")]
        public bool IsInner { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonPropertyName("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The deposit status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// When the deposit was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createdAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// When the deposit was last updated
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("updatedAt")]
        public DateTime UpdateTime { get; set; }
    }
}
