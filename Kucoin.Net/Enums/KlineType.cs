using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Kline type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineType>))]
    public enum KlineType
    {
        /// <summary>
        /// Last price
        /// </summary>
        [Map("TRADE")]
        LastPrice,
        /// <summary>
        /// Index price
        /// </summary>
        [Map("INDEX_PRICE")]
        IndexPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE")]
        MarkPrice,
        /// <summary>
        /// Premium index
        /// </summary>
        [Map("PREMIUM_INDEX")]
        PremiumIndex,
    }
}
