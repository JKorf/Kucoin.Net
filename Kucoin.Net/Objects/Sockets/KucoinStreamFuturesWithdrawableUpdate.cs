using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kucoin.Net.Objects.Sockets
{
    /// <summary>
    /// Update to funds wich are withdrawable
    /// </summary>
    public class KucoinStreamFuturesWithdrawableUpdate
    {
        /// <summary>
        /// Current frozen amount for withdrawal
        /// </summary>
        public decimal WithdrawHold { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }
    }
}
