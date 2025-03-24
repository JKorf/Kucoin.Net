using CryptoExchange.Net.Converters.SystemTextJson;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures account overview
    /// </summary>
    [SerializationModel]
    public record KucoinAccountOverview
    {
        /// <summary>
        /// Account equity = marginBalance + Unrealized PNL 
        /// </summary>
        [JsonPropertyName("accountEquity")]
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPNL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Margin balance = positionMargin + orderMargin + frozenFunds + availableBalance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// Frozen funds for withdrawal and out-transfer
        /// </summary>
        [JsonPropertyName("frozenFunds")]
        public decimal FrozenFunds { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Cross margin risk ratio
        /// </summary>
        [JsonPropertyName("riskRatio")]
        public decimal? RiskRatio { get; set; }
        /// <summary>
        /// Max withdrawable quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal? MaxWithdrawQuantity { get; set; }
    }
}
