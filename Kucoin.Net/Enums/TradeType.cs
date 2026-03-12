using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Type of trade
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeType>))]
    public enum TradeType
    {
        /// <summary>
        /// ["<c>TRADE</c>"] Spot trade
        /// </summary>
        [Map("TRADE")]
        SpotTrade,
        /// <summary>
        /// ["<c>MARGIN_TRADE</c>"] Margin trade
        /// </summary>
        [Map("MARGIN_TRADE")]
        MarginTrade,
        /// <summary>
        /// ["<c>MARGIN_ISOLATED_TRADE</c>"] Isolated margin trade
        /// </summary>
        [Map("MARGIN_ISOLATED_TRADE")]
        IsolatedMarginTrade
    }
}
