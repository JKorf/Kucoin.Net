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
        /// ["<c>closeId</c>"] Close id
        /// </summary>
        [JsonPropertyName("closeId")]
        public string CloseId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionId</c>"] Position id
        /// </summary>
        [JsonPropertyName("positionId")]
        public string? PositionId { get; set; }
        /// <summary>
        /// ["<c>uid</c>"] Uid
        /// </summary>
        [JsonPropertyName("uid")]
        public long? Uid { get; set; }
        /// <summary>
        /// ["<c>userId</c>"] User id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>settleCurrency</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? Side { get; set; }
        /// <summary>
        /// ["<c>closeSize</c>"] Close quantity
        /// </summary>
        [JsonPropertyName("closeSize")]
        public decimal? CloseQuantity { get; set; }
        /// <summary>
        /// ["<c>pnl</c>"] Profit and loss
        /// </summary>
        [JsonPropertyName("pnl")]
        public decimal? ProfitAndLoss { get; set; }
        /// <summary>
        /// ["<c>realisedGrossCost</c>"] Realised gross cost
        /// </summary>
        [JsonPropertyName("realisedGrossCost")]
        public decimal? RealisedGrossCost { get; set; }
        /// <summary>
        /// ["<c>realisedGrossCostNew</c>"] Realised gross cost (new?)
        /// </summary>
        [JsonPropertyName("realisedGrossCostNew")]
        public decimal? RealisedGrossCostNew { get; set; }
        /// <summary>
        /// ["<c>withdrawPnl</c>"] Withdraw profit and loss
        /// </summary>
        [JsonPropertyName("withdrawPnl")]
        public decimal? WithdrawPnl { get; set; }
        /// <summary>
        /// ["<c>tradeFee</c>"] Trading fee
        /// </summary>
        [JsonPropertyName("tradeFee")]
        public decimal? TradeFee { get; set; }
        /// <summary>
        /// ["<c>fundingFee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("fundingFee")]
        public decimal? FundingFee { get; set; }
        /// <summary>
        /// ["<c>openTime</c>"] Open time
        /// </summary>
        [JsonPropertyName("openTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>closeTime</c>"] Close time
        /// </summary>
        [JsonPropertyName("closeTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? CloseTime { get; set; }
        /// <summary>
        /// ["<c>openPrice</c>"] Open price
        /// </summary>
        [JsonPropertyName("openPrice")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>closePrice</c>"] Close price
        /// </summary>
        [JsonPropertyName("closePrice")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Closing type
        /// </summary>
        [JsonPropertyName("type")]
        public string CloseType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("marginMode")]
        public MarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>tax</c>"] Tax
        /// </summary>
        [JsonPropertyName("tax")]
        public decimal? Tax { get; set; }
        /// <summary>
        /// ["<c>roe</c>"] Return on equity
        /// </summary>
        [JsonPropertyName("roe")]
        public decimal? ReturnOnEquity { get; set; }
    }
}
