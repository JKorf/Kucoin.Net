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
        /// ["<c>debtRatio</c>"] Debt ratio
        /// </summary>
        [JsonPropertyName("debtRatio")]
        public decimal DebtRatio { get; set; }
        /// <summary>
        /// ["<c>totalDebt</c>"] Total debt in BTC
        /// </summary>
        [JsonPropertyName("totalDebt")]
        public decimal TotalDebt { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>debtList</c>"] Debt list
        /// </summary>
        [JsonPropertyName("debtList")]
        public Dictionary<string, decimal> Debts { get; set; } = new Dictionary<string, decimal>();
    }
}
