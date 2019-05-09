using CryptoExchange.Net.Converters;
using Kucoin.Net.Converts;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects
{
    public class KucoinHistoricalWithdrawal
    {
        /// <summary>
        /// The currency of the withdrawal
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// The address the withdrawal was to
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The status of the withdrawal
        /// </summary>
        [JsonConverter(typeof(WithdrawalStatusConverter))]
        public KucoinWithdrawalStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; }
        /// <summary>
        /// The time the withdrawal was created
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// Wheter it was an internal withdrawal
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The quantity of the withdrawal
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
