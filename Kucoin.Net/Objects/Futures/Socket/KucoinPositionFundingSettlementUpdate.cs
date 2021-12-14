using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Kucoin.Net.Objects.Futures.Socket
{
    /// <summary>
    /// Funding settlement update
    /// </summary>
    public class KucoinPositionFundingSettlementUpdate
    {
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonProperty("fundingTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime FundTime { get; set; }
        /// <summary>
        /// Position size
        /// </summary>
        [JsonProperty("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Settlement price
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Funding fee
        /// </summary>
        public decimal FundingFee { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(TimestampNanoSecondsConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset used to clear and settle the trades
        /// </summary>
        [JsonProperty("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
