using System;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Funding info
    /// </summary>
    [SerializationModel]
    public record KucoinFundingItem
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timePoint</c>"] Time point
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("timePoint")]
        public DateTime TimePoint { get; set; }
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>markPrice</c>"] Mark price
        /// </summary>
        [JsonPropertyName("markPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>positionQty</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionQty")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>positionCost</c>"] Position value at settlement period
        /// </summary>
        [JsonPropertyName("positionCost")]
        public decimal PositionCost { get; set; }
        /// <summary>
        /// ["<c>funding</c>"] Settled funding fees. A positive number means that the user received the funding fee, and vice versa.
        /// </summary>
        [JsonPropertyName("funding")]
        public decimal Funding { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
    }
}
