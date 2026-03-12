using CryptoExchange.Net.Attributes;

namespace Kucoin.Net.Enums
{
    /// <summary>
    /// Asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// ["<c>0</c>"] Crypto currency
        /// </summary>
        [Map("0")]
        CryptoCurrency,
        /// <summary>
        /// ["<c>1</c>"] Fiat
        /// </summary>
        [Map("1")]
        Fiat
    }
}
