using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Historical deposit info
    /// </summary>
    [SerializationModel]
    public record KucoinHistoricalDeposit
    {
        /// <summary>
        /// ["<c>currency</c>"] The asset of the deposit
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] The status of the deposit
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// ["<c>walletTxId</c>"] The wallet transaction id
        /// </summary>
        [JsonPropertyName("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>createAt</c>"] The time the deposit was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>isInner</c>"] Whether is is an internal deposit
        /// </summary>
        [JsonPropertyName("isInner")]
        public bool IsInner { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] The quantity of the deposit
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
