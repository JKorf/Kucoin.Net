using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    public class KucoinDeposit
    {
        /// <summary>
        /// The deposit address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// A memo for this deposit
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// The amount of the deposit
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The fee of the deposit
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The currency of the deposit
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Whether it is an internal deposit
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; }
        /// <summary>
        /// The deposit status
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public KucoinDepositStatus Status { get; set; }
        /// <summary>
        /// When the deposit was created
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// When the deposit was last updated
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}
