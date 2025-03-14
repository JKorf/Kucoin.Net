using CryptoExchange.Net.Converters.SystemTextJson;

using System;
using System.Collections.Generic;

namespace Kucoin.Net.Objects.Models.Spot.Socket
{
    /// <summary>
    /// Debt ratio update
    /// </summary>
    [SerializationModel]
    public record KucoinMarginDebtRatioUpdate
    {
        /// <summary>
        /// Debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// Total debt in BTC
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Debt list
        /// </summary>
        [JsonPropertyName("debtList")]
        public Dictionary<string, decimal> Debts { get; set; } = new Dictionary<string, decimal>();
    }
}
