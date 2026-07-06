using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Market type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarketType>))]
    public enum MarketType
    {
        /// <summary>
        /// ["<c>CRYPTO</c>"] Crypto
        /// </summary>
        [Map("CRYPTO")]
        Crypto,
        /// <summary>
        /// ["<c>NASDAQ</c>"] Nasdaq
        /// </summary>
        [Map("NASDAQ")]
        Nasdaq
    }
}
