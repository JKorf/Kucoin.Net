using System;
using Kucoin.Net.Enums;


namespace Kucoin.Net.Objects.Models.Futures
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record KucoinFuturesUserTrade: KucoinTradeBase
    {
        /// <summary>
        /// ["<c>orderType</c>"] The type of the order
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// ["<c>tradeType</c>"] Trade type
        /// </summary>
        [JsonPropertyName("tradeType")]
        public FuturesTradeType TradeType { get; set; }

        /// <summary>
        /// ["<c>value</c>"] Order value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>fixFee</c>"] Fixed fee
        /// </summary>
        [JsonPropertyName("fixFee")]
        public decimal FixFee { get; set; }

        /// <summary>
        /// ["<c>tradeTime</c>"] Trade time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("tradeTime")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>settleCurrency</c>"] Settlement asset
        /// </summary>
        [JsonPropertyName("settleCurrency")]
        public string SettleAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>openFeePay</c>"] Opening transaction fee
        /// </summary>
        [JsonPropertyName("openFeePay")]
        public decimal? OpenFeePay { get; set; }
        /// <summary>
        /// ["<c>closeFeePay</c>"] Closing transaction fee
        /// </summary>
        [JsonPropertyName("closeFeePay")]
        public decimal? CloseFeePay { get; set; }
        /// <summary>
        /// ["<c>forceTaker</c>"] Whether to force processing as a taker
        /// </summary>
        [JsonPropertyName("forceTaker")]
        public bool ForceTaker { get; set; }
        /// <summary>
        /// ["<c>marginMode</c>"] Margin mode
        /// </summary>

        [JsonPropertyName("marginMode")]
        public FuturesMarginMode? MarginMode { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>

        [JsonPropertyName("positionSide")]
        public PositionSide? PositionSide { get; set; }
    }
}
