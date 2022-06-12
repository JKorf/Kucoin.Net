using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Lend info
    /// </summary>
    public class KucoinLend
    {
        /// <summary>
        /// Trade id
        /// </summary>
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Repaid
        /// </summary>
        public decimal Repaid { get; set; }
        /// <summary>
        /// Term
        /// </summary>
        public int Term { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyInterestRate { get; set; }
    }

    /// <summary>
    /// Open lend info
    /// </summary>
    public class KucoinOpenLend: KucoinLend
    {
        /// <summary>
        /// Maturity time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime MaturityTime { get; set; }
        /// <summary>
        /// Accrued interest
        /// </summary>
        public decimal AccruedInterest { get; set; }
    }

    /// <summary>
    /// Closed lend info
    /// </summary>
    public class KucoinClosedLend: KucoinLend
    {
        /// <summary>
        /// Interest
        /// </summary>
        public decimal Interest { get; set; }
        /// <summary>
        /// Settle time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("settledAt")]
        public DateTime SettleTime { get; set; }
        /// <summary>
        /// Note
        /// </summary>
        public string? Note { get; set; }
    }
}
