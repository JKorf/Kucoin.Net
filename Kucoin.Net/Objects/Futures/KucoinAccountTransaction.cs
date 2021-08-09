using CryptoExchange.Net.Converters;
using Kucoin.Net.Enums;
using Newtonsoft.Json;
using System;
using Kucoin.Net.Converters;

namespace Kucoin.Net.Objects.Futures
{
    /// <summary>
    /// Account transaction info
    /// </summary>
    public class KucoinAccountTransaction
    {
        /// <summary>
        /// Event time
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        /// <summary>
        /// Type of the transaction
        /// </summary>
        [JsonConverter(typeof(TransactionTypeConverter))]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// Amount of the transaction
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Fee of the transaction
        /// </summary>
        public decimal? Fee { get; set; }
        /// <summary>
        /// Account equity
        /// </summary>
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// Status 
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Ticker of the contract
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// Offset
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
    }
}
