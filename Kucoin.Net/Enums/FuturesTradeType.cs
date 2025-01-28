using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Type of trade
    /// </summary>
    public enum FuturesTradeType
    {
        /// <summary>
        /// Trade
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("liquid", "liquidation")]
        Liquidation,
        /// <summary>
        /// Adl
        /// </summary>
        [Map("adl", "ADL")]
        ADL,
        /// <summary>
        /// Settlement
        /// </summary>
        [Map("settlement")]
        Settlement
    }
}
