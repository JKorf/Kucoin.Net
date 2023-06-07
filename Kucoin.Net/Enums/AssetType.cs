using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// Crypto currency
        /// </summary>
        [Map("0")]
        CryptoCurrency,
        /// <summary>
        /// Fiat
        /// </summary>
        [Map("1")]
        Fiat
    }
}
