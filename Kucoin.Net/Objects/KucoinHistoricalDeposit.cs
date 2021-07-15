using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    /// <summary>
    /// Historical deposit info
    /// </summary>
    public class KucoinHistoricalDeposit
    {
        /// <summary>
        /// The currency of the deposit
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public KucoinDepositStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The time the deposit was created
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// Whether is is an internal deposit
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The amount of the deposit
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
