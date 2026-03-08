namespace Kucoin.Net.Objects.Models.Spot
{
    /// <summary>
    /// Interest rates
    /// </summary>
    public record KucoinBorrowInterestRates
    {
        /// <summary>
        /// ["<c>vipLevel</c>"] VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Asset rates
        /// </summary>
        [JsonPropertyName("items")]
        public KucoinBorrowInterestRate[] Rates { get; set; } = [];
    }

    /// <summary>
    /// Interest rate
    /// </summary>
    public record KucoinBorrowInterestRate
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hourlyBorrowRate</c>"] Hourly borrow rate
        /// </summary>
        [JsonPropertyName("hourlyBorrowRate")]
        public decimal HourlyBorrowRate { get; set; }
        /// <summary>
        /// ["<c>annualizedBorrowRate</c>"] Annualized borrow rate
        /// </summary>
        [JsonPropertyName("annualizedBorrowRate")]
        public decimal AnnualizedBorrowRate { get; set; }
    }
}
