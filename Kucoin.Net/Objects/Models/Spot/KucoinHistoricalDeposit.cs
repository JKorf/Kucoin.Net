using System;
using CryptoExchange.Net.Converters;
using Kucoin.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Historical deposit info
    /// </summary>
    public record KucoinHistoricalDeposit
    {
        /// <summary>
        /// The asset of the deposit
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The status of the deposit
        /// </summary>
        [JsonConverter(typeof(DepositStatusConverter))]
        public DepositStatus Status { get; set; }
        /// <summary>
        /// The wallet transaction id
        /// </summary>
        [JsonProperty("walletTxId")]
        public string WalletTransactionId { get; set; } = string.Empty;
        /// <summary>
        /// The time the deposit was created
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("createAt")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Whether is is an internal deposit
        /// </summary>
        public bool IsInner { get; set; }
        /// <summary>
        /// The quantity of the deposit
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }
    }
}
