using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Hold balance
        /// </summary>
        [JsonPropertyName("holdBalance")]
        public decimal HoldBalance { get; set; }
        /// <summary>
        /// Isolated order margin
        /// </summary>
        [JsonPropertyName("isolatedOrderMargin")]
        public decimal IsolatedOrderMargin { get; set; }
        /// <summary>
        /// Isolated pos margin
        /// </summary>
        [JsonPropertyName("isolatedPosMargin")]
        public decimal IsolatedPosMargin { get; set; }
        /// <summary>
        /// Isolated unrealized profit and loss
        /// </summary>
        [JsonPropertyName("isolatedUnPnl")]
        public decimal IsolatedUnrealizedPnl { get; set; }
        /// <summary>
        /// Isolated funding fee margin
        /// </summary>
        [JsonPropertyName("isolatedFundingFeeMargin")]
        public decimal IsolatedFundingFeeMargin { get; set; }
        /// <summary>
        /// Cross order margin
        /// </summary>
        [JsonPropertyName("crossOrderMargin")]
        public decimal CrossOrderMargin { get; set; }
        /// <summary>
        /// Cross position margin
        /// </summary>
        [JsonPropertyName("crossPosMargin")]
        public decimal CrossPositionMargin { get; set; }
        /// <summary>
        /// Cross unrealized profit and loss
        /// </summary>
        [JsonPropertyName("crossUnPnl")]
        public decimal CrossUnrealizedPnl { get; set; }
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Total cross margin
        /// </summary>
        [JsonPropertyName("totalCrossMargin")]
        public decimal TotalCrossMargin { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Max withdrawable quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal? MaxWithdrawQuantity { get; set; }
    }


}
