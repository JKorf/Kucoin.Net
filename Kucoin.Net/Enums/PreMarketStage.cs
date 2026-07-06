using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Pre-market stage
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PreMarketStage>))]
    public enum PreMarketStage
    {
        /// <summary>
        /// ["<c>NORMAL</c>"] Normal
        /// </summary>
        [Map("NORMAL")]
        Normal,
        /// <summary>
        /// ["<c>PRE_MARKET</c>"] Pre-market
        /// </summary>
        [Map("PRE_MARKET")]
        PreMarket
    }
}
