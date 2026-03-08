using System;


namespace Kucoin.Net.Objects.Models.Futures.Socket
{
    /// <summary>
    /// Funding settlement update
    /// </summary>
    [SerializationModel]
    public record KucoinPositionFundingSettlementUpdate
    {
        /// <summary>
        /// ["<c>fundingTime</c>"] Funding time
        /// </summary>
        [JsonPropertyName("fundingTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundTime { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Position size
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Settlement price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>fundingFee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Asset used to clear and settle the trades
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
