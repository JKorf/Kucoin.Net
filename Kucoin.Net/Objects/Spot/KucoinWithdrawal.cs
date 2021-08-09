using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Spot
{
    /// <summary>
    /// Withdrawal info
    /// </summary>
    public class KucoinWithdrawal
    {
        /// <summary>
        /// The id of the withdrawal
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The address the withdrawal is to
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// The memo for the withdrawal
        /// </summary>
        public string Memo { get; set; } = string.Empty;
        /// <summary>
        /// The currency of the withdrawal
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The quantity of the withdrawal
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// The fee of the withdrawal
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// Whether it is an internal withdrawal
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// Status of the converter
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public KucoinWithdrawalStatus Status { get; set; }
        /// <summary>
        /// The time the withdrawal was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The time the withdrawal was last updated
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
