namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Futures account overview
    /// </summary>
    [SerializationModel]
    public record KucoinAccountOverview
    {
        /// <summary>
        /// ["<c>accountEquity</c>"] Account equity = marginBalance + Unrealized PNL 
        /// </summary>
        [JsonPropertyName("accountEquity")]
        public decimal AccountEquity { get; set; }
        /// <summary>
        /// ["<c>unrealisedPNL</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealisedPNL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>marginBalance</c>"] Margin balance = positionMargin + orderMargin + frozenFunds + availableBalance
        /// </summary>
        [JsonPropertyName("marginBalance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// ["<c>positionMargin</c>"] Position margin
        /// </summary>
        [JsonPropertyName("positionMargin")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>orderMargin</c>"] Order margin
        /// </summary>
        [JsonPropertyName("orderMargin")]
        public decimal OrderMargin { get; set; }
        /// <summary>
        /// ["<c>frozenFunds</c>"] Frozen funds for withdrawal and out-transfer
        /// </summary>
        [JsonPropertyName("frozenFunds")]
        public decimal FrozenFunds { get; set; }
        /// <summary>
        /// ["<c>availableBalance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>riskRatio</c>"] Cross margin risk ratio
        /// </summary>
        [JsonPropertyName("riskRatio")]
        public decimal? RiskRatio { get; set; }
        /// <summary>
        /// ["<c>maxWithdrawAmount</c>"] Max withdrawable quantity
        /// </summary>
        [JsonPropertyName("maxWithdrawAmount")]
        public decimal? MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>availableMargin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("availableMargin")]
        public decimal? AvailableMargin { get; set; }
    }
}
