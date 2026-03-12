using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Type of trade
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTradeType>))]
    public enum FuturesTradeType
    {
        /// <summary>
        /// ["<c>trade</c>"] Trade
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// ["<c>liquid</c>"] Liquidation
        /// </summary>
        [Map("liquid", "liquidation")]
        Liquidation,
        /// <summary>
        /// ["<c>adl</c>"] Adl
        /// </summary>
        [Map("adl", "ADL")]
        ADL,
        /// <summary>
        /// ["<c>settlement</c>"] Settlement
        /// </summary>
        [Map("settlement")]
        Settlement
    }
}
