using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Wallet update
    /// </summary>
    [SerializationModel]
    public record KucoinStreamFuturesWalletUpdate
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>holdBalance</c>"] Hold balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// ["<c>isolatedOrderMargin</c>"] Isolated order margin
        /// </summary>
        [JsonPropertyName("isolatedOrderMargin")]
        public decimal IsolatedOrderMargin { get; set; }
        /// <summary>
        /// ["<c>isolatedPosMargin</c>"] Isolated pos margin
        /// </summary>
        [JsonPropertyName("isolatedPosMargin")]
        public decimal IsolatedPosMargin { get; set; }
        /// <summary>
        /// ["<c>isolatedUnPnl</c>"] Isolated unrealized profit and loss
        /// </summary>
        [JsonPropertyName("isolatedUnPnl")]
        public decimal IsolatedUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>isolatedFundingFeeMargin</c>"] Isolated funding fee margin
        /// </summary>
        [JsonPropertyName("isolatedFundingFeeMargin")]
        public decimal IsolatedFundingFeeMargin { get; set; }
        /// <summary>
        /// ["<c>crossOrderMargin</c>"] Cross order margin
        /// </summary>
        [JsonPropertyName("crossOrderMargin")]
        public decimal CrossOrderMargin { get; set; }
        /// <summary>
        /// ["<c>crossPosMargin</c>"] Cross position margin
        /// </summary>
        [JsonPropertyName("crossPosMargin")]
        public decimal CrossPositionMargin { get; set; }
        /// <summary>
        /// ["<c>crossUnPnl</c>"] Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>totalCrossMargin</c>"] Total cross margin
        /// </summary>
        [JsonPropertyName("totalCrossMargin")]
        public decimal TotalCrossMargin { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdrawable quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal? MaxWithdrawQuantity { get; set; }
    }


}
