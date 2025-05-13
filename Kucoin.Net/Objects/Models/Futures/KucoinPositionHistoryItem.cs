using CryptoExchange.Net.Converters.SystemTextJson;
using Kucoin.Net.Enums;

using System;

namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position history entry
    /// </summary>
    [SerializationModel]
    public record KucoinPositionHistoryItem
    {
        /// <summary>
        /// Close id
        /// </summary>
        [JsonPropertyName("closeId")]
        public string CloseId { get; set; } = string.Empty;
        /// <summary>
        /// Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public string? PositionId { get; set; }
        /// <summary>
        /// Uid
        /// </summary>
        [JsonPropertyName("uid")]
        public long? Uid { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? Side { get; set; }
        /// <summary>
        /// Close quantity
        /// </summary>
        [JsonPropertyName("closeSize")]
        public decimal? CloseQuantity { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal? ProfitAndLoss { get; set; }
        /// <summary>
        /// Realised gross cost
        /// </summary>
        [JsonPropertyName("realisedGrossCost")]
        public decimal? RealisedGrossCost { get; set; }
        /// <summary>
        /// Realised gross cost (new?)
        /// </summary>
        [JsonPropertyName("realisedGrossCostNew")]
        public decimal? RealisedGrossCostNew { get; set; }
        /// <summary>
        /// Withdraw profit and loss
        /// </summary>
        [JsonPropertyName("withdrawPnl")]
        public decimal? WithdrawPnl { get; set; }
        /// <summary>
        /// Trading fee
        /// </summary>
        [JsonPropertyName("tradeFee")]
        public decimal? TradeFee { get; set; }
        /// <summary>
        /// Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal? FundingFee { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("openTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Close time
        /// </summary>
        [JsonPropertyName("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CloseTime { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("closePrice")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// Closing type
        /// </summary>
        [JsonPropertyName("type")]
        public string CloseType { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal? Tax { get; set; }
        /// <summary>
        /// Return on equity
        /// </summary>
        [JsonPropertyName("roe")]
        public decimal? ReturnOnEquity { get; set; }
    }
}
