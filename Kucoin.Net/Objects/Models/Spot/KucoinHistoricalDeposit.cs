using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// The asset of the deposit
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonPropertyName("status")]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonPropertyName("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The time the deposit was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("createAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Whether is is an internal deposit
        /// </summary>
        [JsonPropertyName("isInner")]
        public bool IsInner { get; set; }
        /// <summary>
        /// The quantity of the deposit
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
    }
}
